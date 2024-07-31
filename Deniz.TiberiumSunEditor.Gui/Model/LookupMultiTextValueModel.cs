using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Model;

public class LookupMultiTextValueModel : ILookupValueModel
{
    [DisplayName(" ")]
    public bool Selected { get; set; }

    public string Value { get; set; } = null!;
}