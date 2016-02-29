using FourInLineConsole.DataTypes;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;
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
            Assert.That(game.Player1, Is.SameAs(player1));
            Assert.That(game.Player2, Is.SameAs(player2));
        }
    }
}