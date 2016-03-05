using System;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class ComputerStandardStrategy : IStrategy
    {
        private readonly IComputerPlayer m_player;
        private readonly IPlayer m_otherPlayer;

        public ComputerStandardStrategy(IGame game, IComputerPlayer player, IPlayer otherPlayer)
        {
            Game = game;
            m_player = player;
            m_otherPlayer = otherPlayer;
        }

        #region IStrategy
        public IGame Game { get; private set; }
        public IPlayer Player { get { return m_player;  } }
        public void MakeNextStep()
        {
            int column = ComputerChoice();
            if (column<0)
                throw new Exception();
            Game.PlaceDisk(m_player, column);
        }
        #endregion

        public int ComputerChoice()
        {
            int emptyrow = 0;
            // first check if a move can win
            for (int i = 0; i < Game.Board.Columns; i++)
            {
                if (!Game.Board.IsColumnFull(i))
                {
                    emptyrow = Game.Board.FirstEmptyRow(i);
                    if (Game.Board.IsWin(emptyrow, i, m_player))
                        return i;
                }
            }
            // otherwise then pick up any move that will prevent other player to win 
            // in case there is a win on next turn
            int counter = 0; // i count other player possible winnings
            int chosenrow = 0;
            for (int i = 0; i < Game.Board.Columns; i++)
            {
                if (!Game.Board.IsColumnFull(i))
                {
                    emptyrow = Game.Board.FirstEmptyRow(i);
                    if (Game.Board.IsWin(emptyrow, i, m_otherPlayer))
                    {
                        counter++; // we found a winning disc
                        chosenrow = i; // remember the row
                    }
                }
            }
            // we block the player if there is exactly one winning disc 
            if (counter == 1) return chosenrow;

            // else if other player wins no matter what, pick up first non full column
            for (int i = 0; i < Game.Board.Columns; i++)
                if (!Game.Board.IsColumnFull(i))
                {
                    return i;
                }
            return -1;
        }
    }
}