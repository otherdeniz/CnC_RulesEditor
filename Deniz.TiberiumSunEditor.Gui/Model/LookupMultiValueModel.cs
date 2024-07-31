using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Model;

public class LookupMultiValueModel : ILookupValueModel
{
    [DisplayName(" ")]
    public bool Selected { get; set; }

    public string Key { get; set; } = null!;

    public string Name { get; set; } = null!;

    string ILookupValueModel.Value => Key;

}