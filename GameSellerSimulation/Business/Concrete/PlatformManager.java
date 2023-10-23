package Concrete;

import Abstract.ICustomerService;
import Abstract.IGameService;
import Abstract.IPlatformService;

public class PlatformManager implements IPlatformService {

    private ICustomerService _customerService;
    private IGameService _gameService;

    public PlatformManager ( ICustomerService _customerService, IGameService _gameService ) {
        this._customerService = _customerService;
        this._gameService = _gameService;
    }


    @Override
    public void Sell ( Customer customer, Game game ) {

        if(_customerService.CheckIfCustomerWalletBiggerThan ( customer, _gameService.CalculateDiscountedPrice ( game )))
            System.out.println("Customer: " + customer  + " bought " + game.getName ());


    }

    @Override
    public void Login ( Customer customer ) {

    }

    @Override
    public void Register ( Customer customer ) {

    }

    @Override
    public void AddGameToCampaign ( Game game, Campaign campaign ) {
        game.setInvolvedCampaign ( campaign);
        campaign.addGameToCampaign (game);
    }

}
