using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class ArtRootModel : IRootModel
    {
        private readonly bool _showMissingValues;

        public ArtRootModel(RulesRootModel rulesRootModel,
            IniFile iniFile,
            IniFile? defaultFileOverwrite = null,
            bool showMissingValues = false)
        {
            _showMissingValues = showMissingValues;
            RulesModel = rulesRootModel;
            File = iniFile;
            DefaultFile = defaultFileOverwrite ?? rulesRootModel.FileType.GameDefinition.LoadDefaultArtFile();
            Artstructure = DatastructureFile.ArtInstance;
            if (rulesRootModel.UsePhobos)
            {
                Artstructure = Artstructure.MergeWith(DatastructurePhobosFile.ArtInstancePhobos);
            }
            LoadGameEntities();
        }

        public event EventHandler<EventArgs>? EntitiesChanged;

        public RulesRootModel RulesModel { get; }

        public FileTypeModel FileType => RulesModel.FileType;

        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public DatastructureFile Artstructure { get; }

        public List<GameEntityModel> VehicleEntities { get; private set; } = null!;

        public List<GameEntityModel> AircraftEntities { get; private set; } = null!;

        public List<GameEntityModel> InfantryEntities { get; private set; } = null!;

        public List<GameEntityModel> BuildingEntities { get; private set; } = null!;

        public List<GameEntityModel> ProjectileEntities { get; private set; } = null!;

        public List<GameEntityModel> AnimationEntities { get; private set; } = null!;

        public List<AdditionalGameEntityModels> AdditionalEntities { get; private set; } = null!;

        public List<LookupItemModel> LookupItems => RulesModel.LookupItems;

        public Dictionary<string, List<GameEntityModel>> LookupEntities { get; } = new();

        public void ReloadGameEntites()
        {
            LoadGameEntities();
            EntitiesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LoadGameEntities()
        {
            var allUnitsDefinitions = Artstructure.AllUnits.Select(u =>
                new CategorizedValueDefinition(u, "1) All units")).ToList();

            var buildingValueDefinitions = allUnitsDefinitions
                .Union(Artstructure.BuildingUnits.Select(u =>
                    new CategorizedValueDefinition(u, "2) Building units")))
                .ToList();
            BuildingEntities = GetGameEntitiesByRulesTypesSection("BuildingTypes", buildingValueDefinitions)
                .UnionBy(GetGameEntitiesBySectionFilter("BuildingTypes",
                        s => s.KeyValues.Any(k => k.Key == "Foundation"), buildingValueDefinitions),
                    k => k.EntityKey)
                .ToList();

            var vehicleValueDefinitions = allUnitsDefinitions
                .Union(Artstructure.DrivingVehicleUnits.Select(u =>
                    new CategorizedValueDefinition(u, "2) Vehicle units")))
                .ToList();
            VehicleEntities = GetGameEntitiesByRulesTypesSection("VehicleTypes", vehicleValueDefinitions)
                .UnionBy(GetGameEntitiesBySectionFilter("VehicleTypes",
                        s => s.KeyValues.Any(k => k.Key == "Voxel"), vehicleValueDefinitions),
                    k => k.EntityKey)
                .ToList();

            var infantryValueDefinitions = allUnitsDefinitions
                .Union(Artstructure.InfantryUnits.Select(u =>
                    new CategorizedValueDefinition(u, "2) Infantry units")))
                .ToList();
            InfantryEntities = GetGameEntitiesByRulesTypesSection("InfantryTypes", infantryValueDefinitions)
                .UnionBy(GetGameEntitiesBySectionFilter("InfantryTypes",
                        s => s.KeyValues.Any(k => k.Key == "Crawls"), infantryValueDefinitions),
                    k => k.EntityKey)
                .ToList();

            var aircraftValueDefinitions = allUnitsDefinitions
                .Union(Artstructure.AircraftUnits.Select(u =>
                    new CategorizedValueDefinition(u, "2) Aircraft units")))
                .ToList();
            AircraftEntities = GetGameEntitiesByRulesTypesSection("AircraftTypes", aircraftValueDefinitions);

            var projectileValueDefinitions = Artstructure.Projectiles.Select(u =>
                    new CategorizedValueDefinition(u, "1) Projectiles Image"))
                .ToList();
            ProjectileEntities = GetGameEntities("Projectiles",
                RulesModel.ProjectileEntities
                    .Select(e => e.FileSection.GetValue("Image")?.Value)
                    .Where(v => !string.IsNullOrEmpty(v))
                    .Select(v => v!)
                    .Distinct(), 
                projectileValueDefinitions);

            var animationValueDefinitions = Artstructure.Animations.Select(u =>
                    new CategorizedValueDefinition(u, "1) Animations"))
                .ToList();
            AnimationEntities = GetGameEntities("Animations",
                RulesModel.Animations,
                animationValueDefinitions);

            // additional entities
            AdditionalEntities = new List<AdditionalGameEntityModels>();
            foreach (var additionalType in Artstructure.AdditionalTypes)
            {
                var gameEntities = GetGameEntitiesByArtTypesSection(additionalType.TypesName,
                    additionalType.ValueDefinitions
                        .Select(d => new CategorizedValueDefinition(d, $"{additionalType.Module}: {d.ModuleCategory}")).ToList());
                AdditionalEntities.Add(new AdditionalGameEntityModels(additionalType.Module,
                    additionalType.TypesName, gameEntities));
            }
        }

        private List<GameEntityModel> GetGameEntitiesByRulesTypesSection(string rulesTypesSection,
            List<CategorizedValueDefinition> unitValueList)
        {
            var entityKeys = (RulesModel.File.GetSection(rulesTypesSection)?.KeyValues.Select(k => k.Value)
                              ?? Enumerable.Empty<string>())
                .Union(RulesModel.DefaultFile.GetSection(rulesTypesSection)?.KeyValues.Select(k => k.Value)
                       ?? Enumerable.Empty<string>());
            return GetGameEntities(rulesTypesSection, entityKeys, unitValueList);
        }

        private List<GameEntityModel> GetGameEntitiesByArtTypesSection(string artTypesSection,
            List<CategorizedValueDefinition> unitValueList)
        {
            var entityKeys = (File.GetSection(artTypesSection)?.KeyValues.Select(k => k.Value)
                              ?? Enumerable.Empty<string>())
                .Union(DefaultFile.GetSection(artTypesSection)?.KeyValues.Select(k => k.Value)
                       ?? Enumerable.Empty<string>());
            var artEntities = GetGameEntities(artTypesSection, entityKeys, unitValueList);
            LookupItems.AddRange(artEntities.Select(e =>
                new LookupItemModel(e.EntityType, e.EntityKey,
                    e.FileSection?.GetValue("Name")?.Value
                    ?? (e.DefaultSection ?? e.FileSection)?.HeaderComments.FirstOrDefault()?.Comment
                    ?? "")));
            return artEntities;
        }

        private List<GameEntityModel> GetGameEntitiesBySectionFilter(string entityType,
            Func<IniFileSection, bool> sectionFilter,
            List<CategorizedValueDefinition> unitValueList)
        {
            var entityKeys = File.Sections.Where(sectionFilter).Select(s => s.SectionName!);
            return GetGameEntities(entityType, entityKeys, unitValueList);
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
                var rulesSection = RulesModel.File.GetSection(entityKey);
                if (fileSection != null)
                {
                    result.Add(new GameEntityModel(RulesModel, this,
                        entityType,
                        fileSection,
                        defaultSection,
                        unitValueList,
                        rulesSection));
                }
                else if (defaultSection != null && _showMissingValues)
                {
                    result.Add(new GameEntityModel(RulesModel, this,
                        entityType,
                        File.AddSection(entityKey),
                        defaultSection,
                        unitValueList,
                        rulesSection));
                }
            }
            if (LookupEntities.TryGetValue(entityType, out var existingLookupEntities))
            {
                existingLookupEntities.AddRange(result);
            }
            else
            {
                LookupEntities.Add(entityType, result);
            }
            return result;
        }

    }
}
