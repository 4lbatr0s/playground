package Abstract;

import Concrete.Campaign;
import Concrete.Customer;
import Concrete.Game;

public interface IPlatformService {
    void Sell( Customer customer, Game game); //if there is a campaign, then implement it.
    void Login(Customer customer);
    void Register(Customer customer);

    void AddGameToCampaign( Game game, Campaign campaign);
}
