using System;
using FourInLineConsole.Interfaces.Board;
using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class Board : IBoard
    {
        const int ROWS = 6;
        const int COLUMNS = 7;
        const int WIN = 4;
        private readonly IPlayer[][] m_board;

        public Board()
        {
            Rows = ROWS;
            Columns = COLUMNS;
            m_board = InitBoard();
            Clear();
        }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            m_board = InitBoard();
            Clear();         
        }

        private IPlayer[][] InitBoard()
        {            
            IPlayer[][] board = new IPlayer[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                board[i] = new IPlayer[Columns];
            }
            return board;
        }

        #region IBoard
        public IPlayer this[int row, int col]
        {
            get { return m_board[row][col]; }
            set { m_board[row][col] = value; }
        }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public void Clear()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                    m_board[i][j] = null;
            Status = BoardStatus.Active;
        }
        public void PlaceDisk(IPlayer player, int columnIndex)
        {
            if (columnIndex<0)
                throw new ArgumentException();

            for (int i = Rows - 1; i >= 0; i--)
            {
                if (m_board[i][columnIndex] == null)
                {
                    m_board[i][columnIndex] = player;
                    if (CheckWinningDisk(i, columnIndex))
                        Status = BoardStatus.Finished;
                    else if (BoardIsFull())
                        Status = BoardStatus.Full;
                    break;
                }
            }
        }
        public BoardStatus Status { get; private set; }
        public bool IsColumnFull(int columnIndex)
        {
            for (int i = 0; i < Rows; i++)
            {
                if (m_board[i][columnIndex] == null)
                    return false;
            }
            return true;
        }
        public bool BoardIsFull()
        {
            // it's enough to check top row
            for (int columnIndex = 0; columnIndex < Columns; columnIndex++)
                if (m_board[0][columnIndex] == null) return false;
            return true;
        }
        public int FirstEmptyRow(int columnIndex)
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                if (m_board[i][columnIndex] == null) return i;
            }
            return -1;
        }
        public bool IsWin(int rowIndex, int columnIndex, IPlayer player)
        {
            if (m_board[rowIndex][columnIndex] != null)
                throw new ArgumentException();
            m_board[rowIndex][columnIndex] = player;
            bool isWin = CheckWinningDisk(rowIndex, columnIndex);
            m_board[rowIndex][columnIndex] = null;
            return isWin;
        }
        #endregion

        // is the disc at board[rowIndex][colIndex] winning?
        Boolean CheckWinningDisk(int rowIndex, int columnIndex)
        {
            IPlayer c = m_board[rowIndex][columnIndex];
            int count = 1;

            // horizontal right
            for (int i = columnIndex + 1; i < Columns; i++)
            {
                if (m_board[rowIndex][i] == c)
                    count++;
                else break;
            }
            if (count >= WIN) return true; // won horizontally
            // keep counting horizontal left
            for (int i = columnIndex - 1; i >= 0; i--)
            {
                if (m_board[rowIndex][i] == c)
                    count++;
                else break;
            }
            if (count >= WIN) return true; // won horizontally

            count = 1;
            // vertical down
            for (int i = rowIndex + 1; i < Rows; i++)
            {
                if (m_board[i][columnIndex] == c)
                    count++;
                else break;
            }
            if (count >= WIN) return true; // won vertical
            // keep counting vertical up
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                if (m_board[i][columnIndex] == c)
                    count++;
                else
                    break;
            }
            if (count >= WIN) return true; // won vertical

            // first diagonal:  /
            count = 1;
            // up
            int kol = columnIndex + 1;
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                if (kol >= Columns) break; // we reached the end of the board right side
                if (m_board[i][kol] == c)
                    count++;
                else
                    break;
                kol++;
            }
            if (count >= WIN) return true;
            // keep counting down
            kol = columnIndex - 1;
            for (int i = rowIndex + 1; i < Rows; i++)
            {
                if (kol < 0) break; // we reached the end of the board left side
                if (m_board[i][kol] == c)
                    count++;
                else
                    break;
                kol--;
            }
            if (count >= WIN) return true; // won diagonal "/"

            // second diagonal : \
            count = 1;
            // up
            kol = columnIndex - 1;
            for (int i = rowIndex - 1; i >= 0; i--)
            {
                if (kol < 0) break; // we reached the end of the board left side
                if (m_board[i][kol] == c)
                    count++;
                else
                    break;
                kol--;
            }
            if (count >= WIN) return true; // won diagonal "\"
            // keep counting down
            kol = columnIndex + 1;
            for (int i = rowIndex + 1; i < Rows; i++)
            {
                if (kol >= Columns) break; // we reached the end of the board right side
                if (m_board[i][kol] == c)
                    count++;
                else
                    break;
                kol++;
            }
            if (count >= WIN) return true; // won diagonal "\"

            return false;
        }
    }
}