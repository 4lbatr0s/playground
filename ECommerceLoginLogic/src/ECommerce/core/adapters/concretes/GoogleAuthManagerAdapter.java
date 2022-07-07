package ECommerce.core.adapters.concretes;

import ECommerce.business.abstracts.IAuthService;
import ECommerce.googleAuth.GoogleAuthManager;

public class GoogleAuthManagerAdapter implements IAuthService {

    private GoogleAuthManager googleAuthManager;
    public GoogleAuthManagerAdapter ( GoogleAuthManager googleAuthManager ) {
        this.googleAuthManager = googleAuthManager;
    }

    @Override
    public void Login ( String username, String password ) {
        googleAuthManager.Login( username, password );
    }

    @Override
    public void Register ( String username, String password ) {
        googleAuthManager.SignUp ( username, password );
    }
}
