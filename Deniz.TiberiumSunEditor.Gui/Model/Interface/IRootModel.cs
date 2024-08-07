using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model.Interface
{
    public interface IRootModel
    {
        IniFile File { get; }

        IniFile DefaultFile { get; }

        FileTypeModel FileType { get; }
    }
}
