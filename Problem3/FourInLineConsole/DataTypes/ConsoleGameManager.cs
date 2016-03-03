using System;
using FourInLineConsole.Infra;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Infra;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{    
    public class ConsoleGameManager : IGameManager
    {
        private readonly INotificationService m_notificationService;
        private readonly IGameConsole m_gameConsole;
        // the main menu
        public enum UserChoice { QUIT, PLAY, PLAYCOMPUTER };
    
        private IBoardViewer m_boardViewer;
        private IGameContainer m_gameContainer;
        private readonly ILogger m_logger;

        public ConsoleGameManager(ILoggerFactory loggerFactory, INotificationService notificationService, IGameConsole gameConsole)
        {
            m_notificationService = notificationService;
            m_gameConsole = gameConsole;
            m_logger = loggerFactory.Create();
            m_logger.Info("created at {0}", DateTime.Now);
        }

        #region IGameManager
        public bool Init()
        {            
            PrintWelcome();
            UserChoice userChoice = GetUserChoice();

            switch (userChoice)
            {
                case UserChoice.QUIT:
                    return false;
                case UserChoice.PLAY:
                    IHumanPlayer humanPlayer1 = new HumanPlayer("1");
                    IHumanPlayer humanPlayer2 = new HumanPlayer("2");
                    IGame game = new Game(new Board(), humanPlayer1, humanPlayer2);
                    IStrategy strategy1 = new HumanConsoleStrategy(game, humanPlayer1, m_gameConsole);
                    IStrategy strategy2 = new HumanConsoleStrategy(game, humanPlayer2, m_gameConsole);
                    m_gameContainer = new GameContainer(game, strategy1, strategy2, m_notificationService);                    
                    break;
                case UserChoice.PLAYCOMPUTER:
                    IHumanPlayer humanPlayer = new HumanPlayer("1");
                    IComputerPlayer computerplayer = new ComputerPlayer();

                    game = new Game(new Board(), humanPlayer, computerplayer);
                    strategy1 = new HumanConsoleStrategy(game, humanPlayer, m_gameConsole);
                    strategy2 = new ComputerStandardStrategy(game, computerplayer, humanPlayer);
                    m_gameContainer = new GameContainer(game, strategy1, strategy2, m_notificationService);
                    break;
                default:
                    throw new ArgumentException(String.Format("Unexpected userChoice {0}", userChoice));
            }

            m_boardViewer = new ConsoleBoardViewer(m_gameContainer.GetGame(), m_gameConsole);
            m_gameContainer = Wrapper<IGameContainer>.Wrap<IGameContainer>(m_gameContainer, m_gameContainer, m_logger);
            
            return true;
        }
        public void Run()
        {
            m_gameConsole.WriteLine("Starting a game of 'Four in a Line'.");
            m_boardViewer.DisplayBoard();

            do
            {
                m_gameContainer.NextStep();
                m_boardViewer.DisplayBoard();
            } while (m_gameContainer.GetGame().Status == BoardStatus.Active);
            m_gameConsole.WriteLine("Game completed.");

            PrintFinalStatus();
        }
        #endregion

        private void PrintWelcome()
        {
            m_gameConsole.WriteLine("Welcome to Four in a Line!");
        }
        private void PrintMenu()
        {
            m_gameConsole.WriteLine((int)UserChoice.QUIT + ". Exit");
            m_gameConsole.WriteLine((int)UserChoice.PLAY + ". Play against a friend");
            m_gameConsole.WriteLine((int)UserChoice.PLAYCOMPUTER + ". Play against the computer");
            m_gameConsole.WriteLine("Please choose an option:");
        }
        private UserChoice GetUserChoice()
        {
            bool correctChoice;
            int userChoice;
            do
            {
                PrintMenu();
                string userChoiceAsString = m_gameConsole.ReadLine();
                correctChoice = Int32.TryParse(userChoiceAsString, out userChoice);
                correctChoice = correctChoice && userChoice >= 0 && userChoice <= 2;
                if (!correctChoice)
                    m_gameConsole.WriteLine("Input incorrect! Please try again.");
            } while (!correctChoice);
            return (UserChoice)userChoice;
        }
        private void PrintFinalStatus()
        {
            switch (m_gameContainer.GetGame().Status)
            {
                case BoardStatus.Finished:
                    m_gameConsole.WriteLine("Game has ended! Player '{0}' won!", m_gameContainer.GetActiveStrategy().Player.Name);
                    break;
                case BoardStatus.Full:
                    m_gameConsole.WriteLine("Board is full! game has ended with a tie!");
                    break;
            }            
        }
    }
}