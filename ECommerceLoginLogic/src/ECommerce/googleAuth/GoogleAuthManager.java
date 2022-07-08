package ECommerce.googleAuth;

import ECommerce.business.constrains.Constrains;

public class GoogleAuthManager {

    public void SignUpWithGoogle ( String username, String password) {
        System.out.println("Signed up with google: " + username);
        System.out.println( Constrains.getRegisteredEmail ());
    }

    public void LoginWithGoogle ( String username, String password) {
        System.out.println("Logged in with google: " + username);
    }
}
