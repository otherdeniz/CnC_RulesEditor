namespace Deniz.TiberiumSunEditor.Gui.Model;

public class LookupTextValueModel : ILookupValueModel
{
    public string Value { get; set; } = null!;

    public string Label { get; set; } = string.Empty;
}