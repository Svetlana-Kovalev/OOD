using System;
using System.ComponentModel.Design.Serialization;
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

        [Test]
        public void Init_Console0_False()
        {
            var mock = new Mock<IGameConsole>();
            mock.Setup(f => f.ReadLine()).Returns("0");
            var logMock = new Mock<ILogger>();
            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(f => f.Create()).Returns(logMock.Object);

            INotificationService notificationService = new NotificationService(mock.Object);
            var gameManager = new ConsoleGameManager(mockLoggerFactory.Object, notificationService, mock.Object);
            bool result = gameManager.Init();

            Assert.That(result, Is.False);
            mock.Verify(f => f.WriteLine("Welcome to Four in a Line!"), Times.Once);
            mock.Verify(f => f.WriteLine("0. Exit"), Times.Once);
            mock.Verify(f => f.WriteLine("1. Play against a friend"), Times.Once);
            mock.Verify(f => f.WriteLine("2. Play against the computer"), Times.Once);
            mock.Verify(f => f.WriteLine("Please choose an option:"), Times.Once);
        }

        [Test]
        public void Init_Console1_True()
        {
            var mock = new Mock<IGameConsole>();
            mock.Setup(f => f.ReadLine()).Returns("1");
            var logMock = new Mock<ILogger>();
            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(f => f.Create()).Returns(logMock.Object);

            INotificationService notificationService = new NotificationService(mock.Object);
            var gameManager = new ConsoleGameManager(mockLoggerFactory.Object, notificationService, mock.Object);
            bool result = gameManager.Init();

            Assert.That(result, Is.True);
        }

        [Test]
        public void Init_Console2_True()
        {
            var mock = new Mock<IGameConsole>();
            mock.Setup(f => f.ReadLine()).Returns("1");
            var logMock = new Mock<ILogger>();
            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(f => f.Create()).Returns(logMock.Object);

            INotificationService notificationService = new NotificationService(mock.Object);
            var gameManager = new ConsoleGameManager(mockLoggerFactory.Object, notificationService, mock.Object);
            bool result = gameManager.Init();

            Assert.That(result, Is.True);
        }
    }
}