using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;

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
    private readonly IConfiguration _configuration;

    private User? _user;


    public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
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
        _user = await _userManager.FindByNameAsync(userForAuthentication.UserName);
        var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthentication.Password));
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong username or password.");
        return result;
    }

    /// <summary>Creates a JWT Token</summary>
    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }




    //PRIVATE METHODS TO IMPLEMENT CREATETOKEN FUNCTION!

    /// <summary>Returns our secret key as a byte array with the security algorithm</summary>
    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JWTSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"]));
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
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

}