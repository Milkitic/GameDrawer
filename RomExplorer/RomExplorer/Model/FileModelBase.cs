using Milkitic.WpfApi;
using RomExplorer.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoPath = System.IO.Path;

namespace RomExplorer.Model
{
    public class FileModelBase : ViewModelBase
    {
        public event EventHandler Committed;

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

        public string GetDescriptionFromFile()
        {
            return File.ReadAllText(DescriptionPath);
        }

        private bool _descriptionEditable;

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

        public bool NameEditable
        {
            get => _nameEditable;
            set
            {
                _nameEditable = value;
                OnPropertyChanged();
            }
        }

        public string Identity => IdentityHelper.GetRelativePath(Path);
        public virtual string IconPath //view property
            => File.Exists(InnerIconPath)
                ? InnerIconPath
                : IoPath.Combine(Config.IconCacheDirectory, $"{IoPath.GetExtension(Path)}.png");

        public virtual string DescriptionPath => System.IO.Path.Combine(Path, "description.txt");

        public virtual string Path { get; set; } //view property

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

        public virtual void OnCommitted(object sender, EventArgs e)
        {
            //Committed?.Invoke(sender, e);
        }

        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
