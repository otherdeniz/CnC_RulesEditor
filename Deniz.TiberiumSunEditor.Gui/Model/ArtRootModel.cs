using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class ArtRootModel
    {
        private readonly bool _showMissingValues;

        public ArtRootModel(RootModel rulesRootModel,
            IniFile iniFile,
            IniFile? defaultFileOverwrite = null,
            bool showMissingValues = false)
        {
            _showMissingValues = showMissingValues;
            RulesRootModel = rulesRootModel;
            File = iniFile;
            DefaultFile = defaultFileOverwrite ?? rulesRootModel.FileType.GameDefinition.LoadDefaultArtFile();
            LoadGameEntities();
        }

        public event EventHandler<EventArgs>? EntitiesChanged;

        public RootModel RulesRootModel { get; }

        public FileTypeModel FileType => RulesRootModel.FileType;

        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public List<GameEntityModel> VehicleEntities { get; private set; } = null!;

        public List<GameEntityModel> AircraftEntities { get; private set; } = null!;

        public List<GameEntityModel> InfantryEntities { get; private set; } = null!;

        public List<GameEntityModel> BuildingEntities { get; private set; } = null!;

        public void ReloadGameEntites()
        {
            LoadGameEntities();
            EntitiesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LoadGameEntities()
        {
            VehicleEntities = GetGameEntitiesByRulesTypesSection("VehicleTypes",
                new List<CategorizedValueDefinition>());
            AircraftEntities = GetGameEntitiesByRulesTypesSection("AircraftTypes",
                new List<CategorizedValueDefinition>());
            InfantryEntities = GetGameEntitiesByRulesTypesSection("InfantryTypes",
                new List<CategorizedValueDefinition>());
            BuildingEntities = GetGameEntitiesByRulesTypesSection("BuildingTypes",
                new List<CategorizedValueDefinition>());
        }

        private List<GameEntityModel> GetGameEntitiesByRulesTypesSection(string rulesTypesSection,
            List<CategorizedValueDefinition> unitValueList)
        {
            var entityKeys = (RulesRootModel.File.GetSection(rulesTypesSection)?.KeyValues.Select(k => k.Value)
                              ?? Enumerable.Empty<string>())
                .Union(RulesRootModel.DefaultFile.GetSection(rulesTypesSection)?.KeyValues.Select(k => k.Value)
                       ?? Enumerable.Empty<string>());
            return GetGameEntities(rulesTypesSection, entityKeys, unitValueList);
        }

        private List<GameEntityModel> GetGameEntities(string entityType,
            IEnumerable<string> entityKeysList,
            List<CategorizedValueDefinition> unitValueList)
        {
            var result = new List<GameEntityModel>();
            foreach (var entityKey in entityKeysList.OrderBy(e => e))
            {
                var fileSection = File.GetSection(entityKey);
                var defaultSection = DefaultFile.GetSection(entityKey);
                if (fileSection != null)
                {
                    result.Add(new GameEntityModel(RulesRootModel,
                        entityType,
                        fileSection,
                        defaultSection,
                        unitValueList));
                }
                else if (defaultSection != null && _showMissingValues)
                {
                    result.Add(new GameEntityModel(RulesRootModel,
                        entityType,
                        File.AddSection(entityKey),
                        defaultSection,
                        unitValueList));
                }
                //LookupItems.Add(new LookupItemModel(entityType, entityKey,
                //    fileSection?.GetValue("Name")?.Value
                //    ?? (defaultSection ?? fileSection)?.HeaderComments.FirstOrDefault()?.Comment
                //    ?? ""));
            }
            //LookupEntities.Add(entityType, result);
            return result;
        }

    }
}
