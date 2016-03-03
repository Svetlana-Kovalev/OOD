using System;
using FourInLineConsole.Interfaces.Infra;

namespace FourInLineConsole.Infra
{
    public class GameConsole : IGameConsole
    {
        #region Implementation of IGameConsole
        public void Write(string format, params object[] parameters)
        {
            Console.Write(format, parameters);
        }
        public void WriteLine(string format, params object[] parameters)
        {
            Console.WriteLine(format, parameters);
        }
        public void WriteLine()
        {
            Console.WriteLine();
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        #endregion
    }
}