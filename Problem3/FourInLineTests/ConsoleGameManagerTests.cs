using System;
using FourInLineConsole.DataTypes;
using FourInLineConsole.Infra;
using FourInLineConsole.Interfaces;
using FourInLineConsole.Interfaces.Infra;
using Moq;
using NUnit.Framework;

namespace FourInLineTests
{
    [TestFixture]
    public class ConsoleGameManagerTests
    {
        [Test]
        public void Create_Defaults()
        {
            var mock = new Mock<IGameConsole>();
            var logMock = new Mock<ILogger>();
            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(f => f.Create()).Returns(logMock.Object);
            
            INotificationService notificationService = new NotificationService(mock.Object);
            var gameManager = new ConsoleGameManager(mockLoggerFactory.Object, notificationService, mock.Object);

            Assert.That(gameManager, Is.AssignableTo<IGameManager>());
            logMock.Verify(f => f.Info(It.Is<string>(str => str.StartsWith("created at {0}")), It.IsAny<DateTime>()), Times.Once);
        }
    }
}