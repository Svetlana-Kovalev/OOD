using FourInLineConsole.Interfaces.Infra;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.Infra
{
    public class ChangedUserEvent : INotificationEvent
    {
        public IPlayer Player { get; set; }
        public ChangedUserEvent(IPlayer player)
        {
            Player = player;
        }
    }

    public class NotificationService : INotificationService
    {
        private readonly IGameConsole m_gameConsole;

        public NotificationService(IGameConsole gameConsole)
        {
            m_gameConsole = gameConsole;
        }

        #region INotificationService
        public void RaiseEvent(INotificationEvent notificationEvent)
        {
            ChangedUserEvent userEvent = notificationEvent as ChangedUserEvent;
            if (userEvent != null)
            {
                m_gameConsole.WriteLine("Send notification to player '{0}'.", userEvent.Player.Name);
            }
        }
        #endregion
    }
}