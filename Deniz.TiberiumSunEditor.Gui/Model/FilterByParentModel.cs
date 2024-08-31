using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class FilterByParentModel
    {
        public FilterByParentModel(string key, string value, Func<IniFileLineKeyValue, bool>? filterFunction = null)
        {
            Key = key;
            Value = value;
            FilterFunction = filterFunction;
        }

        public string Key { get; }

        public string Value { get; }

        public Func<IniFileLineKeyValue, bool>? FilterFunction { get; }
    }
}
