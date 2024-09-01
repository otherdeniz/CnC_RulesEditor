using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils;
using ImageMagick;
using Infragistics.Win.UltraWinTabControl;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    public partial class QuickEditForm : Form
    {
        public QuickEditForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
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
                    var entityEditControlType =
                        rootModel.EntityTypeEditControl.FirstOrDefault(e => e.EntityType == entityModel.EntityType)
                            ?.EditControlType;
                    if (entityEditControlType != null)
                    {
                        var contentControl = (EntityEditBaseControl)Activator.CreateInstance(entityEditControlType)!;
                        ThemeManager.Instance.UseTheme(contentControl);
                        contentControl.Dock = DockStyle.Fill;
                        contentControl.LoadEntity(entityModel);
                        tabPageControl.Controls.Add(contentControl);
                    }
                    else
                    {
                        var unitEditControl = new UnitEditControl
                        {
                            Dock = DockStyle.Fill
                        };
                        unitEditControl.LoadModel(entityModel);
                        ThemeManager.Instance.UseTheme(unitEditControl);
                        tabPageControl.Controls.Add(unitEditControl);
                    }
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

        private void QuickEditForm_Load(object sender, EventArgs e)
        {
            DarkTitleBarHelper.UseImmersiveDarkMode(Handle, ThemeManager.Instance.CurrentTheme.WindowUseDarkHeader);
        }
    }
}
