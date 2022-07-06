package Abstract;

import Concrete.Campaign;
import Concrete.Game;

public interface IGameService extends IBaseService<Game> {

    public Double CalculateDiscountedPrice(Game game);
}

