using System.ComponentModel;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class SnippetModel
    {
        public SnippetModel(string folder, string name, IniFile file, IniFile defaultFile)
        {
            Folder = folder;
            Name = name;
            Model = new RulesRootModel(file, FileTypeModel.ParseFile(defaultFile)!, defaultFile);
        }

        public string Folder { get; }

        public string Name { get; }

        [Browsable(false)]
        public RulesRootModel Model { get; }
    }
}
