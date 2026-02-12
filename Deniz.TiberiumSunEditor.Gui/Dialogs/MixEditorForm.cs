using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.CncParser;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class MixEditorForm : Form
    {
        private readonly List<MixEditorEntry> _entries = new List<MixEditorEntry>();
        private string? _currentFilePath;

        public MixEditorForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        private void MixEditorForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }

        private void buttonOpenMix_Click(object sender, EventArgs e)
        {
            using var openDialog = new OpenFileDialog
            {
                Filter = "MIX Files (*.mix)|*.mix|All Files (*.*)|*.*",
                Title = "Open MIX File"
            };
            if (openDialog.ShowDialog(this) != DialogResult.OK) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                var mixFile = new MixFile(Path.GetFileName(openDialog.FileName));
                mixFile.Parse(openDialog.FileName);

                var contents = mixFile.GetFileContents();

                _entries.Clear();
                mixFile.OpenFile();
                try
                {
                    foreach (var content in contents)
                    {
                        if (string.Equals(content.FileName, "local mix database.dat",
                                StringComparison.InvariantCultureIgnoreCase))
                            continue;

                        var data = mixFile.GetSingleFileData(content.FileLocationInfo.Offset,
                            content.FileLocationInfo.Size);
                        _entries.Add(new MixEditorEntry(content.FileName!, data));
                    }
                }
                finally
                {
                    mixFile.CloseFile();
                }

                _currentFilePath = openDialog.FileName;
                RefreshListView();
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to open MIX file:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonNewEmpty_Click(object sender, EventArgs e)
        {
            _entries.Clear();
            _currentFilePath = null;
            RefreshListView();
            UpdateUI();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using var openDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*",
                Title = "Add Files to MIX",
                Multiselect = true
            };
            if (openDialog.ShowDialog(this) != DialogResult.OK) return;

            foreach (var filePath in openDialog.FileNames)
            {
                var fileName = Path.GetFileName(filePath);
                var existing = _entries.FindIndex(
                    entry => string.Equals(entry.FileName, fileName, StringComparison.InvariantCultureIgnoreCase));

                if (existing >= 0)
                {
                    var result = MessageBox.Show(this,
                        $"\"{fileName}\" already exists. Replace it?",
                        "Duplicate File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        _entries[existing] = new MixEditorEntry(fileName, File.ReadAllBytes(filePath));
                    }
                }
                else
                {
                    _entries.Add(new MixEditorEntry(fileName, File.ReadAllBytes(filePath)));
                }
            }

            RefreshListView();
            UpdateUI();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var selectedItems = listViewEntries.SelectedItems.Cast<ListViewItem>().ToList();
            foreach (var item in selectedItems)
            {
                if (item.Tag is MixEditorEntry entry)
                {
                    _entries.Remove(entry);
                }
            }

            RefreshListView();
            UpdateUI();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = "MIX Files (*.mix)|*.mix|All Files (*.*)|*.*",
                Title = "Save MIX File",
                FileName = _currentFilePath != null ? Path.GetFileName(_currentFilePath) : "new.mix"
            };
            if (saveDialog.ShowDialog(this) != DialogResult.OK) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                MixFileWriter.Write(saveDialog.FileName, _entries);
                _currentFilePath = saveDialog.FileName;
                UpdateUI();
                MessageBox.Show(this, "MIX file saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to save MIX file:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRemove.Enabled = listViewEntries.SelectedItems.Count > 0;
        }

        private void RefreshListView()
        {
            listViewEntries.Items.Clear();
            foreach (var entry in _entries.OrderBy(e => e.FileName, StringComparer.InvariantCultureIgnoreCase))
            {
                var item = new ListViewItem(entry.FileName);
                item.SubItems.Add(entry.Size.ToString("N0"));
                item.Tag = entry;
                listViewEntries.Items.Add(item);
            }
        }

        private void UpdateUI()
        {
            labelFilePath.Text = _currentFilePath ?? "(New MIX File)";
            labelFileCount.Text = $"{_entries.Count} file(s)";
            buttonSave.Enabled = _entries.Count > 0;
            buttonRemove.Enabled = listViewEntries.SelectedItems.Count > 0;
        }
    }
}
