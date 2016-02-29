using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class ComputerPlayer : IComputerPlayer
    {
        public ComputerPlayer()
        {
            Name = "Computer";
        }
        #region IPlayer
        public string Name { get; private set; }
        #endregion
    }
}