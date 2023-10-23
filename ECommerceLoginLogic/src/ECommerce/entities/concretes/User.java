package ECommerce.entities.concretes;

import ECommerce.entities.abstracts.IEntity;

public class User implements IEntity {
        private String FirstName;
        private String LastName;
        private String Email;
        private String Password;


        public User ( String firstName, String lastName, String email, String password ) {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                Password = password;
        }




        public String getFirstName () {
                return FirstName;
        }

        public void setFirstName ( String firstName ) {
                FirstName = firstName;
        }

        public String getLastName () {
                return LastName;
        }

        public void setLastName ( String lastName ) {
                LastName = lastName;
        }

        public String getEmail () {
                return Email;
        }

        public void setEmail ( String email ) {
                Email = email;
        }

        public String getPassword () {
                return Password;
        }

        public void setPassword ( String password ) {
                Password = password;
        }
}
