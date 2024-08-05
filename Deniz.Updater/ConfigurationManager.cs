using Microsoft.Extensions.Configuration;

namespace Deniz.Updater
{
    public class ConfigurationManager
    {
        private static ConfigurationManager? _instance;
        public static ConfigurationManager Instance => _instance ??= new ConfigurationManager();

        private readonly IConfigurationRoot _configuration;

        private ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Application.StartupPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            _configuration = builder.Build();
        }
        public string GetSetting(string key)
        {
            return _configuration[key];
        }

        public string TargetPath => Path.Combine(Application.StartupPath, GetSetting("TargetPath"));

        public string[] ExcludeFolders => GetSetting("ExcludeFolders").Split(",");
    }
}
