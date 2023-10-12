using DouSlackingMonitor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tomlyn;
using Tomlyn.Model;

namespace DouSlackingMonitor.Libs
{
    public class ConfigLoader
    {
        public event EventHandler ConfigLoaded = delegate { };
        private readonly string configPath;
        public readonly Config config = new Config();
        public string lastError = "";

        public ConfigLoader(string configPath)
        {
            this.configPath = configPath;
        }

        public void Load()
        {
            // check if config file exists
            if (!File.Exists(configPath))
            {
                lastError = "Config file not found!";
                return;
            }

            // read config file
            try
            {
                string tomlString = File.ReadAllText(configPath);
                TomlTable toml = Toml.ToModel(tomlString);
                config.Secret = (string)toml["secret"];
                config.ApiEntrypoint = (string)toml["entrypoint"];
                config.ProcessWhitelist = (TomlArray)toml["whitelist"];
            } catch (Exception ex)
            {
                lastError = ex.Message;
                ConfigLoaded?.Invoke(this, null);
                return;
            }
            lastError = "";
            ConfigLoaded?.Invoke(this, null);
        }

    }
}
