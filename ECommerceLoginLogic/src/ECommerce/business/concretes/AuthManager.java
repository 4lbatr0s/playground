package ECommerce.business.concretes;

import ECommerce.business.abstracts.IAuthService;
import ECommerce.business.constrains.Constrains;

public class AuthManager implements IAuthService {
    @Override
    public void Login ( String username, String password ) {
        System.out.println( "Logged with system credentials: " + username );
    }

    @Override
    public void Register ( String username, String password ) {
        System.out.println( "Registered with system credentials: " + username);
        System.out.println( Constrains.getRegisteredEmail ());
    }
}
