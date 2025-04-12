using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    public partial class UsedByPopupForm : PopupFormBase
    {
        private readonly List<UsedInEntityControl> _allEntityControls = new List<UsedInEntityControl>();

        public UsedByPopupForm()
        {
            InitializeComponent();
            ThemeManager.Instance.UseTheme(this);
        }

        public void LoadUsedByEntities(Form mainForm, List<GameEntityModel> usedByEntityModels, string usesKey)
        {
            // clear controls
            var controlsToDispose = panelContent.Controls.OfType<GroupBox>().ToList();
            controlsToDispose.ForEach(c =>
            {
                panelContent.Controls.Remove(c);
                c.Dispose();
            });
            // add conrols
            foreach (var entityGroup in usedByEntityModels
                         .GroupBy(e => e.EntityType)
                         .OrderByDescending(g => g.Key))
            {
                var groupBoxControl = new GroupBox();
                groupBoxControl.AutoSize = true;
                groupBoxControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                groupBoxControl.Dock = DockStyle.Top;
                groupBoxControl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
                groupBoxControl.ForeColor = ThemeManager.Instance.CurrentTheme.ControlsTextColor;
                groupBoxControl.Size = new Size(350, 50);
                groupBoxControl.TabStop = false;
                groupBoxControl.Text = entityGroup.Key;
                foreach (var entityModel in entityGroup.OrderByDescending(e => e.EntityKey))
                {
                    var usedInControl = new UsedInEntityControl();
                    usedInControl.BorderStyle = BorderStyle.FixedSingle;
                    usedInControl.Dock = DockStyle.Top;
                    usedInControl.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
                    usedInControl.Location = new Point(3, 19);
                    usedInControl.MinimumSize = new Size(300, 35);
                    usedInControl.Size = new Size(350, 35);
                    usedInControl.TabIndex = 0;
                    usedInControl.LoadEntity(entityModel, usesKey);
                    usedInControl.Click += (sender, args) =>
                    {
                        QuickEditForm.ExecueShow(mainForm, entityModel.RootModel, entityModel.EntityKey);
                        ForceClose();
                    };
                    usedInControl.MouseEnter += (sender, args) => MouseEnterEntry(usedInControl);
                    groupBoxControl.Controls.Add(usedInControl);
                    _allEntityControls.Add(usedInControl);
                }
                panelContent.Controls.Add(groupBoxControl);
            }

            Height = panelContent.Height > MaximumSize.Height 
                ? MaximumSize.Height 
                : panelContent.Height;
        }

        private void MouseEnterEntry(UsedInEntityControl activateControl)
        {
            foreach (var entityControl in _allEntityControls)
            {
                if (entityControl == activateControl)
                {
                    entityControl.BackColor = ThemeManager.Instance.CurrentTheme.HotTrackBackColor;
                    entityControl.ForeColor = ThemeManager.Instance.CurrentTheme.HotTrackTextColor;
                }
                else
                {
                    entityControl.BackColor = ThemeManager.Instance.CurrentTheme.ControlsBackColor;
                    entityControl.ForeColor = ThemeManager.Instance.CurrentTheme.ControlsTextColor;
                }
            }
        }

        private void UsedByPopupForm_Load(object sender, EventArgs e)
        {
            var syncContext = SynchronizationContext.Current;
            Task.Run(() =>
            {
                syncContext?.Post(args =>
                {
                    panelScrolling.AutoScroll = true;
                }, null);
            });
        }
    }
}
