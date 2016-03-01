using System;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class HumanConsoleStrategy : IStrategy
    {
        private readonly IHumanPlayer m_player;

        public HumanConsoleStrategy(IGame game, IHumanPlayer player)
        {
            Game = game;
            m_player = player;
        }

        #region IStrategy
        public IGame Game { get; private set; }
        public IPlayer Player { get { return m_player; } }
        public void MakeNextStep()
        {
            Console.Write("Player " + m_player.Name + ", choose a column: ");
            int column = Int32.Parse(Console.ReadLine()); // no exception handling...
            Game.PlaceDisk(m_player, column);
        }
        #endregion
    }
}