using FourInLineConsole.Infra;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class InfrastructureTests
    {
        [Test]
        public void Test()
        {
            Infrastructure infrastructure = new Infrastructure();
            Assert.That(infrastructure.AssemblyDirectory, Is.Not.Null);
        }
    }
}