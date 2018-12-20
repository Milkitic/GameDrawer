using RomExplorer.IO;
using System;
using System.IO;
using System.Linq;
using IoPath = System.IO.Path;

namespace RomExplorer.Model
{
    public class Game : FileModelBase
    {
        protected override string SuspendedDescription { get; set; } = "暂无游戏介绍";

        public Game(string filePath)
        {
            Path = filePath;
        }

        public override string NameWithoutExtension //view property
        {
            get => IoPath.GetFileNameWithoutExtension(Path);
            set => SuspendedName = value;
        }

        public string Name => IoPath.GetFileName(Path); //view property
        public string Extension => IoPath.GetExtension(Path)?.Trim('.').ToLower(); //view property
        public long Length { get; set; }

        public string MetaDirectory => Path + ".meta";
        public override string DescriptionPath => IoPath.Combine(MetaDirectory, "description.txt");
        protected override string InnerIconPath => IoPath.Combine(MetaDirectory, "icon.png");
        public override string IconPath //view property
        {
            get
            {
                if (File.Exists(InnerIconPath))
                    return InnerIconPath;
                else
                {
                    if (Extension == "exe" || Extension == "lnk")
                    {
                        var iconName = $"{Identity.Replace("/", "").Replace("\\", "").Replace(" ", "")}.png";
                        return IoPath.Combine(Config.IconCacheDirectory, iconName);
                    }
                    else
                        return IoPath.Combine(Config.IconCacheDirectory, $"{Extension}.png");

                }
            }
        }

        public string ScreenShotDirectory => IoPath.Combine(MetaDirectory, "screenshots");

        public void InitDescription()
        {
            File.WriteAllText(DescriptionPath, DefaultDescription);
        }

        public override void CommitChanges()
        {
            if (SuspendedName != NameWithoutExtension && SuspendedName != null)
            {
                var validName = FileExtension.ValidateFileName(SuspendedName);

                var fileInfo = new FileInfo(Path);
                var newPath = IoPath.Combine(fileInfo.Directory.FullName, validName + fileInfo.Extension);
                FileExtension.MoveFile(Path, newPath);
                var dirInfo = new FileInfo(MetaDirectory);
                FileExtension.MoveFile(MetaDirectory, IoPath.Combine(dirInfo.Directory.FullName, validName + fileInfo.Extension + ".meta"));
                Path = newPath;
                OnPropertyChanged($"Path");
                OnPropertyChanged($"Identity");
                OnPropertyChanged($"Name");
                OnPropertyChanged($"NameWithoutExtension");
                OnPropertyChanged($"IconPath");
                OnPropertyChanged($"DescriptionPath");
                OnPropertyChanged($"RomDirectoryPath");
                OnPropertyChanged($"EmulatorDirectoryPath");
            }

            base.CommitChanges();

            //App.GameListLoader.SaveCache();
            //OnCommitted(this, new DataEventArgs());
        }

        public static string DefaultDescription => "暂无游戏介绍";
    }
}
