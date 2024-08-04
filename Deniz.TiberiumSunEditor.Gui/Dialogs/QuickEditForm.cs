using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Infragistics.Win.UltraWinTabControl;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class QuickEditForm : Form
    {
        public QuickEditForm()
        {
            InitializeComponent();
        }

        public static void ExecueShow(Form parentForm, RootModel rootModel, string entityKeys)
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

        private bool LoadEntities(RootModel rootModel, string entityKeys)
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
                        Text = entityKey
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
    }
}
