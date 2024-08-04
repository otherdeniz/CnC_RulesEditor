using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class RootModel
    {
        private readonly bool _showMissingValues;
        private List<KeyValuePair<string, List<string>>>? _sides;

        public RootModel(IniFile iniFile, 
            FileTypeModel fileType, 
            IniFile? defaultFileOverwrite = null,
            bool showMissingValues = false,
            DatastructureFile? datastructureOverwrite = null,
            bool useAres = false,
            bool usePhobos = false)
        {
            _showMissingValues = showMissingValues;
            File = iniFile;
            FileType = fileType;
            UseAres = useAres;
            UsePhobos = usePhobos;
            if (datastructureOverwrite == null)
            {
                Datastructure = DatastructureFile.Instance;
                if (useAres)
                {
                    Datastructure = Datastructure.MergeWith(DatastructureFile.InstanceAres);
                }
                if (usePhobos)
                {
                    Datastructure = Datastructure.MergeWith(DatastructurePhobosFile.InstancePhobos);
                }
            }
            else
            {
                Datastructure = datastructureOverwrite;
            }
            DefaultFile = defaultFileOverwrite ?? GetDefaultFile(FileType.GameDefinition);
            DescriptionFile = GetDescriptionFile(FileType.GameDefinition);
            LoadGameEntities();
            InitialiseLookupItems();
            CommonValues = GetCommonValues(Datastructure.CommonGeneral)
                .Union(GetOtherValues("General"))
                .ToList();
            TiberiumValues = GetCommonValues(Datastructure.TiberiumGeneral)
                .Union(GetTiberiumValues(Datastructure.TiberiumValues))
                .ToList();
            AiValues = GetCommonValues(Datastructure.AIGeneral)
                .Union(GetOtherValues("AI"))
                .ToList();
            SuperWeaponValues = GetCommonValues(Datastructure.SuperWeaponsGeneral)
                .Union(GetOtherValues("SpecialWeapons"))
                .ToList();
            AudioVisualValues = GetAllSectionValues("AudioVisual");
        }

        public event EventHandler<EventArgs>? EntitiesChanged;
        
        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public IniFile? DescriptionFile { get; }

        public FileTypeModel FileType { get; }

        public DatastructureFile Datastructure { get; }

        public bool UseAres { get; }

        public bool UsePhobos { get; }

        public List<KeyValuePair<string, List<string>>> Sides => _sides
            ??= DefaultFile.GetSection("Sides")?.KeyValues
                    .Select(k => new KeyValuePair<string, List<string>>(
                        int.TryParse(k.Key, out _)
                            ? k.Value.Split(",").First()
                            : k.Key,
                        k.Value.Split(",").ToList()))
                    .ToList()
                ?? new List<KeyValuePair<string, List<string>>>
                {
                    new("GDI", new List<string> { "GDI" }),
                    new("Nod", new List<string> { "Nod" })
                };

        public List<CommonValueModel> CommonValues { get; }

        public List<CommonValueModel> AiValues { get; }

        public List<CommonValueModel> SuperWeaponValues { get; }

        public List<CommonValueModel> TiberiumValues { get; }

        public List<CommonValueModel> AudioVisualValues { get; }

        public List<GameEntityModel> SideEntities { get; private set; } = null!;

        public List<GameEntityModel> VehicleEntities { get; private set; } = null!;

        public List<GameEntityModel> AircraftEntities { get; private set; } = null!;

        public List<GameEntityModel> InfantryEntities { get; private set; } = null!;

        public List<GameEntityModel> BuildingEntities { get; private set; } = null!;

        public List<GameEntityModel> WarheadEntities { get; private set; } = null!;

        public List<GameEntityModel> WeaponEntities { get; private set; } = null!;

        public List<GameEntityModel> ProjectileEntities { get; private set; } = null!;

        public List<GameEntityModel> SuperWeaponEntities { get; private set; } = null!;

        public List<GameEntityModel> VoxelDebrisEntities { get; private set; } = null!;

        public List<GameEntityModel> ParticleEntities { get; private set; } = null!;

        public List<GameEntityModel> ParticleSystemEntities { get; private set; } = null!;

        public List<AdditionalGameEntityModels> AdditionalEntities { get; private set; } = null!;

        public List<string> Animations { get; private set; } = null!;

        public List<string> MovementZones { get; private set; } = null!;

        public List<string> WeaponSounds { get; private set; } = null!;

        public List<string> WeaponProjectiles { get; private set; } = null!;

        public List<string> Houses  { get; private set; } = null!;

        public List<LookupItemModel> LookupItems { get; private set; } = null!;

        public Dictionary<string, List<GameEntityModel>> LookupEntities { get; } = new();

        public IniFileSection? FindSection(string name)
        {
            return File.GetSection(name) ?? DefaultFile.GetSection(name);
        }
        public void ReloadGameEntites()
        {
            LoadGameEntities();
            InitialiseLookupItems();
            EntitiesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LoadGameEntities()
        {
            LookupItems = new List<LookupItemModel>();
            LookupEntities.Clear();

            var allUnitsModel = Datastructure.AllUnits.Where(GameKeyFilter).Select(u =>
                new UnitValueModel(u, "1) All units")).ToList();
            var movingUnitsModel = Datastructure.AllMovingUnits.Select(u =>
                new UnitValueModel(u, "2) All moving units")).ToList();

            var sidesEntityKeys = (File.GetSection("Sides")?.KeyValues.SelectMany(k => k.Value.Split(",")).ToList()
                                   ?? new List<string>())
                .Union(DefaultFile.GetSection("Sides")?.KeyValues.SelectMany(k => k.Value.Split(",")).ToList()
                       ?? new List<string>());
            SideEntities = GetGameEntities("Sides", sidesEntityKeys,
                Datastructure.Sides.Select(u =>
                        new UnitValueModel(u, "1) Side"))
                    .ToList());
            VehicleEntities = GetGameEntities("VehicleTypes",
                allUnitsModel.Union(movingUnitsModel)
                    .Union(Datastructure.DrivingVehicleUnits.Select(u =>
                        new UnitValueModel(u, "3) Vehicle units")))
                    .ToList());
            AircraftEntities = GetGameEntities("AircraftTypes",
                allUnitsModel.Union(movingUnitsModel)
                    .Union(Datastructure.AircraftUnits.Select(u =>
                        new UnitValueModel(u, "3) Aircraft units")))
                    .ToList());
            InfantryEntities = GetGameEntities("InfantryTypes",
                allUnitsModel.Union(movingUnitsModel)
                    .Union(Datastructure.InfantryUnits.Select(u =>
                        new UnitValueModel(u, "3) Infantry units")))
                    .ToList());
            BuildingEntities = GetGameEntities("BuildingTypes",
                allUnitsModel
                    .Union(Datastructure.BuildingUnits.Select(u =>
                        new UnitValueModel(u, "2) Building units")))
                    .ToList());
            WarheadEntities = GetGameEntities("Warheads",
                Datastructure.Warheads.Select(u =>
                        new UnitValueModel(u, "1) Warheads"))
                    .ToList());
            WeaponEntities = GetGameEntities("Weapons",
                s => (s.KeyValues.Any(k => k.Key == "Warhead")
                      && s.KeyValues.Any(k => k.Key == "Damage")
                      && s.KeyValues.Any(k => k.Key == "Projectile"))
                || (DefaultFile.GetSection(s.GetValue("BaseSection")?.Value ?? s.SectionName) is { } defaultSection
                    && (defaultSection.KeyValues.Any(k => k.Key == "Warhead")
                        && defaultSection.KeyValues.Any(k => k.Key == "Damage")
                        && defaultSection.KeyValues.Any(k => k.Key == "Projectile")
                        || (defaultSection.GetValue("BaseSection") is { } baseValue &&
                            DefaultFile.GetSection(baseValue.Value) is { } baseSection
                            && baseSection.KeyValues.Any(k => k.Key == "Warhead")
                            && baseSection.KeyValues.Any(k => k.Key == "Damage")
                            && baseSection.KeyValues.Any(k => k.Key == "Projectile")))
                    ),
                Datastructure.Weapons.Select(u =>
                        new UnitValueModel(u, "1) Weapons"))
                    .ToList());
            SuperWeaponEntities = GetGameEntities("SuperWeaponTypes",
                Datastructure.SuperWeapons.Select(u =>
                    new UnitValueModel(u, "1) Super Weapons"))
                    .ToList());
            WeaponSounds = GetAllPossibleValues("Weapons", "Report");
            WeaponProjectiles = GetAllPossibleValues("Weapons", "Projectile");
            ProjectileEntities = GetGameEntities("Projectiles",
                s => WeaponProjectiles.Any(p => s.SectionName == p)
                     || (s.KeyValues.Any(k => k.Key == "AA")
                         && s.KeyValues.Any(k => k.Key == "AG")
                         && s.KeyValues.Any(k => k.Key == "Image"))
                     || (DefaultFile.GetSection(s.SectionName) is { } defaultSection
                         && (defaultSection.KeyValues.Any(k => k.Key == "AA")
                             && defaultSection.KeyValues.Any(k => k.Key == "AG")
                             && defaultSection.KeyValues.Any(k => k.Key == "Image"))
                     ),
                Datastructure.Projectiles.Select(u =>
                        new UnitValueModel(u, "1) Projectiles"))
                    .ToList());
            var housesSection = DefaultFile.GetSection("Houses") ?? DefaultFile.GetSection("Countries");
            Houses = housesSection?.KeyValues.Select(k => k.Value).Distinct().ToList()
                     ?? new List<string>() { "GDI", "Nod" };
            Animations = DefaultFile.GetSection("Animations")?.KeyValues.Select(k => k.Value).ToList()
                         ?? GetAllPossibleValues("Warheads", "AnimList", false);
            MovementZones = GetAllPossibleValues(
                new List<string>()
                {
                    "VehicleTypes",
                    "AircraftTypes",
                    "InfantryTypes"
                }, "MovementZone");
            VoxelDebrisEntities = GetGameEntities("VoxelAnims", new List<UnitValueModel>());
            ParticleEntities = GetGameEntities("Particles", new List<UnitValueModel>());
            ParticleSystemEntities = GetGameEntities("ParticleSystems", new List<UnitValueModel>());
            // additional entities
            AdditionalEntities = new List<AdditionalGameEntityModels>();
            foreach (var additionalType in Datastructure.AdditionalTypes)
            {
                var gameEntities = GetGameEntities(additionalType.TypesName,
                    additionalType.ValueDefinitions
                        .Select(d => new UnitValueModel(d, $"{additionalType.Module}: {d.ModuleCategory}")).ToList());
                AdditionalEntities.Add(new AdditionalGameEntityModels(additionalType.Module,
                    additionalType.TypesName, gameEntities));
            }
        }

        private void InitialiseLookupItems()
        {
            CCGameRepository.Instance.GetAllSounds().ForEach(s =>
                LookupItems.Add(new LookupItemModel("Sounds", s.Key, s.Value)));
        }

        private static IniFile GetDefaultFile(GameDefinition gameDefinition)
        {
            return !string.IsNullOrEmpty(gameDefinition.ResourcesDefaultIniFile) 
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(gameDefinition.ResourcesDefaultIniFile)) 
                : new IniFile();
        }

        private static IniFile? GetDescriptionFile(GameDefinition gameDefinition)
        {
            return !string.IsNullOrEmpty(gameDefinition.ResourcesDescriptionIniFile)
                ? IniFile.Load(ResourcesRepository.Instance.ReadResourcesFile(gameDefinition.ResourcesDescriptionIniFile))
                : null;
        }

        private List<CommonValueModel> GetCommonValues(List<CommonValueDefinition> valueDefinitions)
        {
            var result = new List<CommonValueModel>();
            foreach (var valueDefinition in valueDefinitions)
            {
                var fileValue = File.GetSection(valueDefinition.Section)?.GetValue(valueDefinition.Key);
                var defaultValue = DefaultFile.GetSection(valueDefinition.Section)?.GetValue(valueDefinition.Key);
                if (fileValue != null)
                {
                    result.Add(new CommonValueModel(
                        valueDefinition,
                        valueDefinition.Description ?? defaultValue?.Comment ?? fileValue.Comment ??
                        DescriptionFile?.GetSection(valueDefinition.Section)?.GetValue(valueDefinition.Key)?.Comment ?? "",
                        fileValue,
                        defaultValue?.Value ?? ""));
                }
                else if (_showMissingValues)
                {
                    result.Add(new CommonValueModel(
                        valueDefinition,
                        valueDefinition.Description ?? defaultValue?.Comment ??
                        DescriptionFile?.GetSection(valueDefinition.Section)?.GetValue(valueDefinition.Key)?.Comment ?? "",
                        File,
                        valueDefinition.Section,
                        valueDefinition.Key,
                        defaultValue != null ? defaultValue.Value : valueDefinition.Default));
                }
            }
            return result;
        }

        private List<CommonValueModel> GetOtherValues(string mainSection)
        {
            var result = new List<CommonValueModel>();
            var generalSection = File.GetSection(mainSection);
            var defaultSection = DefaultFile.GetSection(mainSection);
            if (generalSection != null)
            {
                foreach (var sectionValue in generalSection.KeyValues)
                {
                    if (!Datastructure.CommonGeneral.Any(v =>
                            v.Section == mainSection && v.Key == sectionValue.Key)
                        && !Datastructure.AIGeneral.Any(v =>
                            v.Section == mainSection && v.Key == sectionValue.Key)
                        && !Datastructure.SuperWeaponsGeneral.Any(v =>
                            v.Section == mainSection && v.Key == sectionValue.Key))
                    {
                        var defaultValue = defaultSection?.GetValue(sectionValue.Key);
                        var detectByKey = !string.IsNullOrEmpty(sectionValue.Value)
                            ? sectionValue.Value.Split(",").First()
                            : !string.IsNullOrEmpty(defaultValue?.Value)
                                ? defaultValue.Value.Split(",").First()
                                : null;
                        var detectedLookupType = detectByKey != null
                            ? DetectLookupType(detectByKey)
                            : null;
                        var multipleValues = !string.IsNullOrEmpty(sectionValue.Value) && sectionValue.Value.Contains(",")
                                             || !string.IsNullOrEmpty(defaultValue?.Value) && defaultValue.Value.Contains(",");
                        result.Add(new CommonValueModel(
                            new CommonValueDefinition
                            {
                                Key = sectionValue.Key,
                                Category = $"Z) Other '{mainSection}' values",
                                Section = mainSection,
                                LookupType = detectedLookupType,
                                MultipleValues = multipleValues
                            },
                            defaultValue?.Comment ?? sectionValue.Comment ?? "",
                            sectionValue,
                            defaultValue?.Value ?? ""));
                    }
                }
            }
            return result;
        }

        private List<CommonValueModel> GetTiberiumValues(List<CommonValueDefinition> valueDefinitions)
        {
            const string typesSection = "Tiberiums";
            var result = new List<CommonValueModel>();
            var entityKeysList = (File.GetSection(typesSection)?.KeyValues.Select(k => k.Value).ToList()
                                  ?? new List<string>())
                .Union(DefaultFile.GetSection(typesSection)?.KeyValues.Select(k => k.Value).ToList()
                       ?? new List<string>());
            foreach (var entityKey in entityKeysList.OrderBy(e => e))
            {
                var fileSection = File.GetSection(entityKey);
                var defaultSection = DefaultFile.GetSection(entityKey);
                if (fileSection != null)
                {
                    foreach (var commonValue in valueDefinitions)
                    {
                        var fileValue = fileSection.GetValue(commonValue.Key);
                        var defaultValue = defaultSection?.GetValue(commonValue.Key);
                        if (fileValue != null)
                        {
                            var category = (defaultSection ?? fileSection).GetNameValue();
                            if (!category.Contains(entityKey))
                            {
                                category = $"{entityKey} - {category}";
                            }
                            var headerDescription = (defaultSection ?? fileSection).GetHeaderDescription();
                            if (!string.IsNullOrEmpty(headerDescription))
                            {
                                category += $" - {headerDescription}";
                            }
                            result.Add(new CommonValueModel(
                                commonValue,
                                commonValue.Description ?? defaultValue?.Comment ?? fileValue.Comment ??
                                DescriptionFile?.GetSection(entityKey)?.GetValue(commonValue.Key)?.Comment ?? "",
                                fileValue,
                                defaultValue?.Value ?? "",
                                entityKey,
                                category));
                        }
                    }
                }
            }
            return result;
        }

        private List<CommonValueModel> GetAllSectionValues(string mainSection)
        {
            var result = new List<CommonValueModel>();
            var fileSection = File.GetSection(mainSection);
            var defaultSection = DefaultFile.GetSection(mainSection);
            if (fileSection != null)
            {
                foreach (var sectionValue in fileSection.KeyValues)
                {
                    var defaultValue = defaultSection?.GetValue(sectionValue.Key);
                    var detectByKey = !string.IsNullOrEmpty(sectionValue.Value)
                        ? sectionValue.Value.Split(",").First()
                        : !string.IsNullOrEmpty(defaultValue?.Value)
                            ? defaultValue.Value.Split(",").First()
                            : null;
                    var detectedLookupType = detectByKey != null
                        ? DetectLookupType(detectByKey)
                        : null;
                    var multipleValues = !string.IsNullOrEmpty(sectionValue.Value) && sectionValue.Value.Contains(",")
                                         || !string.IsNullOrEmpty(defaultValue?.Value) && defaultValue.Value.Contains(",");
                    result.Add(new CommonValueModel(
                        new CommonValueDefinition
                        {
                            Key = sectionValue.Key,
                            Category = $"'{mainSection}' values",
                            Section = mainSection,
                            LookupType = detectedLookupType,
                            MultipleValues = multipleValues
                        },
                        defaultValue?.Comment ?? sectionValue.Comment ?? "",
                        sectionValue,
                        defaultValue?.Value ?? ""));

                }
            }

            return result;
        }

        public string? DetectLookupType(string valueKey)
        {
            var lookupEntityType = LookupItems.FirstOrDefault(l => 
                string.Equals(l.Key, valueKey, StringComparison.InvariantCultureIgnoreCase))?.EntityType;
            if (lookupEntityType != null)
            {
                return lookupEntityType;
            }
            if (Animations.Contains(valueKey, StringEqualityComparer.Instance))
            {
                return "Animations";
            }
            return null;
        }

        private List<GameEntityModel> GetGameEntities(string entitiesTypesSection, 
            List<UnitValueModel> unitValueList)
        {
            var entityKeys = (File.GetSection(entitiesTypesSection)?.KeyValues.Select(k => k.Value).ToList()
                                  ?? new List<string>())
                .Union(DefaultFile.GetSection(entitiesTypesSection)?.KeyValues.Select(k => k.Value).ToList()
                       ?? new List<string>());
            return GetGameEntities(entitiesTypesSection, entityKeys, unitValueList);
        }

        private List<GameEntityModel> GetGameEntities(string entityType,
            IEnumerable<string> entityKeysList,
            List<UnitValueModel> unitValueList)
        {
            var result = new List<GameEntityModel>();
            foreach (var entityKey in entityKeysList.OrderBy(e => e))
            {
                var fileSection = File.GetSection(entityKey);
                var defaultSection = DefaultFile.GetSection(entityKey);
                if (fileSection != null)
                {
                    result.Add(new GameEntityModel(this,
                        entityType,
                        fileSection,
                        defaultSection,
                        unitValueList));
                }
                else if (defaultSection != null && _showMissingValues)
                {
                    result.Add(new GameEntityModel(this,
                        entityType,
                        File.AddSection(entityKey),
                        defaultSection,
                        unitValueList));
                }
                LookupItems.Add(new LookupItemModel(entityType, entityKey,
                    fileSection?.GetValue("Name")?.Value
                    ?? (defaultSection ?? fileSection)?.HeaderComments.FirstOrDefault()?.Comment
                    ?? ""));
            }
            LookupEntities.Add(entityType, result);
            return result;
        }

        private List<GameEntityModel> GetGameEntities(string entityType, 
            Func<IniFileSection, bool> sectionFilter,
            List<UnitValueModel> unitValueList)
        {
            var result = new List<GameEntityModel>();
            var fileSections = File.Sections.Where(sectionFilter).ToList();
            foreach (var fileSection in fileSections)
            {
                result.Add(new GameEntityModel(this,
                    entityType,
                    fileSection,
                    DefaultFile.GetSection(fileSection.SectionName),
                    unitValueList));
            }
            foreach (var fileSection in fileSections.Union(DefaultFile.Sections.Where(sectionFilter), new SectionEqualityComparer())
                         .OrderBy(s => s.SectionName))
            {
                var defaultSection = DefaultFile.GetSection(fileSection.SectionName);
                LookupItems.Add(new LookupItemModel(entityType, fileSection.SectionName ?? "",
                    fileSection?.GetValue("Name")?.Value
                    ?? (defaultSection ?? fileSection)?.HeaderComments.FirstOrDefault()?.Comment
                    ?? ""));
            }
            var sortedResult = result.OrderBy(e => e.EntityKey).ToList();
            LookupEntities.Add(entityType, sortedResult);
            return sortedResult;
        }

        public List<string> GetAllPossibleValues(string searchEntityType, string valueKey, bool orderByName = true)
        {
            return GetAllPossibleValues(new List<string> { searchEntityType }, valueKey, orderByName);
        }

        public List<string> GetAllPossibleValues(List<string> searchEntityTypes, string valueKey, bool orderByName = true)
        {
            var allSectionKeys = LookupItems
                .Where(l => searchEntityTypes.Contains(l.EntityType, StringEqualityComparer.Instance))
                .Select(l => l.Key)
                .ToList();
            var distinctValues =
                allSectionKeys.Select(s =>
                        File.GetSection(s)?.GetValue(valueKey)?.Value)
                    .Where(v => !string.IsNullOrEmpty(v))
                    .SelectMany(v => v!.Split(",", StringSplitOptions.RemoveEmptyEntries))
                    .Union(
                        allSectionKeys.Select(s =>
                                DefaultFile.GetSection(s)?.GetValue(valueKey)?.Value)
                            .Where(v => !string.IsNullOrEmpty(v))
                            .SelectMany(v => v!.Split(",", StringSplitOptions.RemoveEmptyEntries))
                    )
                    .Distinct();
            return orderByName
                ? distinctValues.OrderBy(s => s).ToList()
                : distinctValues.ToList();
        }

        private bool GameKeyFilter(UnitValueDefinition valueDefinition)
        {
            if (valueDefinition.GameKeyFilter != null)
            {
                return valueDefinition.GameKeyFilter.Split(",").Any(t => t == FileType.GameDefinition.GameKey);
            }
            return true;
        }

        private class SectionEqualityComparer : IEqualityComparer<IniFileSection>
        {
            public bool Equals(IniFileSection? x, IniFileSection? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                return x.SectionName == y.SectionName;
            }

            public int GetHashCode(IniFileSection obj)
            {
                return (obj.SectionName != null ? obj.SectionName.GetHashCode() : 0);
            }
        }
    }
}
