using FourInLineConsole.DataTypes;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class ComputerStandardStrategyTests
    {
        [Test]
        public void ComputerStandardStrategy_FirstStep()
        {
            IBoard board = new Board();
            IPlayer humanPlayer = new HumanPlayer("1");
            IComputerPlayer computerPlayer = new ComputerPlayer();
            IGame game = new Game(board, humanPlayer, computerPlayer);

            Assert.That(board[0, 0], Is.Null);

            IStrategy computerStrategy = new ComputerStandardStrategy(game, computerPlayer, humanPlayer);
            computerStrategy.MakeNextStep();

            Assert.That(computerStrategy.Player, Is.SameAs(computerPlayer));
            Assert.That(board[board.Rows-1, 0].Name, Is.EqualTo(computerPlayer.Name));
        }

        [Test]
        public void ComputerStandardStrategy_VerticalWin()
        {
            IBoard board = new Board();
            IPlayer humanPlayer = new HumanPlayer("1");
            IComputerPlayer computerPlayer = new ComputerPlayer();
            IGame game = new Game(board, humanPlayer, computerPlayer);

            board[board.Rows - 1, 1] = computerPlayer;
            board[board.Rows - 2, 1] = computerPlayer;
            board[board.Rows - 3, 1] = computerPlayer;
            Assert.That(board[board.Rows - 4, 1], Is.Null);

            IStrategy computerStrategy = new ComputerStandardStrategy(game, computerPlayer, humanPlayer);
            computerStrategy.MakeNextStep();

            Assert.That(board[board.Rows - 4, 1].Name, Is.EqualTo(computerPlayer.Name));
        }

        [Test]
        public void ComputerStandardStrategy_HorizontalWin()
        {
            IBoard board = new Board();
            IPlayer humanPlayer = new HumanPlayer("1");
            IComputerPlayer computerPlayer = new ComputerPlayer();
            IGame game = new Game(board, humanPlayer, computerPlayer);

            board[board.Rows - 1, 0] = computerPlayer;
            board[board.Rows - 1, 1] = computerPlayer;
            board[board.Rows - 1, 2] = computerPlayer;
            Assert.That(board[board.Rows - 1, 3], Is.Null);

            IStrategy computerStrategy = new ComputerStandardStrategy(game, computerPlayer, humanPlayer);
            computerStrategy.MakeNextStep();

            Assert.That(board[board.Rows - 1, 3].Name, Is.EqualTo(computerPlayer.Name));
        }

        [Test]
        public void ComputerStandardStrategy_CloseOtherUserToWin()
        {
            IBoard board = new Board();
            IPlayer humanPlayer = new HumanPlayer("1");
            IComputerPlayer computerPlayer = new ComputerPlayer();
            IGame game = new Game(board, humanPlayer, computerPlayer);

            board[board.Rows - 1, 1] = humanPlayer;
            board[board.Rows - 2, 1] = humanPlayer;
            board[board.Rows - 3, 1] = humanPlayer;
            Assert.That(board[board.Rows - 4, 1], Is.Null);

            IStrategy computerStrategy = new ComputerStandardStrategy(game, computerPlayer, humanPlayer);
            computerStrategy.MakeNextStep();

            Assert.That(board[board.Rows - 4, 1].Name, Is.EqualTo(computerPlayer.Name));
        }
    }
}