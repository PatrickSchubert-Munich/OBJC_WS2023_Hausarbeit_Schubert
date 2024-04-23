using System.Configuration;
using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Reading paths in App.config file
    /// </summary>
    public class ReadConfiguration : IReadPath
    {
        public string ReadFilePath(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
