package Interfaces;

public class SmsLogger implements ILogger {

    @Override
    public void Log ( String message ) {
        System.out.println("Sms logged." + message );
    }
}


