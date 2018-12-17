﻿using RomExplorer.Model;
using RomExplorer.Pages;
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

namespace RomExplorer.Windows
{
    /// <summary>
    /// AddGameConsoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddGameConsoleWindow : Window
    {
        private readonly ConsoleMachine _console;

        public AddGameConsoleWindow(ConsoleMachine console)
        {
            _console = console;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_console == null)
            {
                MainFrame.Navigate(new AddConsolePage(this));
            }
            else
            {
                MainFrame.Navigate(new AddGamePage(this, _console));
            }
        }
    }
}