package Concrete;

import Abstract.IEntity;
import Utils.GuidOperations;

import java.util.ArrayList;
import java.util.List;

public class Campaign implements IEntity {
    private String Id;
    private String campaignName;
    private double campaignDiscount;

    private List<Game> Games;

    public Campaign ( int id, String campaignName, double campaignDiscount ) {
        setId ();
        this.setCampaignName ( campaignName );
        this.setCampaignDiscount ( campaignDiscount );
        Games = new ArrayList<Game> ();
    }

    public String getId () {
        return Id;
    }

    public void setId ( ) {
         this.Id = GuidOperations.CreateRandomGuid ();
    }

    public String getCampaignName () {
        return campaignName;
    }

    public void setCampaignName ( String campaignName ) {
        this.campaignName = campaignName;
    }

    public double getCampaignDiscount () {
        return campaignDiscount;
    }

    public void setCampaignDiscount ( double campaignDiscount ) {
        this.campaignDiscount = campaignDiscount;
    }

    public List<Game> getGames () {
        return Games;
    }

    public void addGameToCampaign ( Game game) {
        Games.add(game);
    }
}
