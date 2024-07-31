namespace Deniz.TiberiumSunEditor.Gui.Utils.Files
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JsonFileNameAttribute : Attribute
    {
        public JsonFileNameAttribute(string filename)
        {
            Filename = filename;
        }

        public string Filename { get; }
    }
}
