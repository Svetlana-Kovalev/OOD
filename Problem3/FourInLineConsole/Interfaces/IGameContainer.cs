namespace FourInLineConsole.Interfaces
{
    public interface IGameContainer
    {
        IGame GetGame();
        IStrategy GetStrategyPlayer1();
        IStrategy GetStrategyPlayer2();
        IStrategy GetLastStep();
        IStrategy GetActiveStrategy();

        void NextStep();
    }
}