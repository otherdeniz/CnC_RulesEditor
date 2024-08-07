using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs;

public partial class ShowArtChangesForm : Form
{
    public ShowArtChangesForm()
    {
        InitializeComponent();
    }

    public void LoadModel(IniFile changesFile, IniFile defaultFile, RulesRootModel rulesRootModel)
    {
        var changesModel = new ArtRootModel(rulesRootModel, changesFile, defaultFile);
        artEdit.LoadModel(changesModel);
    }

    private void buttonClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}