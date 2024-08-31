using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class AiRootModel : IRootModel
    {
        private readonly bool _showMissingValues;

        public AiRootModel(RulesRootModel rulesRootModel,
            IniFile iniFile,
            IniFile? defaultFileOverwrite = null,
            bool showMissingValues = false)
        {
            _showMissingValues = showMissingValues;
            RulesModel = rulesRootModel;
            File = iniFile;
            DefaultFile = defaultFileOverwrite ?? rulesRootModel.FileType.GameDefinition.LoadDefaultAiFile();
            Aistructure = AistructureFile.Instance;
            LoadGameEntities();
            foreach (var rulesEntity in RulesModel.InfantryEntities
                         .Union(RulesModel.VehicleEntities)
                         .Union(RulesModel.AircraftEntities))
            {
                rulesEntity.RootModel = this;
                //TODO: caching of InfoNumber
                rulesEntity.InfoNumberFunction = () =>
                    TaskForceEntities.Count(e => e.EntityModel.FileSection.KeyValues.Any(k =>
                        k.Value.EndsWith($",{rulesEntity.EntityKey}")));
            }
        }

        public event EventHandler<EventArgs>? EntitiesChanged;

        public RulesRootModel RulesModel { get; }

        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public FileTypeModel FileType => RulesModel.FileType;

        public AistructureFile Aistructure { get; }

        public List<LookupItemModel> LookupItems => RulesModel.LookupItems;

        public Dictionary<string, List<GameEntityModel>> LookupEntities { get; } = new();

        public List<EntityListItemModel> TaskForceEntities { get; private set; } = null!;

        public List<EntityListItemModel> ScriptEntities { get; private set; } = null!;

        public List<EntityListItemModel> TeamEntities { get; private set; } = null!;

        public void ReloadGameEntites()
        {
            LoadGameEntities();
            EntitiesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LoadGameEntities()
        {
            TaskForceEntities = GetGameEntitiesByAiTypesSection("TaskForces", null);
            ScriptEntities = GetGameEntitiesByAiTypesSection("ScriptTypes", null);
            TeamEntities = GetGameEntitiesByAiTypesSection("TeamTypes", Aistructure.Teams.ToCategorizedList());
        }

        private List<EntityListItemModel> GetGameEntitiesByAiTypesSection(string aiTypesSection,
            List<CategorizedValueDefinition>? unitValueList)
        {
            var entityKeys = (File.GetSection(aiTypesSection)?.KeyValues
                              ?? Enumerable.Empty<IniFileLineKeyValue>())
                .UnionBy(DefaultFile.GetSection(aiTypesSection)?.KeyValues
                       ?? Enumerable.Empty<IniFileLineKeyValue>(),
                    k => k.Value);
            var artEntities = GetGameEntities(aiTypesSection, entityKeys, unitValueList);
            LookupItems.AddRange(artEntities.Select(e =>
                new LookupItemModel(e.EntityModel.EntityType, e.EntityModel.EntityKey,
                    e.EntityModel.FileSection?.GetValue("Name")?.Value
                    ?? (e.EntityModel.DefaultSection ?? e.EntityModel.FileSection)?.HeaderComments.FirstOrDefault()?.Comment
                    ?? "")));
            return artEntities;
        }

        private List<EntityListItemModel> GetGameEntities(string entityType,
            IEnumerable<IniFileLineKeyValue> entityKeysList,
            List<CategorizedValueDefinition>? unitValueList)
        {
            var result = new List<EntityListItemModel>();
            foreach (var entityKey in entityKeysList)
            {
                var fileSection = File.GetSection(entityKey.Value);
                var defaultSection = DefaultFile.GetSection(entityKey.Value);
                var rulesSection = RulesModel.File.GetSection(entityKey.Value);
                if (fileSection != null)
                {
                    var entityModel = new GameEntityModel(RulesModel, this,
                        entityType,
                        fileSection,
                        defaultSection,
                        unitValueList,
                        rulesSection);
                    result.Add(new EntityListItemModel(entityKey.Key, entityModel));
                }
                else if (defaultSection != null && _showMissingValues)
                {
                    var entityModel = new GameEntityModel(RulesModel, this,
                        entityType,
                        File.AddSection(entityKey.Value),
                        defaultSection,
                        unitValueList,
                        rulesSection);
                    result.Add(new EntityListItemModel(entityKey.Key, entityModel));
                }
            }
            if (LookupEntities.TryGetValue(entityType, out var existingLookupEntities))
            {
                existingLookupEntities.AddRange(result.Select(r => r.EntityModel));
            }
            else
            {
                LookupEntities.Add(entityType, result.Select(r => r.EntityModel).ToList());
            }
            return result;
        }

    }
}
