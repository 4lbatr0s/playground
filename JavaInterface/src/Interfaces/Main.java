package Interfaces;

public class Main {
    public static void main ( String[] args ) {
        CustomerManager customerManager = new CustomerManager(new SmsLogger ()); //cannot pass Interface, its not newable.
        Customer serhat = new Customer (1, "Serhat", "Oner");
        customerManager.Add(serhat);
    }
}
