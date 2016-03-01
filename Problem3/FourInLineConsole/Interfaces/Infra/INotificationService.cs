namespace FourInLineConsole.Interfaces.Infra
{
    public interface INotificationEvent
    {
        
    }
    public interface INotificationService
    {
        void RaiseEvent(INotificationEvent notificationEvent);
    }
}