﻿using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;
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
            AnimationsAsyncLoader.Instance.Stop(true, false);
            var changesModel = new RootModel(changesFile, FileTypeModel.Empty, defaultFile);
            rulesEdit.LoadModel(changesModel, "", "");
            AnimationsAsyncLoader.Instance.Start();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
