using FourInLineConsole.DataTypes;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;
using Moq;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Create()
        {
            IBoard board = new Board();
            IPlayer player1 = new HumanPlayer("1");
            IPlayer player2 = new HumanPlayer("2");
            IGame game = new Game(board, player1, player2);

            Assert.That(game.Board, Is.SameAs(board));
            Assert.That(game.Board.Status, Is.EqualTo(BoardStatus.Active));
            Assert.That(game.Player1, Is.SameAs(player1));
            Assert.That(game.Player2, Is.SameAs(player2));
        }

        [Test]
        public void BoardStatusTakeValueFromBoard()
        {
            var mock = new Mock<IBoard>();
            IPlayer player1 = new HumanPlayer("1");
            IPlayer player2 = new HumanPlayer("2");
            IGame game = new Game(mock.Object, player1, player2);

            Assert.That(game.Status, Is.Not.Null);
            mock.VerifyGet(foo => foo.Status);
        }
    }
}