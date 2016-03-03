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
    public class ConsoleBoardViewerTests
    {
        [Test]
        public void DisplayBoard_EmptyBoard()
        {
            var mock = new Mock<IGameConsole>();

            IBoard board = new Board(3, 2);
            IPlayer player1 = new HumanPlayer("1");
            IPlayer player2 = new HumanPlayer("2");
            IGame game = new Game(board, player1, player2);
  
            IBoardViewer boardViewer = new ConsoleBoardViewer(game, mock.Object);
            Assert.That(boardViewer, Is.Not.Null);

            boardViewer.DisplayBoard();
            mock.Verify(f => f.WriteLine("Printing board:"), Times.Once);
        }
    }
}