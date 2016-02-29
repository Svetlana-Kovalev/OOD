using System;
using FourInLineConsole.DataTypes;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void InitializationBoard_Defaults()
        {
            IBoard board = new Board();
            Assert.That(board.Columns, Is.EqualTo(7));
            Assert.That(board.Rows, Is.EqualTo(6));
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));
        }

        [Test]
        public void FirstEmptyRowTests()
        {
            const int INITIAL_FIRST_EMPTY_ROW = 5;
            IBoard board = new Board();
            Assert.That(board.FirstEmptyRow(0), Is.EqualTo(INITIAL_FIRST_EMPTY_ROW));

            IPlayer player = new ComputerPlayer();

            for (int i = 0; i < 6; i++)
            {
                board.PlaceDisk(player, 0);
                Assert.That(board.FirstEmptyRow(0), Is.EqualTo(4-i));
            }
        }

        [Test]
        public void IsColumnFullTests()
        {
            IBoard board = new Board();
            Assert.That(board.IsColumnFull(0), Is.False);

            IPlayer player = new ComputerPlayer();

            for (int i = 0; i < 5; i++)
            {
                board.PlaceDisk(player, 0);
                Assert.That(board.IsColumnFull(0), Is.False);
            }

            board.PlaceDisk(player, 0);
            Assert.That(board.IsColumnFull(0), Is.True);
        }

        [Test]
        public void ClearBoardTests()
        {
            const int INITIAL_FIRST_EMPTY_ROW = 5;
            IBoard board = new Board();
            Assert.That(board.FirstEmptyRow(0), Is.EqualTo(INITIAL_FIRST_EMPTY_ROW));
            IPlayer player = new ComputerPlayer();
            board.PlaceDisk(player, 0);

            Assert.That(board.FirstEmptyRow(0), Is.EqualTo(INITIAL_FIRST_EMPTY_ROW-1));
            board.Clear();
            Assert.That(board.FirstEmptyRow(0), Is.EqualTo(INITIAL_FIRST_EMPTY_ROW));
        }

        [Test]
        public void CheckFinishedVerticalCase()
        {
            const int WIN_COUNT = 4;
            IBoard board = new Board();
            IPlayer player = new ComputerPlayer();
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));

            for (int i = 0; i < WIN_COUNT-1; i++)
            {
                board.PlaceDisk(player, 0);
                Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));
            }
            
            board.PlaceDisk(player, 0);
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Finished));
        }

        [Test]
        public void CheckFinishedHorizontalCase()
        {
            const int WIN_COUNT = 4;
            IBoard board = new Board();
            IPlayer player = new ComputerPlayer();
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));

            for (int i = 0; i < WIN_COUNT - 1; i++)
            {
                board.PlaceDisk(player, i);
                Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));
            }

            board.PlaceDisk(player, WIN_COUNT-1);
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Finished));
        }

        [Test]
        public void CheckFinishedDiagonalCase()
        {
            IBoard board = new Board();
            IPlayer playerWin = new ComputerPlayer();
            IPlayer playerOther = new ComputerPlayer();
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));

            board.PlaceDisk(playerWin, 0);

            board.PlaceDisk(playerOther, 1); 
            board.PlaceDisk(playerWin, 1);
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));
            
            board.PlaceDisk(playerOther, 2); 
            board.PlaceDisk(playerOther, 2);
            board.PlaceDisk(playerWin, 2);
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));

            board.PlaceDisk(playerOther, 3);
            board.PlaceDisk(playerOther, 3);
            board.PlaceDisk(playerOther, 3);
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Active));
            board.PlaceDisk(playerWin, 3);
            Assert.That(board.Status, Is.EqualTo(BoardStatus.Finished));
        }

        [Test]
        public void IsWinTests()
        {
            int WIN_COUNT = 4;
            int ROW_COUNT = 6;
            IBoard board = new Board();
            IPlayer playerWin = new ComputerPlayer();
            IPlayer playerOther = new ComputerPlayer();

            board.PlaceDisk(playerWin, 0);
            board.PlaceDisk(playerWin, 0);
            board.PlaceDisk(playerWin, 0);
            Assert.That(board.IsWin(ROW_COUNT - WIN_COUNT, 0, playerWin), Is.True);
            Assert.That(board.IsWin(ROW_COUNT - WIN_COUNT, 0, playerOther), Is.False);
        }
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IsWinTests_IncorrectArgument()
        {
            int WIN_COUNT = 4;
            int ROW_COUNT = 6;
            IBoard board = new Board();
            IPlayer playerWin = new ComputerPlayer();

            board.PlaceDisk(playerWin, 0);
            board.PlaceDisk(playerWin, 0);
            board.PlaceDisk(playerWin, 0);
            Assert.That(board.IsWin(ROW_COUNT - WIN_COUNT+1 , 0, playerWin), Is.True);
        }

        [Test]
        public void BoardIsFull()
        {
            IBoard board = new Board(3, 2);
            Assert.That(board.Rows, Is.EqualTo(3));
            Assert.That(board.Columns, Is.EqualTo(2));
            Assert.That(board.BoardIsFull(), Is.False);

            IPlayer player1 = new HumanPlayer("1");
            board.PlaceDisk(player1, 0);
            board.PlaceDisk(player1, 0);
            board.PlaceDisk(player1, 0);

            board.PlaceDisk(player1, 1);
            board.PlaceDisk(player1, 1);
            Assert.That(board.BoardIsFull(), Is.False);

            board.PlaceDisk(player1, 1);
            Assert.That(board.BoardIsFull(), Is.True);
        }
    }
}
