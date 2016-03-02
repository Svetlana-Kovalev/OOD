using FourInLineConsole.DataTypes;
using FourInLineConsole.Infra;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Infra;
using Microsoft.Practices.Unity;

namespace FourInLineConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityContainer unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<IGameInfrastructure>(new Infrastructure());
            unityContainer.RegisterType<IGameConsole, GameConsole>();
            unityContainer.RegisterType<ILoggerFactory, FileLoggerFactory>();
            unityContainer.RegisterType<INotificationService, NotificationService>();
            unityContainer.RegisterType<IGameManager, ConsoleGameManager>();

            IGameManager gameManager = unityContainer.Resolve<IGameManager>();

            if (gameManager.Init())
                gameManager.Run();
        }
    }
}
