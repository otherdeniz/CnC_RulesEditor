﻿using Deniz.TiberiumSunEditor.Gui.Model.Extensions;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;
using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class RulesRootModel : IRootModel
    {
        private readonly bool _showMissingValues;
        private List<KeyValuePair<string, List<string>>>? _sides;

        public RulesRootModel(IniFile iniFile, 
            FileTypeModel fileType, 
            IniFile? defaultFileOverwrite = null,
            bool showMissingValues = false,
            DatastructureFile? datastructureOverwrite = null,
            bool useAres = false,
            bool usePhobos = false,
            bool usePhobosSectionInheritance = false,
            bool useVinifera = false,
            bool useSectionInheritance = false)
        {
            _showMissingValues = showMissingValues;
            File = iniFile;
            FileType = fileType;
            UseAres = useAres;
            UsePhobos = usePhobos;
            UsePhobosSectionInheritance = usePhobosSectionInheritance;
            UseVinifera = useVinifera;
            UseSectionInheritance = useSectionInheritance;
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
                if (useVinifera)
                {
                    Datastructure = Datastructure.MergeWith(DatastructureFile.InstanceVinifera);
                }
            }
            else
            {
                Datastructure = datastructureOverwrite;
            }
            DefaultFile = defaultFileOverwrite ?? FileType.GameDefinition.LoadDefaultRulesFile();
            DescriptionFile = FileType.GameDefinition.LoadDescriptionRulesFile();
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

        public event EventHandler<EventArgs>? EntitiesReloaded;
        public event EventHandler<GlobalEntityNotificationEventArgs>? GlobalEntityNotification;

        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public IniFile? DescriptionFile { get; }

        public FileTypeModel FileType { get; }

        public RulesRootModel RulesModel => this;

        public DatastructureFile Datastructure { get; }

        public bool UseAres { get; }

        public bool UsePhobos { get; }

        public bool UsePhobosSectionInheritance { get; }

        public bool UseVinifera { get; }

        public bool UseSectionInheritance { get; }

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

        public List<EntityTypeEditControlTypeModel> EntityTypeEditControl { get; } = new();

        public void RaiseGlobalEntityNotification(string entitiyKey, string notificationName)
        {
            GlobalEntityNotification?.Invoke(this, new GlobalEntityNotificationEventArgs(entitiyKey, notificationName));
        }

        public IniFileSection? FindSection(string name)
        {
            return File.GetSection(name) ?? DefaultFile.GetSection(name);
        }
        public void ReloadGameEntites()
        {
            LoadGameEntities();
            InitialiseLookupItems();
            EntitiesReloaded?.Invoke(this, EventArgs.Empty);
        }

        private void LoadGameEntities()
        {
            LookupItems = new List<LookupItemModel>();
            LookupEntities.Clear();

            var allUnitsDefinitions = Datastructure.AllUnits.Where(GameKeyFilter).Select(u =>
                new CategorizedValueDefinition(u, "1) All units")).ToList();
            var movingUnitsDefinitions = Datastructure.AllMovingUnits.Select(u =>
                new CategorizedValueDefinition(u, "2) All moving units")).ToList();

            var sidesEntityKeys = (File.GetSection("Sides")?.KeyValues.SelectMany(k => k.Value.Split(",")).ToList()
                                   ?? new List<string>())
                .Union(DefaultFile.GetSection("Sides")?.KeyValues.SelectMany(k => k.Value.Split(",")).ToList()
                       ?? new List<string>());
            SideEntities = GetGameEntities("Sides", sidesEntityKeys,
                Datastructure.Sides.Select(u =>
                        new CategorizedValueDefinition(u, "1) Side"))
                    .ToList());
            VehicleEntities = GetGameEntities("VehicleTypes",
                allUnitsDefinitions.Union(movingUnitsDefinitions)
                    .Union(Datastructure.DrivingVehicleUnits.Select(u =>
                        new CategorizedValueDefinition(u, "3) Vehicle units")))
                    .ToList());
            AircraftEntities = GetGameEntities("AircraftTypes",
                allUnitsDefinitions.Union(movingUnitsDefinitions)
                    .Union(Datastructure.AircraftUnits.Select(u =>
                        new CategorizedValueDefinition(u, "3) Aircraft units")))
                    .ToList());
            InfantryEntities = GetGameEntities("InfantryTypes",
                allUnitsDefinitions.Union(movingUnitsDefinitions)
                    .Union(Datastructure.InfantryUnits.Select(u =>
                        new CategorizedValueDefinition(u, "3) Infantry units")))
                    .ToList());
            BuildingEntities = GetGameEntities("BuildingTypes",
                allUnitsDefinitions
                    .Union(Datastructure.BuildingUnits.Select(u =>
                        new CategorizedValueDefinition(u, "2) Building units")))
                    .ToList());
            WarheadEntities = GetGameEntities("Warheads",
                Datastructure.Warheads.Select(u =>
                        new CategorizedValueDefinition(u, "1) Warheads"))
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
                        new CategorizedValueDefinition(u, "1) Weapons"))
                    .ToList());
            SuperWeaponEntities = GetGameEntities("SuperWeaponTypes",
                Datastructure.SuperWeapons.Select(u =>
                    new CategorizedValueDefinition(u, "1) Super Weapons"))
                    .ToList());
            WeaponSounds = this.GetAllPossibleValues("Weapons", "Report");
            WeaponProjectiles = this.GetAllPossibleValues("Weapons", "Projectile");
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
                        new CategorizedValueDefinition(u, "1) Projectiles"))
                    .ToList());
            var housesSection = DefaultFile.GetSection("Houses") ?? DefaultFile.GetSection("Countries");
            Houses = housesSection?.KeyValues.Select(k => k.Value).Distinct().ToList()
                     ?? new List<string>() { "GDI", "Nod" };
            Animations = DefaultFile.GetSection("Animations")?.KeyValues.Select(k => k.Value).ToList()
                         ?? this.GetAllPossibleValues("Warheads", "AnimList", false);
            Animations.ForEach(a => LookupItems.Add(new LookupItemModel("Animations", a, string.Empty)));
            MovementZones = this.GetAllPossibleValues(
                new List<string>()
                {
                    "VehicleTypes",
                    "AircraftTypes",
                    "InfantryTypes"
                }, "MovementZone");
            VoxelDebrisEntities = GetGameEntities("VoxelAnims", CategorizedValueDefinition.EmptyList);
            ParticleEntities = GetGameEntities("Particles", CategorizedValueDefinition.EmptyList);
            ParticleSystemEntities = GetGameEntities("ParticleSystems", CategorizedValueDefinition.EmptyList);
            // additional entities
            AdditionalEntities = new List<AdditionalGameEntityModels>();
            foreach (var additionalType in Datastructure.AdditionalTypes)
            {
                var gameEntities = GetGameEntities(additionalType.TypesName,
                    additionalType.ValueDefinitions
                        .Select(d => new CategorizedValueDefinition(d, $"{additionalType.Module}: {d.ModuleCategory}")).ToList());
                AdditionalEntities.Add(new AdditionalGameEntityModels(additionalType.Module,
                    additionalType.TypesName, gameEntities));
            }
        }

        private void InitialiseLookupItems()
        {
            // Sounds
            CCGameRepository.Instance.GetAllSounds().ForEach(s =>
                LookupItems.Add(new LookupItemModel("Sounds", s.Key, s.Value)));
            // Locomotors
            const string locomotorsType = "Locomotors";
            LookupItems.Add(new LookupItemModel(locomotorsType, "{4A582741-9839-11d1-B709-00A024DDAFD1}", "Ground Vehicles"));
            LookupItems.Add(new LookupItemModel(locomotorsType, "{4A582742-9839-11d1-B709-00A024DDAFD1}", "Hover Vehicles"));
            LookupItems.Add(new LookupItemModel(locomotorsType, "{4A582743-9839-11d1-B709-00A024DDAFD1}", "Subtarranean Vehicles"));
            LookupItems.Add(new LookupItemModel(locomotorsType, "{4A582744-9839-11d1-B709-00A024DDAFD1}", "Ground Infantry"));
            LookupItems.Add(new LookupItemModel(locomotorsType, "{4A582745-9839-11d1-B709-00A024DDAFD1}", "Drop Pod"));
            LookupItems.Add(new LookupItemModel(locomotorsType, "{4A582746-9839-11d1-B709-00A024DDAFD1}", "Aircraft"));
            LookupItems.Add(new LookupItemModel(locomotorsType, "{55D141B8-DB94-11d1-AC98-006008055BB5}", "Walker Vehicles"));
            LookupItems.Add(new LookupItemModel(locomotorsType, "{92612C46-F71F-11d1-AC9F-006008055BB5}", "Jumpjet Infantry"));
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
                        && !Datastructure.TiberiumGeneral.Any(v =>
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
            List<CategorizedValueDefinition> unitValueList)
        {
            var entityKeys = (File.GetSection(entitiesTypesSection)?.KeyValues.Select(k => k.Value)
                              ?? Enumerable.Empty<string>())
                .Union(DefaultFile.GetSection(entitiesTypesSection)?.KeyValues.Select(k => k.Value)
                       ?? Enumerable.Empty<string>());
            return GetGameEntities(entitiesTypesSection, entityKeys, unitValueList);
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
                    result.Add(new GameEntityModel(this, this,
                        entityType,
                        fileSection,
                        defaultSection,
                        unitValueList));
                }
                else if (defaultSection != null && _showMissingValues)
                {
                    result.Add(new GameEntityModel(this, this,
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
            List<CategorizedValueDefinition> unitValueList)
        {
            var result = new List<GameEntityModel>();
            var fileSections = File.Sections.Where(sectionFilter).ToList();
            foreach (var fileSection in fileSections)
            {
                result.Add(new GameEntityModel(this, this,
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
