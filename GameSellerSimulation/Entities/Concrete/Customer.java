package Concrete;

import Abstract.IEntity;
import Utils.GuidOperations;

import java.util.ArrayList;
import java.util.List;

public class Customer implements IEntity {
    private String Id;
    private String Username;
    private String Email;
    private String Password;
    private Double WalletAmount;
    private List<Game> GameList;

    public Customer ( String id, String username, String email, String password, Double walletAmount, List<Game> gameList ) {
        setId ( );
        setUsername ( username );
        setEmail ( email );
        setPassword ( password );
        setWalletAmount ( walletAmount );
        GameList = new ArrayList<Game> ();
    }

    public String getId () {
        return Id;
    }

    public void setId (  ) {
        Id = GuidOperations.CreateRandomGuid ();
    }

    public String getUsername () {
        return Username;
    }

    public void setUsername ( String username ) {
        Username = username;
    }

    public String getEmail () {
        return Email;
    }

    public void setEmail ( String email ) {
        Email = email;
    }

    public String getPassword () {
        return Password;
    }

    public void setPassword ( String password ) {
        Password = password;
    }

    public Double getWalletAmount () {
        return WalletAmount;
    }

    public void setWalletAmount ( Double walletAmount ) {
        WalletAmount = walletAmount;
    }

    public List<Game> getGameList () {
        return GameList;
    }


}

