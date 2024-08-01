namespace Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;

public class StringEqualityComparer : IEqualityComparer<string>
{
    public static readonly StringEqualityComparer Instance = new();

    public bool Equals(string x, string y)
    {
        return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
    }

    public int GetHashCode(string obj)
    {
        return obj.GetHashCode();
    }
}