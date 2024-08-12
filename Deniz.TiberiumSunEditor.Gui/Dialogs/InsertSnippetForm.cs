using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Infragistics.Win.UltraWinGrid;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class InsertSnippetForm : Form
    {
        private IniFile _defaultFile = null!;
        private SnippetModel? _selectedSnippet;

        public InsertSnippetForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public static bool InsertSnippetToModel(Form mainForm, RulesRootModel rulesRootModel)
        {
            using (var snippetForm = new InsertSnippetForm())
            {
                snippetForm._defaultFile = rulesRootModel.DefaultFile;
                snippetForm.ListSnippets(rulesRootModel.FileType.GameDefinition.SnippetsFolder);
                if (snippetForm.ShowDialog(mainForm) == DialogResult.OK
                    && snippetForm._selectedSnippet != null)
                {
                    rulesRootModel.File.MergeWithFile(snippetForm._selectedSnippet.Model.File,
                        snippetForm._selectedSnippet.Model.DefaultFile);
                    rulesRootModel.ReloadGameEntites();
                    return true;
                }
            }

            return false;
        }

        private void ListSnippets(string editionFolder)
        {
            valuesGrid.DataSource = SnippetsRepository.Instance.GetSnippets(editionFolder, _defaultFile);
            if (valuesGrid.DisplayLayout.Bands[0].SortedColumns.Count == 0)
            {
                valuesGrid.DisplayLayout.Bands[0].SortedColumns.Add("Folder", false, true);
            }
        }

        private void SelectSnippet(SnippetModel snippetModel)
        {
            rulesEdit.Visible = false;
            Application.DoEvents();
            _selectedSnippet = snippetModel;
            rulesEdit.LoadModel(snippetModel.Model, "Snippet", snippetModel.Name);
            rulesEdit.Visible = true;
            buttonOk.Enabled = true;
        }

        private void valuesGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (valuesGrid.Selected.Rows.Count > 0
                && valuesGrid.Selected.Rows[0].ListObject is SnippetModel snippetModel)
            {
                SelectSnippet(snippetModel);
            }
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                valuesGrid.Selected.Rows.Clear();
                SelectSnippet(new SnippetModel("",
                    Path.GetFileNameWithoutExtension(openFileDialog.FileName),
                    IniFile.Load(openFileDialog.FileName),
                    _defaultFile));
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InsertSnippetForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
