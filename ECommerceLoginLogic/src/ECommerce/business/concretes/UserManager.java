package ECommerce.business.concretes;

import ECommerce.business.abstracts.IAuthService;
import ECommerce.business.abstracts.IUserService;
import ECommerce.business.businessRules.concretes.UserBusinessRules;
import ECommerce.business.constrains.Constrains;
import ECommerce.core.email.abstracts.IEmailService;
import ECommerce.dataAcces.concretes.UserDao;
import ECommerce.entities.concretes.User;

import java.util.List;

public class UserManager implements IUserService {

    private UserBusinessRules userBusinessRules;
    private UserDao _userDao;
    private IAuthService _authService;
    private IEmailService _emailService;


    public UserManager ( UserBusinessRules userBusinessRules, UserDao _userDao, IAuthService authService, IEmailService emailService ) {
        this.userBusinessRules = userBusinessRules;
        this._userDao = _userDao;
        this._authService = authService;
        this._emailService = emailService;
    }


    @Override
    public User Create ( User user ) {
        if(userBusinessRules.emailFormatMustBeValid ( user )
                && userBusinessRules.emailFormatMustBeValid ( user )
                && !userBusinessRules.userEmailMustBeUniqueValue (_userDao.GetAll (), user.getEmail ())){
            _authService.Register ( user.getEmail (), user.getPassword () );
            this.ClickActivationLink();
            _emailService.SendEmail ( Constrains.getActivationSuccesful () );
            return user;
        } else {
            System.out.println ("Validations are not fullfilled");
            return user;
        }
    }

    @Override
    public User Update ( User user ) {
        System.out.println (user.getFirstName () + " updated!");
        return user;
    }

    @Override
    public User Delete ( User user ) {
        _userDao.Remove ( user );
        return user;
    }

    @Override
    public List<User> Get () {
        return _userDao.GetAll ();
    }

    @Override
    public User GetById ( int id ) {
        return null;
    }


    @Override
    public void ClickActivationLink () {
        System.out.println( Constrains.getClickedActivationLink ());
    }
}
