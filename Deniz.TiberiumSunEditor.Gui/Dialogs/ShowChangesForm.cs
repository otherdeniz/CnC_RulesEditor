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

        public void LoadModel(IniFile changesFile, IniFile defaultFile, RulesRootModel rulesRootModel)
        {
            var changesModel = new RulesRootModel(changesFile, FileTypeModel.Empty, defaultFile,
                useAres: rulesRootModel.UseAres,
                usePhobos: rulesRootModel.UsePhobos);
            rulesEdit.LoadModel(changesModel, "", "");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
