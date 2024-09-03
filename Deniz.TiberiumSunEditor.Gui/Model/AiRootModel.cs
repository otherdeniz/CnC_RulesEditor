using System.Text;
using Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit;
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
            bool showMissingValues = false,
            bool remapTechnosRootModel = true)
        {
            _showMissingValues = showMissingValues;
            RulesModel = rulesRootModel;
            File = iniFile;
            DefaultFile = defaultFileOverwrite ?? rulesRootModel.FileType.GameDefinition.LoadDefaultAiFile();
            Aistructure = AistructureFile.Instance;
            TeamUnitValueDefinitions = Aistructure.Teams.ToCategorizedList();
            TriggerUnitValueDefinitions = Aistructure.TriggerVirtualSections.ToCategorizedList();
            EntityTypeEditControl.Add(new EntityTypeEditControlTypeModel("TaskForces", typeof(AiTaskForceEditControl)));
            EntityTypeEditControl.Add(new EntityTypeEditControlTypeModel("ScriptTypes", typeof(AiScriptEditControl)));
            EntityTypeEditControl.Add(new EntityTypeEditControlTypeModel("TeamTypes", typeof(AiTeamEditControl)));
            EntityTypeEditControl.Add(new EntityTypeEditControlTypeModel("AITriggerTypes", typeof(AiTriggerEditControl)));
            EntityTypeEditControl.Add(new EntityTypeEditControlTypeModel("InfantryTypes", typeof(AiUnitEditControl)));
            EntityTypeEditControl.Add(new EntityTypeEditControlTypeModel("VehicleTypes", typeof(AiUnitEditControl)));
            EntityTypeEditControl.Add(new EntityTypeEditControlTypeModel("AircraftTypes", typeof(AiUnitEditControl)));
            LoadGameEntities();
            if (remapTechnosRootModel)
            {
                foreach (var rulesEntity in RulesModel.InfantryEntities
                             .Union(RulesModel.VehicleEntities)
                             .Union(RulesModel.AircraftEntities))
                {
                    rulesEntity.RootModel = this;
                    rulesEntity.InfoNumberFunction = () =>
                        TaskForceEntities.Count(e => e.EntityModel.FileSection.KeyValues.Any(k =>
                            k.Value.EndsWith($",{rulesEntity.EntityKey}")));
                }
            }
        }

        public event EventHandler<EventArgs>? EntitiesReloaded;
        public event EventHandler<GlobalEntityNotificationEventArgs>? GlobalEntityNotification;

        public RulesRootModel RulesModel { get; }

        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public FileTypeModel FileType => RulesModel.FileType;

        public AistructureFile Aistructure { get; }

        public List<CategorizedValueDefinition> TeamUnitValueDefinitions { get; }

        public List<CategorizedValueDefinition> TriggerUnitValueDefinitions { get; }

        public List<LookupItemModel> LookupItems => RulesModel.LookupItems;

        public Dictionary<string, List<GameEntityModel>> LookupEntities { get; } = new();

        public List<EntityListItemModel> TaskForceEntities { get; private set; } = null!;

        public List<EntityListItemModel> ScriptEntities { get; private set; } = null!;

        public List<EntityListItemModel> TeamEntities { get; private set; } = null!;

        public List<AiTriggerListItemModel> TriggerEntities { get; private set; } = null!;

        public List<EntityTypeEditControlTypeModel> EntityTypeEditControl { get; } = new();

        public void RaiseGlobalEntityNotification(string entitiyKey, string notificationName)
        {
            GlobalEntityNotification?.Invoke(this, new GlobalEntityNotificationEventArgs(entitiyKey, notificationName));
        }

        public void ReloadGameEntites()
        {
            LoadGameEntities();
            EntitiesReloaded?.Invoke(this, EventArgs.Empty);
        }

        private void LoadGameEntities()
        {
            TaskForceEntities = GetGameEntitiesByAiTypesSection("TaskForces", null);
            ScriptEntities = GetGameEntitiesByAiTypesSection("ScriptTypes", null);
            TeamEntities = GetGameEntitiesByAiTypesSection("TeamTypes", TeamUnitValueDefinitions);
            TriggerEntities = GetVirtualTriggerGameEntities();
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
                    unitValueList = TeamUnitValueDefinitions;
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
            var typeKey = entitiesTypesSection.GetMaxKeyValue() + 1 ?? 0;
            entitiesTypesSection.SetValue(typeKey.ToString(), newKey);

            LookupItems.Add(new LookupItemModel(entityType, newKey, newGameEntity));
            if (LookupEntities.TryGetValue(entityType, out var lookupEntities))
            {
                lookupEntities.Add(newGameEntity);
            }

            var newListItemModel = new EntityListItemModel(typeKey.ToString(), newGameEntity);
            entitiesList.Add(newListItemModel);
            return newListItemModel;
        }

        private List<AiTriggerListItemModel> GetVirtualTriggerGameEntities()
        {
            var triggerSection = File.GetSection("AITriggerTypes");
            if (triggerSection == null)
            {
                return new List<AiTriggerListItemModel>();
            }

            var defaultTriggerSection = DefaultFile.GetSection("AITriggerTypes");

            return triggerSection.KeyValues.Select(k =>
                    new AiTriggerListItemModel(k.Key, new VirtualTriggerGameEntitiyModel(this, k, 
                        defaultTriggerSection?.KeyValues.FirstOrDefault(d => d.Key == k.Key))))
                .ToList();
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
                if (fileSection != null)
                {
                    var entityModel = new GameEntityModel(RulesModel, this,
                        entityType,
                        fileSection,
                        defaultSection,
                        unitValueList);
                    result.Add(new EntityListItemModel(entityKey.Key, entityModel));
                }
                else if (defaultSection != null && _showMissingValues)
                {
                    var entityModel = new GameEntityModel(RulesModel, this,
                        entityType,
                        File.AddSection(entityKey.Value),
                        defaultSection,
                        unitValueList);
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

        public class VirtualTriggerGameEntitiyModel : GameEntityModel
        {
            private readonly AiRootModel _rootModel;

            public VirtualTriggerGameEntitiyModel(AiRootModel rootModel, 
                IniFileLineKeyValue triggerKeyValue, 
                IniFileLineKeyValue? defaultTriggerKeyValue)
                : base(rootModel.RulesModel, 
                    rootModel, 
                    "AITriggerTypes", 
                    new IniFileSection { SectionName = triggerKeyValue.Key },
                    defaultTriggerKeyValue == null 
                        ? null 
                        : new IniFileSection { SectionName = triggerKeyValue.Key },
                    rootModel.TriggerUnitValueDefinitions)
            {
                _rootModel = rootModel;
                TriggerKeyValue = triggerKeyValue;
                ReadTriggerValue(FileSection, triggerKeyValue);
                if (DefaultSection != null && defaultTriggerKeyValue != null)
                {
                    ReadTriggerValue(DefaultSection, defaultTriggerKeyValue);
                }
                FileSection.ValueChanged += FileSectionOnValueChanged;
            }

            public IniFileLineKeyValue TriggerKeyValue { get; }

            private void ReadTriggerValue(IniFileSection fileSection, IniFileLineKeyValue triggerKeyValue)
            {
                var valueParts = triggerKeyValue.Value.Split(',');
                if (valueParts.Length == 18)
                {
                    fileSection.SetValue("Name", valueParts[0]);
                    fileSection.SetValue("Team1", valueParts[1]);
                    fileSection.SetValue("OwnerHouse", valueParts[2]);
                    fileSection.SetValue("TechLevel", valueParts[3]);
                    fileSection.SetValue("TriggerWhen", valueParts[4]);
                    fileSection.SetValue("ComparisonObject", valueParts[5]);

                    var comparisonValue = valueParts[6];
                    if (comparisonValue.Length >= 16)
                    {
                        var hexNumber = $"0x{comparisonValue[6..7]}{comparisonValue[4..5]}{comparisonValue[2..3]}{comparisonValue[0..1]}";
                        if (int.TryParse(hexNumber, out var valueNumber))
                        {
                            fileSection.SetValue("Value", valueNumber.ToString("0"));
                        }

                        var comparatorText = "Less than";
                        switch (comparisonValue[9])
                        {
                            case '1':
                                comparatorText = "Less than or equal to";
                                break;
                            case '2':
                                comparatorText = "Equal to";
                                break;
                            case '3':
                                comparatorText = "Greater than or equal to";
                                break;
                            case '4':
                                comparatorText = "Greater than";
                                break;
                            case '5':
                                comparatorText = "Not equal to";
                                break;
                        }
                        fileSection.SetValue("Comparator", comparatorText);
                    }

                    fileSection.SetValue("StartingWeight", valueParts[7]);
                    fileSection.SetValue("MinimumWeight", valueParts[8]);
                    fileSection.SetValue("MaximumWeight", valueParts[9]);
                    fileSection.SetValue("IsForSkirmish", valueParts[10] == "1" ? "yes" : "no");
                    fileSection.SetValue("Side", valueParts[12]);
                    fileSection.SetValue("Team2", valueParts[14]);
                    fileSection.SetValue("Easy", valueParts[15] == "1" ? "yes" : "no");
                    fileSection.SetValue("Medium", valueParts[16] == "1" ? "yes" : "no");
                    fileSection.SetValue("Hard", valueParts[17] == "1" ? "yes" : "no");
                }
                else
                {
                    // use all default values
                    fileSection.SetValue("Name", "new trigger");
                    _rootModel.TriggerUnitValueDefinitions.ForEach(v => fileSection.SetValue(v.UnitValueDefinition.Key, v.UnitValueDefinition.Default));
                }
            }

            private void FileSectionOnValueChanged(object? sender, IniFileSectionChangedEventArgs e)
            {
                // write the trigger value
                var valueBuilder = new StringBuilder();
                valueBuilder.Append($"{FileSection.GetValue("Name")?.Value ?? "no name"},");
                valueBuilder.Append($"{FileSection.GetValue("Team1")?.Value ?? "<none>"},");
                valueBuilder.Append($"{FileSection.GetValue("OwnerHouse")?.Value ?? "<none>"},");
                valueBuilder.Append($"{FileSection.GetValue("TechLevel")?.Value ?? "1"},");
                valueBuilder.Append($"{FileSection.GetValue("TriggerWhen")?.Value ?? "-1"},");
                valueBuilder.Append($"{FileSection.GetValue("ComparisonObject")?.Value ?? "<none>"},");

                var octetPart1 = "00000000";
                if (int.TryParse(FileSection.GetValue("Value")?.Value, out var valueNumber))
                {
                    var hexNumber = valueNumber.ToString("X8");
                    octetPart1 = $"{hexNumber[6..7]}{hexNumber[4..5]}{hexNumber[2..3]}{hexNumber[0..1]}";
                }
                var octetPart2 = "00000000";
                switch (FileSection.GetValue("Comparator")?.Value)
                {
                    case "Less than or equal to":
                        octetPart2 = "01000000";
                        break;
                    case "Equal to":
                        octetPart2 = "02000000";
                        break;
                    case "Greater than or equal to":
                        octetPart2 = "03000000";
                        break;
                    case "Greater than":
                        octetPart2 = "04000000";
                        break;
                    case "Not equal to":
                        octetPart2 = "05000000";
                        break;
                }
                valueBuilder.Append($"{octetPart1}{octetPart2}000000000000000000000000000000000000000000000000");

                valueBuilder.Append((decimal.TryParse(FileSection.GetValue("StartingWeight")?.Value ?? "50",
                                        out var startingWeightNumber)
                                        ? startingWeightNumber.ToString("0.000000")
                                        : "50.000000")
                                    + ",");

                valueBuilder.Append((decimal.TryParse(FileSection.GetValue("MinimumWeight")?.Value ?? "50",
                                        out var minWeightNumber)
                                        ? minWeightNumber.ToString("0.000000")
                                        : "50.000000")
                                    + ",");

                valueBuilder.Append((decimal.TryParse(FileSection.GetValue("MaximumWeight")?.Value ?? "50",
                                        out var maxWeightNumber)
                                        ? maxWeightNumber.ToString("0.000000")
                                        : "50.000000")
                                    + ",");

                valueBuilder.Append((FileSection.GetValue("IsForSkirmish")?.Value == "yes" ? "1" : "0") + ",");
                valueBuilder.Append("0");
                valueBuilder.Append($"{FileSection.GetValue("Side")?.Value ?? "0"},");
                valueBuilder.Append("0");
                valueBuilder.Append($"{FileSection.GetValue("Team2")?.Value ?? "<none>"},");
                valueBuilder.Append((FileSection.GetValue("Easy")?.Value == "yes" ? "1" : "0") + ",");
                valueBuilder.Append((FileSection.GetValue("Medium")?.Value == "yes" ? "1" : "0") + ",");
                valueBuilder.Append(FileSection.GetValue("Hard")?.Value == "yes" ? "1" : "0");

                TriggerKeyValue.Value = valueBuilder.ToString();
            }
        }
    }
}
