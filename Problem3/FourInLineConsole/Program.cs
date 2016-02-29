using FourInLineConsole.DataTypes;
using FourInLineConsole.Interfaces;

namespace FourInLineConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameManager gameManager = new ConsoleGameManager();
            if (gameManager.Init())
                gameManager.Run();
        }
    }
}
