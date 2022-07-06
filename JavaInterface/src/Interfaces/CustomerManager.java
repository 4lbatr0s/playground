package Interfaces;

import jdk.jshell.execution.Util;

import javax.swing.text.Utilities;

public class CustomerManager {

    private ILogger [] _loggers;

    //loosly-tightly  coupled!
    public CustomerManager (ILogger[] loggers) {
        this._loggers = loggers;
    }
    public void Add (Customer customer) {
        System.out.println("Customer added " + customer.getFirstName ());
        // this._logger.Log (customer.getFirstName ()); //single logger.

        //multiple logger
        Utils.RunLoggers (_loggers, customer.getFirstName () );
    }

    public void Delete (Customer customer) {
        System.out.println( "Customer deleted " + customer.getFirstName ());
        //this._logger.Log ( customer.getFirstName ());
        Utils.RunLoggers ( _loggers, customer.getLastName () );
    }
}
