package Concrete;

import Abstract.ICampageService;
import Abstract.IGameService;

import java.util.List;

public class GameManager implements IGameService {

    private ICampageService _campaignService;

    public GameManager ( ICampageService _campaignService ) {
        this._campaignService = _campaignService;
    }




    @Override
    public Game Create ( Game entity ) {
        return null;
    }

    @Override
    public Game Remove ( Game entity ) {
        return null;
    }

    @Override
    public Game Update ( Game entity ) {
        return null;
    }

    @Override
    public List<Game> GetAll () {
        return null;
    }

    @Override
    public Game GetById ( String id ) {
        return null;
    }

    public Double CalculateDiscountedPrice(Game game) {
        var involvedCampaign = _campaignService.GetById ( game.getInvolvedCampaign ().getId ());
        var discountedPrice = game.getPrice () * involvedCampaign.getCampaignDiscount ();
        game.setDiscountedPrice (  discountedPrice);
        return game.getDiscountedPrice ();
    }
}
