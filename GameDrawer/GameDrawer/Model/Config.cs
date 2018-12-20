using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Milkitic.WpfApi;
using Newtonsoft.Json;

namespace GameDrawer.Model
{
    public class Config
    {
        public Config()
        {
            if (!Directory.Exists(BackupDirectory))
                Directory.CreateDirectory(BackupDirectory);
            if (!Directory.Exists(GameDirectory))
                Directory.CreateDirectory(GameDirectory);
            if (!Directory.Exists(CacheDirectory))
                Directory.CreateDirectory(CacheDirectory);
            if (!Directory.Exists(IconCacheDirectory))
                Directory.CreateDirectory(IconCacheDirectory);
        }

        public string GameDirectory { get; set; } = Path.Combine(BaseDirectory, "Games");

        public List<GameConsoleConfig> GameConsoleConfigs { get; set; }
            = new List<GameConsoleConfig>();

        [JsonIgnore] public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;
        [JsonIgnore] public static string ConfigPath => Path.Combine(BaseDirectory, "config.json");
        [JsonIgnore] public static string CacheDirectory => Path.Combine(BaseDirectory, "Cache");
        [JsonIgnore] public static string BackupDirectory => Path.Combine(BaseDirectory, "MetaBackup");
        [JsonIgnore] public static string IconCacheDirectory => Path.Combine(CacheDirectory, "Icon");

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

        public bool UseWhiteList { get; set; }
        public string ExtensionFilter { get; set; }
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
