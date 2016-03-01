namespace FourInLineConsole.Interfaces.Infra
{
    public interface ILogger
    {
        void Info(string format, params object[] parameters);
    }
}