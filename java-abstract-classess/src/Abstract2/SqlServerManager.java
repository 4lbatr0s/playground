package Abstract2;

public class SqlServerManager extends BaseDatabaseManagement {
    @Override
    public void getData () {
        System.out.println ( "SQL ServerManager data fetched!" );
    }
}
