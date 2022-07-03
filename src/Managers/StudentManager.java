package Managers;
import Loggers.BaseLogger;

public class StudentManager extends  UserManager{
    public StudentManager ( BaseLogger logger ) {
        super ( logger );
    }

    public void likeInstructor( Instructor instructor) {
        instructor.setNumberOfLikes ( instructor.getNumberOfLikes () + 1);
    }

}
