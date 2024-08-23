namespace Deniz.TiberiumSunEditor.Gui.Utils.Files
{
    public class FileChangeWatcher : IDisposable
    {
        private readonly FileSystemWatcher _watcher;

        public FileChangeWatcher(string path, string filter)
        {
            _watcher = new FileSystemWatcher();
            _watcher.Path = path;
            _watcher.Filter = filter;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Changed += WatcherOnChanged;
            _watcher.EnableRaisingEvents = true;
        }

        public event EventHandler<FileSystemEventArgs>? Changed;

        public bool IsWatching { get; set; } = true;

        public void StartWatching()
        {
            IsWatching = true;
        }

        public void StopWatching()
        {
            IsWatching = false;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs e)
        {
            if (IsWatching)
            {
                Changed?.Invoke(this, e);
            }
        }

        public void Dispose()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
        }
    }
}
