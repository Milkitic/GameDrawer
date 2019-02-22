using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using Milky.WpfApi;
using Milky.ApplicationUpdater.Github;

namespace Milky.ApplicationUpdater
{
    /// <summary>
    /// NewVersionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewVersionWindow : Window
    {
        private readonly Action _updateCallback;
        private readonly Action _laterCallback;
        private readonly Action _skipCallback;
        private bool _later = false;
        public NewVersionWindow(Release release, Action updateCallback)
            : this(release, updateCallback, null, null)
        {

        }

        public NewVersionWindow(Release release, Action updateCallback, Action laterCallback)
            : this(release, updateCallback, laterCallback, null)
        {
        }

        public NewVersionWindow(Release release, Action updateCallback, Action laterCallback, Action skipCallback)
        {
            _updateCallback = updateCallback;
            _laterCallback = laterCallback;
            _skipCallback = skipCallback;

            InitializeComponent();
            MainGrid.DataContext = release;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            _updateCallback?.Invoke();
            Close();
        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e)
        {
            _skipCallback?.Invoke();
            Close();
        }

        private void BtnLater_Click(object sender, RoutedEventArgs e)
        {
            _later = true;
            Close();
        }

        public new void Close()
        {
            if (_later)
                _laterCallback?.Invoke();
            base.Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)sender;
            Process.Start(link.NavigateUri.AbsoluteUri);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }
    }
}
