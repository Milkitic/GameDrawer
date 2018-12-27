using Milkitic.WpfApi;
using System.Threading;
using System.Threading.Tasks;

namespace GameDrawer.Model
{
    public class GameListProperties : ViewModelBase
    {
        public static Task SyncTask;
        public static CancellationTokenSource SyncCts;
        public static bool IsSyncRunning =>
            SyncTask != null && !(SyncTask.IsCanceled || SyncTask.IsCompleted || SyncTask.IsFaulted);

        public bool IsSyncRunningView =>
            SyncTask != null && !(SyncTask.IsCanceled || SyncTask.IsCompleted || SyncTask.IsFaulted);

        public void NotifySyncChanged()
        {
            OnPropertyChanged(nameof(IsSyncRunningView));
        }
    }
}