namespace FourInLineConsole.Interfaces.Infra
{
    public interface IGameConsole
    {
        void Write(string format, params object[] parameters);
        void WriteLine(string format, params object[] parameters);
        void WriteLine();
        string ReadLine();
    }
}