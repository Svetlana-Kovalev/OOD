using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Infra;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class ConsoleBoardViewer : IBoardViewer
    {
        private readonly IGame m_game;
        private readonly IGameConsole _gameConsole;
        private readonly IBoard m_board;

        public ConsoleBoardViewer(IGame game, IGameConsole gameConsole)
        {
            m_game = game;
            _gameConsole = gameConsole;
            m_board = game.Board;
        }

        #region IBoardViewer
        public void DisplayBoard()
        {
            _gameConsole.WriteLine("Printing board:");
            _gameConsole.WriteLine();
            for (int j = 0; j < m_board.Rows; j++)
            {
                _gameConsole.Write("|");
                for (int k = 0; k < m_board.Columns; k++)
                    _gameConsole.Write(ConvertToChar(m_board[j, k]) + "|");
                _gameConsole.WriteLine();
            }
            for (int k = 0; k < 2 * m_board.Columns + 1; k++)
                _gameConsole.Write("-");
            _gameConsole.WriteLine();
            _gameConsole.WriteLine();
        }
        #endregion
        private char ConvertToChar(IPlayer player)
        {
            if (player == null)
                return ' ';
            return player == m_game.Player1 ? 'X' : 'O';
        }
    }
}