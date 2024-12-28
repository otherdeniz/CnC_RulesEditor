using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class FileChangedControl : UserControl
    {
        private FileChangeWatcher? _fileChangeWatcher;
        private SynchronizationContext _uiSyncContext = null!;

        public FileChangedControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? ReloadFile;

        [Browsable(false)]
        public bool HasUnsavedChanges { get; private set; }

        [Browsable(false)]
        public bool HasExternalChanges { get; private set; }

        public void BindFile(IniFile iniFile)
        {
            iniFile.ValueChanged += (sender, args) => OnFileValueChanged(true);
            iniFile.BeforeOriginalFileSaved += (sender, args) => StopFileWatcher();
            iniFile.AfterOriginalFileSaved += (sender, args) =>
            {
                OnFileValueChanged(false);
                StartFileWatcher(iniFile.OriginalFullPath!);
            };
            if (!string.IsNullOrEmpty(iniFile.OriginalFullPath))
            {
                StartFileWatcher(iniFile.OriginalFullPath);
            }
        }

        private void StartFileWatcher(string iniFileFullPath)
        {
            var fileInfo = new FileInfo(iniFileFullPath);
            _fileChangeWatcher = new FileChangeWatcher(fileInfo.Directory!.FullName, fileInfo.Name);
            _fileChangeWatcher.Changed += FileChangeWatcherOnChanged;
            _fileChangeWatcher.StartWatching();
        }

        private void StopFileWatcher()
        {
            if (_fileChangeWatcher != null)
            {
                _fileChangeWatcher.StopWatching();
                _fileChangeWatcher.Changed -= FileChangeWatcherOnChanged;
            }
            _fileChangeWatcher = null;
        }

        private void FileChangeWatcherOnChanged(object? sender, FileSystemEventArgs e)
        {
            if (HasUnsavedChanges || HasExternalChanges) return;
            HasExternalChanges = true;
            _uiSyncContext.Post(args =>
            {
                labelMessage.Text = "External changes detected";
                labelMessage.Visible = true;
                labelReload.Visible = true;
            }, null);
        }

        private void OnFileValueChanged(bool hasChanges)
        {
            if (HasUnsavedChanges && hasChanges) return;
            if (hasChanges)
            {
                labelMessage.Text = "Changes not saved yet";
                labelMessage.Visible = true;
            }
            else
            {
                labelMessage.Visible = false;
            }
            labelReload.Visible = false;
            HasUnsavedChanges = hasChanges;
        }

        private void labelReload_Click(object sender, EventArgs e)
        {
            ReloadFile?.Invoke(sender, EventArgs.Empty);
        }

        private void FileChangedControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                AutoSize = true;
                labelMessage.Visible = false;
                labelReload.Visible = false;
                _uiSyncContext = SynchronizationContext.Current!;
            }
        }
    }
}
