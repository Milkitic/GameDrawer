using System;
using System.IO;
using GameDrawer.IO;
using Milkitic.WpfApi;
using Newtonsoft.Json;
using IoPath = System.IO.Path;

namespace GameDrawer.Model
{
    public class FileModelBase : ViewModelBase
    {
        protected virtual string InnerIconPath => IoPath.Combine(Path, "icon.png");
        protected virtual string SuspendedName { get; set; }
        protected virtual string SuspendedDescription { get; set; } = "暂无介绍";

        public virtual string NameWithoutExtension //view property
        {
            get => IoPath.GetFileNameWithoutExtension(Path);
            set => SuspendedName = value;
        }

        public string Description
        {
            get => GetDescriptionFromFile();
            set => SuspendedDescription = value;
        }

        private string GetDescriptionFromFile()
        {
            return File.ReadAllText(DescriptionPath);
        }

        private bool _descriptionEditable;

        [JsonIgnore]
        public bool DescriptionEditable
        {
            get => _descriptionEditable;
            set
            {
                _descriptionEditable = value;
                OnPropertyChanged();
            }
        }

        private bool _nameEditable;
        private string _path;

        [JsonIgnore]
        public bool NameEditable
        {
            get => _nameEditable;
            set
            {
                _nameEditable = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public string Identity => IdentityHelper.GetRelativePath(Path);

        [JsonIgnore]
        public virtual string IconPath //view property
            => File.Exists(InnerIconPath)
                ? InnerIconPath
                : IoPath.Combine(Config.IconCacheDirectory, $"{IoPath.GetExtension(Path)}.png");

        [JsonIgnore]
        public virtual string DescriptionPath => System.IO.Path.Combine(Path, "description.txt");

        public virtual string Path
        {
            get => _path;
            set => _path = value;
        } //view property

        public virtual void CommitChanges()
        {
            if (SuspendedDescription != Description)
            {
                File.WriteAllText(DescriptionPath, SuspendedDescription);
                OnPropertyChanged($"Description");
            }
        }

        public void SetIcon(string path)
        {
            if (path == InnerIconPath) return;
            File.Copy(path, InnerIconPath, true);
            OnPropertyChanged($"IconPath");
            OnPropertyChanged($"InnerIconPath");
        }

        public void ResetSuspended()
        {
            SuspendedName = NameWithoutExtension;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
