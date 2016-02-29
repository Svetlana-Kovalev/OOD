using System;
using FourInLineConsole.DataTypes;
using FourInLineConsole.Interfaces.Player;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void ComputerPlayerDefaults()
        {
            IComputerPlayer computerPlayer = new ComputerPlayer();
            Assert.That(computerPlayer, Is.AssignableTo<IPlayer>());
            Assert.That(computerPlayer.Name, Is.EqualTo("Computer"));
        }

        [Test]
        public void HumanPlayerDefaults()
        {
            IHumanPlayer humanPlayer = new HumanPlayer("player1");
            Assert.That(humanPlayer, Is.AssignableTo<IPlayer>());
            Assert.That(humanPlayer.Name, Is.EqualTo("player1"));
            Assert.That(humanPlayer.Email, Is.Null);
        }
    }
}