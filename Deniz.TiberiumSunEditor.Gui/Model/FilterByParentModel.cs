using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class FilterByParentModel
    {
        public FilterByParentModel(GameEntityModel parentEntity, Func<IniFileLineKeyValue, bool> filterFunction)
        {
            ParentEntity = parentEntity;
            Key = "";
            Value = "";
            FilterFunction = filterFunction;
        }

        public FilterByParentModel(GameEntityModel parentEntity, string key, string value)
        {
            ParentEntity = parentEntity;
            Key = key;
            Value = value;
        }

        public GameEntityModel ParentEntity { get; }

        public string Key { get; }

        public string Value { get; }

        public Func<IniFileLineKeyValue, bool>? FilterFunction { get; }
    }
}
