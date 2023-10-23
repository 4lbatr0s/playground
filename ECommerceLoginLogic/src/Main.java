import ECommerce.business.abstracts.IUserService;
import ECommerce.business.businessRules.concretes.UserBusinessRules;
import ECommerce.business.concretes.AuthManager;
import ECommerce.business.concretes.UserManager;
import ECommerce.core.email.concretes.EmailManager;
import ECommerce.dataAcces.concretes.UserDao;
import ECommerce.entities.concretes.User;
import ECommerce.googleAuth.GoogleAuthManager;

public class Main {
    public static void main ( String[] args ) {
        User user = new User ("Serhat", "Oner", "serhatoner@protonmail.com", "as5dkasd1241asd");
        User user2 = new User ("Private", "Jackson", "privatejackson@gmail.com", "asdkoa12412541");
        IUserService userService = new UserManager ( new UserBusinessRules (),new UserDao(), new AuthManager(), new EmailManager () );
        userService.Create ( user );
        System.out.println(userService.Get ());

    }
}