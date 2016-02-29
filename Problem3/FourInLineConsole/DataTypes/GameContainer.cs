using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;

namespace FourInLineConsole.DataTypes
{
    public class GameContainer : IGameContainer
    {
        private readonly IGame m_game;
        private readonly IStrategy m_strategyPlayer1;
        private readonly IStrategy m_strategyPlayer2;
        private IStrategy m_activeStrategy;
        private IStrategy m_lastStep;
        public GameContainer(IGame game, IStrategy strategy1, IStrategy strategy2)
        {
            m_game = game;
            m_strategyPlayer1 = strategy1;
            m_strategyPlayer2 = strategy2;
            m_activeStrategy = m_strategyPlayer1;
            m_lastStep = null;
        }
        #region Implementation of IGameContainer
        public IGame GetGame()
        {
            return m_game;
        }
        public IStrategy GetStrategyPlayer1() {  return m_strategyPlayer1;}
        public IStrategy GetStrategyPlayer2() {  return m_strategyPlayer2;}
        public IStrategy GetLastStep() { return m_activeStrategy; }
        public IStrategy GetActiveStrategy() { return m_activeStrategy; }
        public void NextStep()
        {
            if (m_activeStrategy == m_strategyPlayer1)
                m_strategyPlayer1.MakeNextStep();
            else
                m_strategyPlayer2.MakeNextStep();
            m_lastStep = m_activeStrategy;

            switch (m_game.Status)
            {
                case BoardStatus.Active:
                    ChangePlayer();
                    break;
                case BoardStatus.Finished:
                    break;
                case BoardStatus.Full:
                    break;
            }
        }
        #endregion
        private void ChangePlayer()
        {
            m_activeStrategy = m_activeStrategy == m_strategyPlayer1 ? m_strategyPlayer2 : m_strategyPlayer1;
        }
    }
}