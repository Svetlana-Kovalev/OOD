using FourInLineConsole.Infra;
using FourInLineConsole.Interfaces.Infra;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class FileLoggerFactoryTests
    {
        [Test]
        public void Create()
        {
            IGameInfrastructure infrastructure = new Infrastructure();
            FileLoggerFactory factory = new FileLoggerFactory(infrastructure);
            ILogger logger = factory.Create();

            Assert.That(logger, Is.InstanceOf<FileLogger>());
        }
    }
}