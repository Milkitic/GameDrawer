using Milkitic.WpfApi;
using RomExplorer.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomExplorer.Model
{
    public class FileModelBase : ViewModelBase
    {
        private string InnerIconPath => System.IO.Path.Combine(Path, "icon.png");
        protected virtual string SuspendedName { get; set; }
        protected virtual string SuspendedDescription { get; set; } = "暂无介绍";

        public virtual string NameWithoutExtension //view property
        {
            get => new DirectoryInfo(Path).Name;
            set => SuspendedName = value;
        }

        public string Description
        {
            get => File.ReadAllText(DescriptionPath);
            set => SuspendedDescription = value;
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

        public string Identity => IdentityHelper.GetRelativePath(Path);
        public virtual string IconPath //view property
            => InnerIconPath;

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
            File.Copy(path, InnerIconPath);
            OnPropertyChanged();
        }
    }
}
