namespace FourInLineConsole.Interfaces
{
    public interface ILogger
    {
        void Info(string format, params object[] parameters);
    }
}