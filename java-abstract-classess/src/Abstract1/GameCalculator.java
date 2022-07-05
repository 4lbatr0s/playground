public abstract class GameCalculator {
    public abstract void Calculate ();
    public final void GameOver(){ //dont override it.
        System.out.println("Game Over");
    }
 }

