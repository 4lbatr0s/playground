package Managers;

import Loggers.BaseLogger;

public class UserManager {
    private BaseLogger _logger;
    private boolean _userOnlineStatus;
    public UserManager ( BaseLogger logger ) {
        this._logger = logger;
    }


    public void Login( String email, String password ) {
        System.out.println("User login," + _logger.Log ());
        set_userOnlineStatus ( true ); //if user login, make userOnlineStatus true!
    }

    public void LogOut( ) {
        System.out.println("User login," + _logger.Log ());
        set_userOnlineStatus ( false );
    }

    public boolean is_userOnlineStatus () {
        return _userOnlineStatus;
    }

    public void set_userOnlineStatus ( boolean _userOnlineStatus ) {
        this._userOnlineStatus = _userOnlineStatus;
    }

    public void getOnlineStatus() {
        if ( _userOnlineStatus ) {
            System.out.println("User online!");
        } else {
            System.out.println("User not online!");
        }
    }
}
