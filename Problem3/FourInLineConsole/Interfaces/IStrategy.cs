using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.Interfaces
{
    public interface IStrategy
    {
        IGame Game { get; }
        IPlayer Player { get; }
        void MakeNextStep();
    }
}