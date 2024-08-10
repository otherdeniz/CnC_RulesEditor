using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Infragistics.Win.UltraWinTabControl;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class QuickEditForm : Form
    {
        public QuickEditForm()
        {
            InitializeComponent();
        }

        public static void ExecueShow(Form parentForm, IRootModel rootModel, string entityKeys)
        {
            var editForm = new QuickEditForm();
            if (!string.IsNullOrEmpty(entityKeys)
                && editForm.LoadEntities(rootModel, entityKeys))
            {
                editForm.Show(parentForm);
            }
            else
            {
                editForm.Dispose();
            }
        }

        private bool LoadEntities(IRootModel rootModel, string entityKeys)
        {
            var result = false;
            foreach (var entityKey in entityKeys.Split(",", StringSplitOptions.RemoveEmptyEntries))
            {
                var entityModel = rootModel.LookupEntities.Values.SelectMany(v => v)
                    .FirstOrDefault(m => m.EntityKey == entityKey);
                if (entityModel != null)
                {
                    var unitEditControl = new UnitEditControl
                    {
                        Dock = DockStyle.Fill
                    };
                    unitEditControl.LoadModel(entityModel);
                    var tabPageControl = new UltraTabPageControl
                    {
                        Location = new Point(-10000, -10000),
                        Margin = new Padding(4, 3, 4, 3),
                        Tag = "Custom"
                    };
                    var ultraTab = new UltraTab
                    {
                        Text = $"{entityKey} [{entityModel.EntityType}]"
                    };
                    ultraTab.TabPage = tabPageControl;
                    tabPageControl.Controls.Add(unitEditControl);
                    ultraTabEntities.Controls.Add(tabPageControl);
                    ultraTabEntities.Tabs.Add(ultraTab);
                    result = true;
                }
            }
            return result;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
