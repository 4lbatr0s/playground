package ECommerce.business.abstracts;

import ECommerce.entities.concretes.User;

public interface IUserService extends  IBaseService<User> {

    void ClickActivationLink ();

}
