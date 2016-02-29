using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class Game : IGame
    {
        public Game(IBoard board, IPlayer player1, IPlayer player2)
        {
            Board = board;
            Player1 = player1;
            Player2 = player2;
        }

        #region IGame
        public IPlayer Player1 { get; private set; }
        public IPlayer Player2 { get; private set; }
        public IBoard Board { get; private set; }
        public BoardStatus Status
        {
            get { return Board.Status; }
        }
        public void PlaceDisk(IPlayer player, int column)
        {
            Board.PlaceDisk(player, column);
        }
        #endregion
    }
}