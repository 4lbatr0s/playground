package Concrete;

import Abstract.IEntity;
import Utils.GuidOperations;

import java.util.ArrayList;
import java.util.List;

public class Game implements IEntity {
    private String Id;
    private String Name;
    private Double Price;
    private Double DiscountedPrice;
    private Campaign InvolvedCampaign;


    public Game () {

    }

    public Game ( String id, String name, Double price, Double discountedPrice, Campaign involvedCampaign ) {
        super();
        setId ();
        setName ( name );
        setPrice ( price );
        setDiscountedPrice ( discountedPrice );
        setInvolvedCampaign ( involvedCampaign );
    }

    public String getId () {
        return Id;
    }

    public void setId (  ) {
        Id = GuidOperations.CreateRandomGuid ();
    }

    public String getName () {
        return Name;
    }

    public void setName ( String name ) {
        Name = name;
    }

    public Double getPrice () {
        return Price;
    }

    public void setPrice ( Double price ) {
        Price = price;
    }

    public Double getDiscountedPrice () {
        return DiscountedPrice;
    }

    public void setDiscountedPrice ( Double discountedPrice ) {
        DiscountedPrice = discountedPrice;
    }

    public Campaign getInvolvedCampaign () {
        return InvolvedCampaign;
    }

    public void setInvolvedCampaign ( Campaign involvedCampaign ) {
        InvolvedCampaign = involvedCampaign;
    }
}
