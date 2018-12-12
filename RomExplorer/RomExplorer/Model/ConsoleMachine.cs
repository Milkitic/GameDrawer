using Milkitic.WpfApi;
using RomExplorer.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using IoPath = System.IO.Path;

namespace RomExplorer.Model
{
    public class ConsoleMachine : FileModelBase
    {
        protected override string SuspendedDescription { get; set; } = "暂无主机介绍";

        public ConsoleMachine(string directoryPath)
        {
            Path = directoryPath;
        }

        public string RomDirectoryPath => IoPath.Combine(Path, "Rom");
        public string EmulatorDirectoryPath => IoPath.Combine(Path, "Emu");

        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();

        public void InitDescription()
        {
            File.WriteAllText(DescriptionPath, DefaultDescription);
        }

        public override void CommitChanges()
        {
            base.CommitChanges();

            if (SuspendedName != NameWithoutExtension)
            {
                var chars = IoPath.GetInvalidPathChars();
                if (SuspendedName.Any(k => chars.Contains(k)))
                {
                    throw new InvalidOperationException("文件名非法。");
                }

                var newPath = IoPath.Combine(new DirectoryInfo(Path).Parent.FullName, SuspendedName);
                Directory.Move(Path, newPath);
                Path = newPath;
                OnPropertyChanged($"Path");
                OnPropertyChanged($"Identity");
                OnPropertyChanged($"NameWithoutExtension");
                OnPropertyChanged($"IconPath");
                OnPropertyChanged($"DescriptionPath");
                OnPropertyChanged($"RomDirectoryPath");
                OnPropertyChanged($"EmulatorDirectoryPath");
            }
        }

        public static string DefaultDescription => "这个文件暂时还没有介绍，请帮忙一同编辑吧！";
    }
}
