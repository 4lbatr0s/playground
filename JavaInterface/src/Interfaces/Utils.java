package Interfaces;

public class Utils { //we can't declare STATIC CLASSES in JAVA. But if we declare a class inside of a class, then it can be declared as a static class

        //for example:

    public static class UtilsStatic { }

    //multiple logger.
    public static void RunLoggers ( ILogger[] loggers, String message ) {

        for (ILogger logger : loggers) {
            logger.Log ( message );
        }
    }

}
