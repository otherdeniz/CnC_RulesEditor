using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Exceptions;
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

        public EntityListItemModel AddGameEntity(string entityType)
        {
            var newKeyPrefix = FileType.BaseType == FileBaseType.Ai
                ? "A"
                : "B";
            var newKeySufix = FileType.BaseType == FileBaseType.Ai
                ? "-G"
                : "";
            List<EntityListItemModel> entitiesList;
            List<CategorizedValueDefinition>? unitValueList = null;
            switch (entityType)
            {
                case "TaskForces":
                    newKeyPrefix += "1";
                    entitiesList = TaskForceEntities;
                    break;
                case "ScriptTypes":
                    newKeyPrefix += "2";
                    entitiesList = ScriptEntities;
                    break;
                case "TeamTypes":
                    newKeyPrefix += "3";
                    entitiesList = TeamEntities;
                    unitValueList = Aistructure.Teams.ToCategorizedList();
                    break;
                default:
                    throw new RuntimeException($"could not add new GameEntity for EntiyType '{entityType}'");
            }

            string newKey;
            do
            {
                newKey = $"{newKeyPrefix}{HexGenerator.GenerateRandomHex(6)}{newKeySufix}";
            } while (entitiesList.Any(e => e.EntityModel.EntityKey == newKey));

            var newFileSection = File.AddSection(newKey);
            var newGameEntity = new GameEntityModel(RulesModel,
                this,
                entityType,
                newFileSection,
                null,
                unitValueList);

            var entitiesTypesSection = File.GetSection(entityType)
                                       ?? File.AddSection(entityType);
            var typeKey = entitiesTypesSection.KeyValues.Any()
                ? entitiesTypesSection.KeyValues.Max(k => int.TryParse(k.Key, out var number) ? number : 0) + 1
                : 0;
            entitiesTypesSection.SetValue(typeKey.ToString(), newKey);

            var newListItemModel = new EntityListItemModel(typeKey.ToString(), newGameEntity);
            entitiesList.Add(newListItemModel);
            return newListItemModel;
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
