package Managers;
import Student;
import Loggers.BaseLogger;

public class InstructorManager extends UserManager{
    public InstructorManager ( BaseLogger logger ) {
        super ( logger );
    }

    public void kickFromServer ( Student student ) {
        student.setKickedFromServer ( true );
    }
}
