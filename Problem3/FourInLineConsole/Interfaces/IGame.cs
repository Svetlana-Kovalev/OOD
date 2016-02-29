using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.Interfaces
{
    public interface IGame
    {
        IPlayer Player1 { get; }
        IPlayer Player2 { get; }
        IBoard Board { get; }
        BoardStatus Status { get; }

        void PlaceDisk(IPlayer player, int column);
    }
}
