using System;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class ConsoleBoardViewer : IBoardViewer
    {
        private readonly IGame m_game;
        private readonly IBoard m_board;

        public ConsoleBoardViewer(IGame game)
        {
            m_game = game;
            m_board = game.Board;
        }

        #region IBoardViewer
        public void DisplayBoard()
        {
            Console.WriteLine("Printing board:");
            Console.WriteLine();
            for (int j = 0; j < m_board.Rows; j++)
            {
                Console.Write("|");
                for (int k = 0; k < m_board.Columns; k++)
                    Console.Write(ConvertToChar(m_board[j, k]) + "|");
                Console.WriteLine();
            }
            for (int k = 0; k < 2 * m_board.Columns + 1; k++)
                Console.Write("-");
            Console.WriteLine();
            Console.WriteLine();
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