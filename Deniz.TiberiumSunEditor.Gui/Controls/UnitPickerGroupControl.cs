using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using Infragistics.Win;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class UnitPickerGroupControl : UserControl
    {
        private bool _doEvents;

        public UnitPickerGroupControl()
        {
            InitializeComponent();
            unitsLayoutPanel.AutoSize = true;
        }

        public List<UnitPickerControl> UnitPickerControls { get; } = new();

        public EntityGroupSetting EntityGroupSetting { get; private set; } = new();

        public void InitGroup(EntityGroupSetting entityGroupSetting)
        {
            _doEvents = false;
            EntityGroupSetting = entityGroupSetting;
            textBoxGroupName.Text = entityGroupSetting.GroupName;
            ThemeManager.Instance.UseTheme(textBoxGroupName);
            var colors = new Color[]
            {
                Color.Blue,
                Color.Aqua,
                Color.BlueViolet,
                Color.Brown,
                Color.DarkOrange,
                Color.DarkCyan,
                Color.Green,
                Color.LawnGreen,
                Color.Gold,
                Color.Yellow,
                Color.Red
            };
            foreach (var color in colors)
            {
                var listItem = new ValueListItem(color, " ");
                listItem.Appearance.BackColor = color;
                ultraComboColor.Items.Add(listItem);
            }
            ultraComboColor.Items.Add(new ValueListItem("other", "other"));
            ultraComboColor.Appearance.BackColor = EntityGroupSetting.GroupColor;
            ApplyColor();
            _doEvents = true;
        }

        public void AddPickerControl(UnitPickerControl unitPickerControl)
        {
            UnitPickerControls.Add(unitPickerControl);
            unitsLayoutPanel.Controls.Add(unitPickerControl);
        }

        private void ApplyColor()
        {
            unitsLayoutPanel.BackColor = EntityGroupSetting.GroupColor;
        }

        private void unitsLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            Height = unitsLayoutPanel.Top + unitsLayoutPanel.Height;
        }

        private void textBoxGroupName_TextChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            EntityGroupSetting.GroupName = textBoxGroupName.Text;
            UserSettingsFile.Instance.Save();
        }

        private void ultraComboColor_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents || ultraComboColor.SelectedItem == null) return;
            if (ultraComboColor.SelectedItem.DataValue is Color colorItem)
            {
                EntityGroupSetting.GroupColor = colorItem;
            }
            else
            {
                if (colorDialog.ShowDialog(ParentForm) != DialogResult.OK)
                {
                    ultraComboColor.SelectedIndex = -1;
                    return;
                }
                EntityGroupSetting.GroupColor = colorDialog.Color;
            }
            UserSettingsFile.Instance.Save();
            ApplyColor();
            ultraComboColor.Appearance.BackColor = EntityGroupSetting.GroupColor;
            ultraComboColor.SelectedIndex = -1;
        }

    }

}
