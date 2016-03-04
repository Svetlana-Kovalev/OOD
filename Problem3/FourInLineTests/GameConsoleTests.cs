using FourInLineConsole.Infra;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class GameConsoleTests
    {
        [Test]
        public void Create()
        {
            GameConsole console = new GameConsole();
            console.WriteLine("", null);
            console.WriteLine("");
            console.WriteLine();
            console.Write("");
            Assert.Pass();
        }
    }
}