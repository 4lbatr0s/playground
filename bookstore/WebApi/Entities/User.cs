
namespace WebApi.Entities;


public class User
{
    //access token: erişim için kullandıgımız, esas token
    //refresh token: access tokenin süresi bittiğinde kullanıcıyı çıkarmıyorsak, refresh token kullanarak yeni bir access token alırız, bunu refresh token ile yaparız. 
    //refresh token browserin local storage vb'de bir yerde saklanıyor  
    public int Id {get; set;}
    public string Name {get; set;}
    public string Surname {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string? RefreshToken {get; set;}
    public DateTime? RefreshTokenExpireDate {get; set;}


}