using System;
using FourInLineConsole.DataTypes;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Infra;
using FourInLineConsole.Interfaces.Player;
using Moq;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class HumanStrategyTests
    {
        [Test]
        public void Create_Defaults()
        {
            var mock = new Mock<IGameConsole>();
            IBoard board = new Board();
            IHumanPlayer humanPlayer = new HumanPlayer("1");
            IPlayer otherPlayer = new HumanPlayer("other");
            IGame game = new Game(board, humanPlayer, otherPlayer);
            HumanConsoleStrategy strategy = new HumanConsoleStrategy(game, humanPlayer, mock.Object);

            Assert.That(strategy.Player, Is.SameAs(humanPlayer));
            Assert.That(strategy.Game, Is.SameAs(game));
        }

        [Test]
        public void NextStep_CheckConsole()
        {
            IHumanPlayer humanPlayer = new HumanPlayer("1");
            var mock = new Mock<IGameConsole>();
            mock.Setup(f => f.ReadLine()).Returns("0");
            
            IBoard board = new Board();            
            IPlayer otherPlayer = new HumanPlayer("other");
            IGame game = new Game(board, humanPlayer, otherPlayer);
            HumanConsoleStrategy strategy = new HumanConsoleStrategy(game, humanPlayer, mock.Object);

            strategy.MakeNextStep();

            mock.Verify(f => f.Write("Player {0}, choose a column: ", humanPlayer.Name), Times.Once);
        }

        [Test]
        public void NextIncorrectStep_Exception()
        {
            IHumanPlayer humanPlayer = new HumanPlayer("1");
            var mock = new Mock<IGameConsole>();
            mock.Setup(f => f.ReadLine()).Returns("-1");

            IBoard board = new Board();
            IPlayer otherPlayer = new HumanPlayer("other");
            IGame game = new Game(board, humanPlayer, otherPlayer);
            HumanConsoleStrategy strategy = new HumanConsoleStrategy(game, humanPlayer, mock.Object);

            Assert.Throws<ArgumentException>(()=>strategy.MakeNextStep());            
        }
    }
}