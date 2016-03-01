using System;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Infra;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class HumanConsoleStrategy : IStrategy
    {
        private readonly IHumanPlayer m_player;
        private readonly IGameConsole m_gameConsole;

        public HumanConsoleStrategy(IGame game, IHumanPlayer player, IGameConsole gameConsole)
        {
            Game = game;
            m_player = player;
            m_gameConsole = gameConsole;
        }

        #region IStrategy
        public IGame Game { get; private set; }
        public IPlayer Player { get { return m_player; } }
        public void MakeNextStep()
        {
            m_gameConsole.Write("Player {0}, choose a column: ", m_player.Name);
            int column = Int32.Parse(m_gameConsole.ReadLine()); // no exception handling...
            Game.PlaceDisk(m_player, column);
        }
        #endregion
    }
}