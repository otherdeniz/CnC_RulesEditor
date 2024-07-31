using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class ShowChangesForm : Form
    {
        public ShowChangesForm()
        {
            InitializeComponent();
        }

        public void LoadModel(IniFile changesFile, IniFile defaultFile)
        {
            var changesModel = new RootModel(changesFile, FileTypeModel.Empty, defaultFile);
            rulesEdit.LoadModel(changesModel, "", "");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
