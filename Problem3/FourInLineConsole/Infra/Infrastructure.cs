using System;
using System.IO;
using System.Reflection;
using FourInLineConsole.Interfaces;

namespace FourInLineConsole.Infra
{
    public class Infrastructure : IGameInfrastructure
    {
        public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}