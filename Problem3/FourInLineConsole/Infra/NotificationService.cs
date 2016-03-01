using System;
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
        #region INotificationService
        public void RaiseEvent(INotificationEvent notificationEvent)
        {
            if (notificationEvent is ChangedUserEvent)
            {
                Console.WriteLine("Send notification to player '{0}'.", ((ChangedUserEvent) notificationEvent).Player.Name);
            }
        }

        #endregion
    }
}