using FourInLineConsole.DataTypes;
using FourInLineConsole.Infra;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;
using Moq;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class GameContainerTests
    {
        [Test]
        public void CreateGameContainer_Defaults()
        {
            IBoard board = new Board();

            IHumanPlayer player1 = new HumanPlayer("1");
            IHumanPlayer player2 = new HumanPlayer("2");

            IGame game = new Game(board, player1, player2);
            IStrategy strategy1 = new HumanConsoleStrategy(game, player1, new GameConsole());
            IStrategy strategy2 = new HumanConsoleStrategy(game, player2, new GameConsole());
            IGameContainer gameContainer = new GameContainer(game, strategy1, strategy2, null);

            Assert.That(gameContainer.GetGame(), Is.SameAs(game));
            Assert.That(gameContainer.GetActiveStrategy(), Is.SameAs(strategy1));
            Assert.That(gameContainer.GetLastStep(), Is.Null);
            Assert.That(gameContainer.GetStrategyPlayer1(), Is.SameAs(strategy1));
            Assert.That(gameContainer.GetStrategyPlayer2(), Is.SameAs(strategy2));
        }

        [Test]
        public void CreateGameContainer_NextStep()
        {
            IBoard board = new Board();

            IHumanPlayer player1 = new HumanPlayer("1");
            IHumanPlayer player2 = new HumanPlayer("1");

            IGame game = new Game(board, player1, player2);

            var mock1 = new Mock<IStrategy>();
            var mock2 = new Mock<IStrategy>();

            IStrategy strategy1 = mock1.Object; //new HumanConsoleStrategy(game, player1);
            IStrategy strategy2 = mock2.Object; //new HumanConsoleStrategy(game, player2);
            IGameContainer gameContainer = new GameContainer(game, strategy1, strategy2, null);

            gameContainer.NextStep();

            Assert.That(gameContainer.GetGame(), Is.SameAs(game));
            Assert.That(gameContainer.GetActiveStrategy(), Is.SameAs(strategy2));
            Assert.That(gameContainer.GetLastStep(), Is.SameAs(strategy1));
            Assert.That(gameContainer.GetStrategyPlayer1(), Is.SameAs(strategy1));
            Assert.That(gameContainer.GetStrategyPlayer2(), Is.SameAs(strategy2));

            gameContainer.NextStep();

            Assert.That(gameContainer.GetGame(), Is.SameAs(game));
            Assert.That(gameContainer.GetActiveStrategy(), Is.SameAs(strategy1));
            Assert.That(gameContainer.GetLastStep(), Is.SameAs(strategy2));
            Assert.That(gameContainer.GetStrategyPlayer1(), Is.SameAs(strategy1));
            Assert.That(gameContainer.GetStrategyPlayer2(), Is.SameAs(strategy2));
        }
    }
}