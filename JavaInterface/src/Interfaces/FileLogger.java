package Interfaces;

public class FileLogger implements ILogger {
    @Override
    public void Log ( String message ) {
        System.out.println ( "File logged" + message );
    }
}


