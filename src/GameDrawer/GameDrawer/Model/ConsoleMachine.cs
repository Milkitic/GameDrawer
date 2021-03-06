﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using GameDrawer.IO;
using Newtonsoft.Json;
using IoPath = System.IO.Path;

namespace GameDrawer.Model
{
    public sealed class ConsoleMachine : FileModelBase
    {
        private ObservableCollection<Game> _searchedGames;
        private ObservableCollection<Game> _visibleGames;
        protected override string SuspendedDescription { get; set; } = "暂无主机介绍";
        public override string IconPath //view property
            => File.Exists(InnerIconPath)
                ? InnerIconPath
                : IoPath.Combine(Config.IconCacheDirectory, $"Folder.png");

        public ConsoleMachine(string directoryPath)
        {
            Path = directoryPath;
        }

        [JsonIgnore]
        public string RomDirectoryPath => IoPath.Combine(Path, "Apps");
        [JsonIgnore]
        public string EmulatorDirectoryPath => IoPath.Combine(Path, "HostApp");

        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();

        [JsonIgnore]
        public ObservableCollection<Game> SearchedGames
        {
            get => _searchedGames;
            set
            {
                _searchedGames = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public ObservableCollection<Game> VisibleGames
        {
            get => _visibleGames;
            set
            {
                _visibleGames = value;
                OnPropertyChanged();
            }
        }

        public void InitDescription()
        {
            File.WriteAllText(DescriptionPath, DefaultDescription);
        }

        public override void CommitChanges()
        {
            if (SuspendedName != NameWithoutExtension)
            {
                var validName = FileExtension.ValidateFileName(SuspendedName);

                var newPath = IoPath.Combine(new DirectoryInfo(Path).Parent.FullName, validName);
                var success = FileExtension.MoveFile(Path, newPath);
                Path = newPath;
                OnPropertyChanged($"Path");
                OnPropertyChanged($"Identity");
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

        public static string DefaultDescription => "暂无主机介绍";
    }
}
