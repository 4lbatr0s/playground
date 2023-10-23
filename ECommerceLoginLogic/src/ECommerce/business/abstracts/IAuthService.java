package ECommerce.business.abstracts;

public interface IAuthService {

    void Login(String username, String password);
    void Register(String username, String password);
}
