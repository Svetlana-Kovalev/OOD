using System;
using System.IO;
using FourInLineConsole.Interfaces;

namespace FourInLineConsole.DataTypes
{
    public class FileLogger : ILogger
    {
        private readonly string m_filePath;

        public FileLogger(string filePath)
        {
            m_filePath = filePath;
        }

        #region Implementation of ILogger
        public void Info(string format, params object[] parameters)
        {
            File.AppendAllText(m_filePath, String.Format(format, parameters)+Environment.NewLine);
        }
        #endregion
    }

    public class FileLoggerFactory : ILoggerFactory
    {
        private readonly IGameInfrastructure m_infrastructure;

        public FileLoggerFactory(IGameInfrastructure infrastructure)
        {
            m_infrastructure = infrastructure;
        }

        #region ILoggerFactory
        public ILogger Create()
        {
            ILogger logger = new FileLogger(Path.Combine(m_infrastructure.AssemblyDirectory, "FourInLine.log"));
            return logger;            
        }

        #endregion
    }
}