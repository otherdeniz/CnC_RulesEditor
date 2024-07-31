using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

namespace Deniz.TiberiumSunEditor.Gui.Model;

public interface IValueModel
{
    string Category { get; }
    string Key { get; }
    string Value { get; set; }
    string DefaultValue { get; }
    string Description { get; }

    UnitValueDefinition ValueDefinition { get; }
    bool Favorite { get; set; }
}