package Abstract2;

public class Main {
    public static void main ( String[] args ) {
        CustomerManager customerManager = new CustomerManager();
        //ileride yeni bir veritabanıyla calısmak istediğimizde sadece burayı degistireceğiz.
        //constructor ile yapılanı burada instance örneği değiştirerek yaptık!
        //SOLID'IN 2.HARFI: Open Closed Principle, mevcuttaki hiç bir kodu değiştiremezsin.Bir yerlerde kodu değştiriyorsan arıza var. Kod değiştirilmemeli ama geliştirilebilmeli.
        customerManager.databaseManagement = new OracleDatabaseManager (); //STRATEGY PATTERN!
        customerManager.getCustomers ();
    }
}


