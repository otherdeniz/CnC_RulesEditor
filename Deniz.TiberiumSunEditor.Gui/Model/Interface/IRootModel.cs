using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model.Interface
{
    public interface IRootModel
    {
        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public FileTypeModel FileType { get; }

        public RulesRootModel RulesModel { get; }

        public List<LookupItemModel> LookupItems { get; }

        public Dictionary<string, List<GameEntityModel>> LookupEntities { get; }
    }
}
