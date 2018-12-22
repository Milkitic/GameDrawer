using GameDrawer.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GameDrawer.Windows
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
        }

        private ConfigWindowViewModel ViewModel { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (ConfigWindowViewModel)DataContext;
            ViewModel.SetConfig(App.Config);
        }
    }
}
