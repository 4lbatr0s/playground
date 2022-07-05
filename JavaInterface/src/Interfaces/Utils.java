package Interfaces;

public class Utilities {

    //multiple logger.
    public static void RunLoggers(ILogger[] loggers, String message) {
        for(ILogger logger: loggers){
            logger.Log (message );
        }
    }

}
