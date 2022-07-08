package ECommerce.business.businessRules.concretes;

import ECommerce.business.businessRules.abstracts.IBusinessRules;
import ECommerce.entities.concretes.User;

import java.util.List;
import java.util.regex.Pattern;

public class UserBusinessRules  {
    public static boolean passwordMustBeLongerThanSixCharactes(User user) {
        return user.getPassword ().length ()>6;
    }

    //if macthes, return true, else return false!
    public  boolean emailFormatMustBeValid(User user) {
            var regexPattern = "^(.+)@(\\S+)$";
            return Pattern.compile(regexPattern)
                    .matcher(user.getEmail ())
                    .matches();
    }

    public static boolean userEmailMustBeUniqueValue(final List<User> list, final String email){
        return list.stream().filter(o -> o.getEmail ().equals(email)).findFirst().isPresent(); //if contains, returns true else, returns false.
    }

    public static boolean userNameAndSurnameMustBeLongerThanTwoCharacters(User user){
     return user.getFirstName ().length() > 2 && user.getLastName ().length() > 2;
    }


}
