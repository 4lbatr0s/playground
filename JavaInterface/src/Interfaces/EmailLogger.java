package Interfaces;

public class EmailLogger implements ILogger {

    @Override
    public void Log ( String message ) {
        System.out.println ( "Email logged." + message );
    }
}
