using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class FilterByParentModel
    {
        public FilterByParentModel(Func<IniFileLineKeyValue, bool> filterFunction)
        {
            Key = "";
            Value = "";
            FilterFunction = filterFunction;
        }

        public FilterByParentModel(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }

        public Func<IniFileLineKeyValue, bool>? FilterFunction { get; }
    }
}
