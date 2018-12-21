using Milkitic.WpfApi;
using System.Threading;
using System.Threading.Tasks;

namespace GameDrawer.Model
{
    public class GameListExtensionProperties : ViewModelBase
    {
        public static Task RefreshTask;
        public static CancellationTokenSource RefreshCts;
        public static bool IsRefreshRunning =>
            RefreshTask != null && !(RefreshTask.IsCanceled || RefreshTask.IsCompleted || RefreshTask.IsFaulted);

        public bool IsRefreshRunningView =>
            RefreshTask != null && !(RefreshTask.IsCanceled || RefreshTask.IsCompleted || RefreshTask.IsFaulted);

        public void NotifyRefreshChanged()
        {
            OnPropertyChanged(nameof(IsRefreshRunningView));
        }
    }
}