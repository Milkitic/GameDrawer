using Milkitic.WpfApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace RomExplorer.Model
{
    public class Config
    {
        public Config()
        {
            if (!Directory.Exists(BackupDirectory))
                Directory.CreateDirectory(BackupDirectory);
            if (!Directory.Exists(GameDirectory))
                Directory.CreateDirectory(GameDirectory);
        }

        public string GameDirectory { get; set; } = Path.Combine(BaseDirectory, "Games");

        public List<GameConsoleConfig> GameConsoleConfigs { get; set; }
            = new List<GameConsoleConfig>();

        [JsonIgnore] public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;
        [JsonIgnore] public static string ConfigPath => Path.Combine(BaseDirectory, "config.json");
        [JsonIgnore] public static string BackupDirectory => Path.Combine(BaseDirectory, "MetaBackup");

        public void SaveConfig()
        {
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(this));
        }

        public static Config LoadOrCreateConfig()
        {
            try
            {
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigPath));
            }
            catch (Exception e)
            {
                return new Config();
            }
        }
    }

    public class GameConsoleConfig : ViewModelBase
    {
        public GameConsoleConfig(string identity)
        {
            Identity = identity;
        }

        public string Identity { get; set; }
        public string HostApplication { get; set; }
        public string StartupArguments { get; set; }

        public void CommitChanges()
        {
            var config = App.Config.GameConsoleConfigs.FirstOrDefault(k => k.Identity == Identity);
            if (config == null)
            {
                App.Config.GameConsoleConfigs.Add(this);
            }

            App.Config.SaveConfig();
        }
    }
}
