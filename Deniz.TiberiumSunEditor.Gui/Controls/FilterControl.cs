using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class FilterControl : UserControl
    {
        private IRootModel _rootModel = null!;

        public FilterControl()
        {
            InitializeComponent();
        }

        public event EventHandler? FilterChanged;

        public FilterModel? CurrentFilter { get; private set; }

        public void LoadModel(IRootModel rootModel)
        {
            _rootModel = rootModel;
            InitSidesCombo();
            IniFieldKeysCombo();
            InitComparisonCombo();
            textValue.Text = string.Empty;
        }

        private void InitSidesCombo()
        {
            ultraComboSide.Items.Clear();
            ultraComboSide.Items.Add("", "all");
            foreach (var side in _rootModel.RulesModel.SideEntities
                         .OrderBy(s => s.Thumbnail != null ? 0 : 1))
            {
                var valueListItem = ultraComboSide.Items.Add(side.EntityKey);
                var sideDefinition = _rootModel.FileType.GameDefinition.Sides
                    .FirstOrDefault(d => d.Name.Equals(side.FileSection.GetValue("Side")?.Value
                                                       ?? side.DefaultSection?.GetValue("Side")?.Value
                                                       ?? side.EntityKey,
                        StringComparison.InvariantCultureIgnoreCase));
                if (sideDefinition != null)
                {
                    valueListItem.Appearance.Image = sideDefinition.GetLogoImage();
                }
            }
            ultraComboSide.SelectedIndex = 0;
        }

        private void IniFieldKeysCombo()
        {
            comboField.Items.Clear();
            comboField.Items.Add("");
            foreach (var fieldKey in _rootModel.LookupEntities.Values.SelectMany(v => v)
                         .SelectMany(e => e.FileSection.KeyValues)
                         .Select(k => k.Key)
                         .Distinct(StringEqualityComparer.Instance)
                         .OrderBy(s => s))
            {
                comboField.Items.Add(fieldKey);
            }
            comboField.SelectedIndex = 0;
        }

        private void InitComparisonCombo()
        {
            comboComparison.Items.Clear();
            comboComparison.Items.AddRange(new object[]
            {
                "contains",
                "greater than",
                "smaller than",
                "is yes",
                "is no",
                "is empty"
            });
            comboComparison.SelectedIndex = 0;
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            CurrentFilter = new FilterModel
            {
                FilterByHouse = ultraComboSide.SelectedItem?.DataValue as string,
                FieldKey = comboField.SelectedItem as string,
                Comparison = (FilterComparison)comboComparison.SelectedIndex,
                Value = textValue.Text
            };
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ultraComboSide.SelectedIndex = 0;
            comboField.SelectedIndex = 0;
            comboComparison.SelectedIndex = 0;
            textValue.Text = string.Empty;
            CurrentFilter = null;
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void comboComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboComparison.SelectedIndex < (int)FilterComparison.IsYes)
            {
                textValue.Enabled = true;
            }
            else
            {
                textValue.Enabled = false;
                textValue.Text = string.Empty;
            }
        }
    }
}
