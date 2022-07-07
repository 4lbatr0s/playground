import ECommerce.dataAcces.concretes.UserDao;
import ECommerce.entities.concretes.User;

public class Main {
    public static void main ( String[] args ) {
        User user = new User ("Serhat", "Oner", "serhatoner@protonmail.com", "as5dkasd1241asd");
        User user2 = new User ("Private", "Jackson", "privatejackson@gmail.com", "asdkoa12412541");
        UserDao userDao = new UserDao();
        userDao.Add ( user );
        userDao.Add ( user2 );

        for (User userObject: userDao.GetAll ()) {
            System.out.println(userObject.getFirstName ());
        }

    }
}