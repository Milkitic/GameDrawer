﻿using Milkitic.WpfApi;
using RomExplorer.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using IoPath = System.IO.Path;

namespace RomExplorer.Model
{
    public sealed class ConsoleMachine : FileModelBase
    {
        protected override string SuspendedDescription { get; set; } = "暂无主机介绍";
        public override string IconPath //view property
            => File.Exists(InnerIconPath)
                ? InnerIconPath
                : IoPath.Combine(Config.IconCacheDirectory, $"Folder.png");

        public ConsoleMachine(string directoryPath)
        {
            Path = directoryPath;
            Games.CollectionChanged += Games_CollectionChanged;
        }

        private void Games_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    var game = (Game)newItem;
                    game.Committed += Game_Committed;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var newItem in e.OldItems)
                {
                    var game = (Game)newItem;
                    game.Committed -= Game_Committed;
                }
            }
        }

        private void Game_Committed(object sender, EventArgs e)
        {
            //OnCommitted(sender, e);
        }

        public string RomDirectoryPath => IoPath.Combine(Path, "Apps");
        public string EmulatorDirectoryPath => IoPath.Combine(Path, "HostApp");

        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();
        public ObservableCollection<Game> VisibleGames { get; set; }

        public void InitDescription()
        {
            File.WriteAllText(DescriptionPath, DefaultDescription);
        }

        public override void CommitChanges()
        {
            if (SuspendedName != NameWithoutExtension)
            {
                var validName = ValidateFileName(SuspendedName);

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
