using FourInLineConsole.Infra;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Infra;

namespace FourInLineConsole.DataTypes
{
    public class GameContainer : IGameContainer
    {
        private readonly IGame m_game;
        private readonly IStrategy m_strategyPlayer1;
        private readonly IStrategy m_strategyPlayer2;
        private readonly INotificationService m_notificationService;
        private IStrategy m_activeStrategy;
        private IStrategy m_lastStep;
        public GameContainer(IGame game, IStrategy strategy1, IStrategy strategy2, INotificationService notificationService)
        {
            m_game = game;
            m_strategyPlayer1 = strategy1;
            m_strategyPlayer2 = strategy2;
            m_notificationService = notificationService;
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
        public IStrategy GetLastStep() { return m_lastStep; }
        public IStrategy GetActiveStrategy() { return m_activeStrategy; }
        public void NextStep()
        {
            m_activeStrategy.MakeNextStep();
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
            OnChanged(new ChangedUserEvent(m_activeStrategy.Player));
        }
        protected virtual void OnChanged(INotificationEvent notificationEvent)
        {
            if (m_notificationService != null)
            {
                m_notificationService.RaiseEvent(notificationEvent);
            }
        }
    }
}