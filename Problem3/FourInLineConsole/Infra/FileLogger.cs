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
}