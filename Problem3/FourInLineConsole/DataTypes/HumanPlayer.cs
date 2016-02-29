using FourInLineConsole.Interfaces.Player;

namespace FourInLineConsole.DataTypes
{
    public class HumanPlayer : IHumanPlayer
    {
        public HumanPlayer(string name)
        {
            Name = name;
        }

        #region IPlayer
        public string Name { get; private set; }
        #endregion

        #region Implementation of IHumanPlayer
        public string Email { get; private set; }
        #endregion
    }
}