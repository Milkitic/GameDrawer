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

        public string Name //view property
        {
            get => IoPath.GetFileName(Path);
            set => SuspendedName = value;
        }

        public string MetaDirectory => Path + ".meta";
        public override string DescriptionPath => IoPath.Combine(MetaDirectory, "description.txt");
        public override string IconPath => IoPath.Combine(MetaDirectory, "icon.png"); //view property
        public string ScreenShotDirectory => IoPath.Combine(MetaDirectory, "screenshots");

        public void InitDescription()
        {
            File.WriteAllText(DescriptionPath, DefaultDescription);
        }

        public override void CommitChanges()
        {
            base.CommitChanges();
            if (SuspendedName != NameWithoutExtension && SuspendedName != null)
            {
                var chars = IoPath.GetInvalidFileNameChars();
                if (SuspendedName.Any(k => chars.Contains(k)))
                {
                    throw new InvalidOperationException("文件名非法。");
                }

                File.Move(Path, IoPath.Combine(new FileInfo(Path).Directory.FullName, SuspendedName));
                OnPropertyChanged($"Path");
                OnPropertyChanged($"Identity");
                OnPropertyChanged($"Name");
                OnPropertyChanged($"NameWithoutExtension");
                OnPropertyChanged($"IconPath");
                OnPropertyChanged($"DescriptionPath");
                OnPropertyChanged($"RomDirectoryPath");
                OnPropertyChanged($"EmulatorDirectoryPath");
            }
        }

        public static string DefaultDescription => "暂无游戏介绍";
    }
}
