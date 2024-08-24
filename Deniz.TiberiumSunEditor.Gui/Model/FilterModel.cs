namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class FilterModel
    {
        public string? FilterByHouse { get; set; }

        public string? FieldKey { get; set; }

        public FilterComparison Comparison { get; set; }

        public string Value { get; set; } = string.Empty;
    }

    public enum FilterComparison
    {
        Contains = 0,
        GreaterThan = 1,
        LesserThan = 2,
        IsYes = 3,
        IsNo = 4,
        IsEmpty = 5,
        IsNotEmpty = 6
    }
}
