using RomExplorer.IO;
using RomExplorer.Model;
using RomExplorer.ViewModel;
using RomExplorer.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace RomExplorer.Pages
{
    /// <summary>
    /// AddGamePage.xaml 的交互逻辑
    /// </summary>
    public partial class AddGamePage : Page
    {
        private readonly AddGameConsoleWindow _baseWindow;
        private readonly ConsoleMachine _baseConsole;

        public AddGamePage(AddGameConsoleWindow baseWindow, ConsoleMachine baseConsole)
        {
            _baseWindow = baseWindow;
            _baseConsole = baseConsole;
            InitializeComponent();
        }

        public AddGamePageViewModel ViewModel { get; private set; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (AddGamePageViewModel)DataContext;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var fileName = FileExtension.TryValidateFileName(ViewModel.Name, out var trueName)
                ? trueName
                : Path.GetFileNameWithoutExtension(ViewModel.Path);
            var ext = Path.GetExtension(ViewModel.Path);

            if (ViewModel.UseShortcut)
            {
                IShellLink link = (IShellLink)new ShellLink();

                //link.SetDescription(fileName);
                link.SetPath(ViewModel.Path);
                System.Runtime.InteropServices.ComTypes.IPersistFile file =
                    (System.Runtime.InteropServices.ComTypes.IPersistFile)link;
                file.Save(Path.Combine(_baseConsole.RomDirectoryPath, fileName + ".lnk"), false);
            }
            else
            {
                File.Copy(ViewModel.Path, Path.Combine(_baseConsole.RomDirectoryPath, fileName + ext));
            }

            _baseConsole.Refresh();
            var game = _baseConsole.Games.FirstOrDefault(k => k.NameWithoutExtension == fileName);
            if (File.Exists(ViewModel.IconPath))
                game?.SetIcon(ViewModel.IconPath);
            game?.RaisePropertyChanged(nameof(game.IconPath));
            _baseWindow.Close();
        }

        private void TbPath_TextChanged(object sender, TextChangedEventArgs e)
        {
            var path = TbPath.Text;
            var handled = path.Trim().ToLower();
            string[] blackList = { ".exe", ".jar", ".com", ".bat" };
            if (!blackList.Any(s => handled.EndsWith(s))) return;
            if (File.Exists(path)) ViewModel.UseShortcut = true;
        }
    }
}
