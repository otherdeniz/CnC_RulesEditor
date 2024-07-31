namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class LookupSingleValueModel : ILookupValueModel
    {
        public string Key { get; set; } = null!;

        public string Name { get; set; } = null!;

        string ILookupValueModel.Value => Key;
    }
}
