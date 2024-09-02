namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

public class ScriptBuildingParameter2Definition
{
    public int AddValue { get; set; }

    public string Name { get; set; } = string.Empty;

    public override string ToString()
    {
        return Name;
    }
}