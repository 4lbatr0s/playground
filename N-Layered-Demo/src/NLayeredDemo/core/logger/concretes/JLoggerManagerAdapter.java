package NLayeredDemo.core.logger.concretes;

import NLayeredDemo.core.logger.abstracts.ILoggerService;
import NLayeredDemo.jLogger.JLoggerManageer;

public class JLoggerManagerAdapter implements ILoggerService {
    /*
    * Microservice architecture:
    * 1. Create a folder.
    * 2. Create a service for the Microservice class
    * 3. Create an adapter class for the service interface
    * 4. Call microservice's functions in this adapter class
    * */
    @Override
    public void Log ( String message ) {
        JLoggerManageer loggerManageer = new JLoggerManageer();
        loggerManageer.Log ( message );
    }
}
