using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Entities.Models.ConfigurationModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Service;


/*
    Required Packages:
    System.IdentityModel.Tokens.Jwt 
*/
internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager; //TIP: provided by Aspnet core Identity API
    private  IOptionsMonitor<JwtConfiguration> _configuration;
    private User? _user; //INFO: We get user info in the ValidateUser method!, because its a heap value, we are able to change it to the current user!
    private readonly JwtConfiguration _jwtConfiguration;

    public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IOptionsMonitor<JwtConfiguration> configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _jwtConfiguration = _configuration.CurrentValue;
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);//Returns an error if its not successfull!
        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles); //TIP: if user created, add the roles!
        return result;
    }


    /// <summary>Checks if user does exist, if exists controls username and password.If wrong, logs error, else returns a flag.</summary>
    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthentication)
    {
        _user = await _userManager.FindByNameAsync(userForAuthentication.UserName); //TIP: Where we set our user!
        var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthentication.Password));
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");
        return result;
    }

    /// <summary>Creates access and refresh token</summary>
    public async Task<TokenDto> CreateToken(bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        var refreshToken = GenerateRefreshToken();
        _user.RefreshToken = refreshToken;
        if(populateExp)
            _user.RefreshTokenExpiryDate = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(_user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new TokenDto(accessToken, refreshToken);
    }



    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        var user = await _userManager.FindByNameAsync(principal.Identity.Name);//TIP: Identity.Name is the Username of the user!

        //INFO:
        // If the user doesn’t exist, or the refresh
        // tokens are not equal, or the refresh token has expired, we stop the flow
        // returning the BadRequest response to the user
        if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryDate<DateTime.UtcNow)
                throw new RefreshTokenBadRequest();
        _user = user;
        return await CreateToken(populateExp:true);
    }

    //PRIVATE METHODS TO IMPLEMENT CREATETOKEN FUNCTION!
    /// <summary>Returns our secret key as a byte array with the security algorithm</summary>
    private SigningCredentials GetSigningCredentials()
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));
        return new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
    }

    /// <summary> creates a list of claims with the user name inside and all the roles the user belongs to.</summary>
    private async Task<List<Claim>> GetClaims()
    {
        //INFO: Claim: Username, Password, email adress etc...
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(_user); //bring the roles

        /*
        INFO:
        Claims and Roles are different, but adding a role to the Claims list allows us to associate a user with
        specific roles, which can be used in authorization. By including the user's roles in the Claims list,
        we can make authorization decisions based on the user's role, 
        such as granting or denying access to specific resources. 
        This allows us to specify different permissions based on the user's role.
        */

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    /// <summary>creates an object of the JwtSecurityToken type with all of the required options.</summary>
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
    List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
        issuer: _jwtConfiguration.ValidIssuer,
        audience: _jwtConfiguration.ValidAudience,
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    //INFO: Private methods for RefreshToken implementation.

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte [32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }



    /*
        INFO: GENERAL INFORMATION ABOUT 'USER PRINCIPAL' AND ClaimsPrincipal
        A user principal represents a user in the context of the application. 
        It contains information about the user such as their identity, claims, and roles.
        For example, consider a web application that requires users to log in to access certain features.
        When a user logs in, their credentials are verified, and if successful,
        a user principal is created and stored in the application's security context. 
        This user principal is then used to enforce access control, 
        allowing the application to determine what actions the user is authorized to perform 
        based on their claims and roles.

        TIP: Principal = USER!

        //INFO:    
        The ClaimsPrincipal object is a representation of the identity of a user in .NET.
        It holds a collection of Claim objects, which contain information about the user such as their name,
        email address, and any other custom claims that may be defined.
        A "user principal" is a term used to describe the identity of a user in a system.
        It encompasses all the information that represents a user and their privileges within that system.
        The ClaimsPrincipal object can be thought of as the object-oriented representation of a user principal
        in .NET.
    */
    /// <summary>Returns principal from the expired access token</summary>
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey)),
            ValidateLifetime=true,
            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var JwtSecurityToken = securityToken as JwtSecurityToken;//TIP: How to convert SecurityToken to JWTSecurity token, because JWTSecurityToken is a SecurityToken implementation.
        if(JwtSecurityToken == null || !JwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token!");    
            }
        return principal;
    }

}