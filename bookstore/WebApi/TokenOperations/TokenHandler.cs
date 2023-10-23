using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;

namespace WebApi.TokenOperations.Models;

public class TokenHandler{
    

    public IConfiguration Configuration;
    public TokenHandler(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    public Token CreateAccessToken(User user)
    {
        Token tokenModel = new Token(); //token modeli olusturuldu.
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])); //Simetrik security key olusturuldu.
        
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 

        tokenModel.ExpirationDate  = DateTime.Now.AddMinutes(15); //15 dakikalık bir access token üretmek istiyoruz.

        JwtSecurityToken securityToken = new JwtSecurityToken(
            audience:Configuration["Token:Audience"],
            issuer:Configuration["Token:Issuer"],
            expires:tokenModel.ExpirationDate,
            notBefore:DateTime.Now, //token üretilmeye başlandıgı andan itibaren üretilebilir.
            signingCredentials: credentials
        );


        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler(); //token handler,
        tokenModel.AccessToken  = tokenHandler.WriteToken(securityToken); //security tokenden bir token yarat
        tokenModel.RefreshToken = CreateRefreshToken(); //refresh token ile her yaratımda, yeniden üretilir.
        

        return tokenModel;
         
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}