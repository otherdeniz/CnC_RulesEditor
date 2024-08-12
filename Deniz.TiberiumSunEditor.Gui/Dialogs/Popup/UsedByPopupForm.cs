using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    public partial class UsedByPopupForm : PopupFormBase
    {
        public UsedByPopupForm()
        {
            InitializeComponent();
        }

        public void LoadUsedByEntities(List<GameEntityModel> usedByEntityModels, string usesKey)
        {
            // clear controls
            var controlsToDispose = Controls
                .OfType<GroupBox>().ToList();
            controlsToDispose.ForEach(c =>
            {
                Controls.Remove(c);
                c.Dispose();
            });
            // add conrols
            foreach (var entityGroup in usedByEntityModels
                         .Take(25) // max entries
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
                    groupBoxControl.Controls.Add(usedInControl);
                }
                Controls.Add(groupBoxControl);
            }

        }
    }
}
