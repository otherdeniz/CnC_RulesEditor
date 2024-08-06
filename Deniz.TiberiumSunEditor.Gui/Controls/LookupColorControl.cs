using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    public partial class LookupColorControl : UserControl
    {
        private bool _doEvents;
        private IValueModel? _valueModel;
        private string _valueKey = string.Empty;

        public LookupColorControl()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs>? RefreshEntityValue;

        public void LoadValue(IValueModel valueModel)
        {
            _valueModel = valueModel;
            var keyValue = valueModel.Value;
            var colorValues = keyValue.Split(",")
                                  .Select(v => int.TryParse(v.Trim(), out var intValue) ? intValue : 0).ToArray();
            if (colorValues.Length != 3)
            {
                colorValues = new int[] { 0, 0, 0 };
            }

            _doEvents = false;
            numericRed.Value = colorValues[0];
            colorSliderRed.Value = colorValues[0];
            numericGreen.Value = colorValues[1];
            colorSliderGreen.Value = colorValues[1];
            numericBlue.Value = colorValues[2];
            colorSliderBlue.Value = colorValues[2];
            panelColor.BackColor = Color.FromArgb(colorValues[0], colorValues[1], colorValues[2]);
            _doEvents = true;
        }

        private void UpdateValue()
        {
            if (_valueModel == null) return;
            panelColor.BackColor = Color.FromArgb(
                Convert.ToInt32(numericRed.Value), 
                Convert.ToInt32(numericGreen.Value), 
                Convert.ToInt32(numericBlue.Value));
            _valueModel.Value = $"{numericRed.Value:0},{numericGreen.Value:0},{numericBlue.Value:0}";
            RefreshEntityValue?.Invoke(this, EventArgs.Empty);
        }

        private void numericRed_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _doEvents = false;
            colorSliderRed.Value = Convert.ToSingle(numericRed.Value);
            UpdateValue();
            _doEvents = true;
        }

        private void colorSliderRed_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _doEvents = false;
            numericRed.Value = Convert.ToDecimal(colorSliderRed.Value);
            UpdateValue();
            _doEvents = true;
        }

        private void numericGreen_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _doEvents = false;
            colorSliderGreen.Value = Convert.ToSingle(numericGreen.Value);
            UpdateValue();
            _doEvents = true;
        }

        private void colorSliderGreen_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _doEvents = false;
            numericGreen.Value = Convert.ToDecimal(colorSliderGreen.Value);
            UpdateValue();
            _doEvents = true;
        }

        private void numericBlue_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _doEvents = false;
            colorSliderBlue.Value = Convert.ToSingle(numericBlue.Value);
            UpdateValue();
            _doEvents = true;
        }

        private void colorSliderBlue_ValueChanged(object sender, EventArgs e)
        {
            if (!_doEvents) return;
            _doEvents = false;
            numericBlue.Value = Convert.ToDecimal(colorSliderBlue.Value);
            UpdateValue();
            _doEvents = true;
        }
    }
}
