using Microsoft.AspNetCore.Identity;

namespace Entities.Models;


public class User:IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    //TIP: To implement refresh token!
    public string? RefreshToken {get; set;}
    public DateTime RefreshTokenExpiryDate {get; set;}

}