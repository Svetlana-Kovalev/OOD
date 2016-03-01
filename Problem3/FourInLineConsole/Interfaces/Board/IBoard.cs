using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.Interfaces.Board
{
    public enum BoardStatus
    {
        Active,
        Finished,
        Full
    };
    public interface IBoard
    {
        IPlayer this[int row, int col] { get; set; }       

        int Rows { get; }
        int Columns { get; }
        
        void Clear();
        void PlaceDisk(IPlayer player, int column);

        BoardStatus Status { get; }
        bool IsColumnFull(int columnIndex);
        bool BoardIsFull();
        int FirstEmptyRow(int columnIndex);
        bool IsWin(int rowIndex, int columnIndex, IPlayer player);
    }
}
