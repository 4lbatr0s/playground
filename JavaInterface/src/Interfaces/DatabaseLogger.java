package Interfaces;

public class DatabaseLogger implements ILogger {
    @Override
    public void Log ( String message ) {
        System.out.println( "Database loglandi" + message );
    }
}
