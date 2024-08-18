using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class DatastructureFile : JsonFileBase
    {
        private static DatastructureFile? _artInstance;
        private static DatastructureFile? _instance;
        private static DatastructureFile? _instanceAres;
        private static DatastructureFile? _instanceVinifera;

        public static DatastructureFile ArtInstance => _artInstance ??= LoadFile("Artstructure.json", null);
        public static DatastructureFile Instance => _instance ??= LoadFile("Datastructure.json", null);
        public static DatastructureFile InstanceAres => _instanceAres ??= LoadFile("DatastructureAres.json", "ARES");
        public static DatastructureFile InstanceVinifera => _instanceVinifera ??= LoadFile("DatastructureVinifera.json", "VINIFERA");

        protected static DatastructureFile LoadFile(string fileName, string? moduleCategory)
        {
            using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream(fileName))
            {
                var file = Load<DatastructureFile>(fileStream);
                if (moduleCategory != null)
                {
                    file.ApplyModuleCategory($"{moduleCategory}: ");
                }
                return file;
            }
        }

        public List<CommonValueDefinition> CommonGeneral { get; set; } = new();

        public List<CommonValueDefinition> AIGeneral { get; set; } = new();

        public List<CommonValueDefinition> TiberiumGeneral { get; set; } = new();

        public List<CommonValueDefinition> TiberiumValues { get; set; } = new();

        public List<CommonValueDefinition> AudioVisualValues { get; set; } = new();

        public List<UnitValueDefinition> Sides { get; set; } = new();

        public List<UnitValueDefinition> AllUnits { get; set; } = new();

        public List<UnitValueDefinition> AllMovingUnits { get; set; } = new();

        public List<UnitValueDefinition> InfantryUnits { get; set; } = new();

        public List<UnitValueDefinition> DrivingVehicleUnits { get; set; } = new();

        public List<UnitValueDefinition> AircraftUnits { get; set; } = new();

        public List<UnitValueDefinition> BuildingUnits { get; set; } = new();

        public List<UnitValueDefinition> Weapons { get; set; } = new();

        public List<UnitValueDefinition> Projectiles { get; set; } = new();

        public List<UnitValueDefinition> Warheads { get; set; } = new();

        public List<CommonValueDefinition> SuperWeaponsGeneral { get; set; } = new();

        public List<UnitValueDefinition> SuperWeapons { get; set; } = new();

        public List<UnitValueDefinition> Animations { get; set; } = new();

        public List<ValueLookupDefinition> ValueTypes { get; set; } = new();

        public List<KeyValueDefinition> NewBuilding { get; set; } = new();

        public List<KeyValueDefinition> NewInfantry { get; set; } = new();

        public List<KeyValueDefinition> NewVehicle { get; set; } = new();

        public List<KeyValueDefinition> NewAircraft { get; set; } = new();

        public List<AdditionalTypesSectionDefinition> AdditionalTypes { get; set; } = new();

        public DatastructureFile MergeWith(DatastructureFile priorityFile)
        {
            return new DatastructureFile
            {
                AIGeneral = priorityFile.AIGeneral
                    .UnionBy(AIGeneral, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                CommonGeneral = priorityFile.CommonGeneral
                    .UnionBy(CommonGeneral, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                Sides = priorityFile.Sides
                    .UnionBy(Sides, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                AudioVisualValues = priorityFile.AudioVisualValues
                    .UnionBy(AudioVisualValues, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                AircraftUnits = priorityFile.AircraftUnits
                    .UnionBy(AircraftUnits, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                AllMovingUnits = priorityFile.AllMovingUnits
                    .UnionBy(AllMovingUnits, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                AllUnits = priorityFile.AllUnits
                    .UnionBy(AllUnits, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                BuildingUnits = priorityFile.BuildingUnits
                    .UnionBy(BuildingUnits, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                DrivingVehicleUnits = priorityFile.DrivingVehicleUnits
                    .UnionBy(DrivingVehicleUnits, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                InfantryUnits = priorityFile.InfantryUnits
                    .UnionBy(InfantryUnits, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                Warheads = priorityFile.Warheads
                    .UnionBy(Warheads, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                Projectiles = priorityFile.Projectiles
                    .UnionBy(Projectiles, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                Weapons = priorityFile.Weapons
                    .UnionBy(Weapons, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                TiberiumGeneral = priorityFile.TiberiumGeneral
                    .UnionBy(TiberiumGeneral, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                TiberiumValues = priorityFile.TiberiumValues
                    .UnionBy(TiberiumValues, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                SuperWeapons = priorityFile.SuperWeapons
                    .UnionBy(SuperWeapons, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                SuperWeaponsGeneral = priorityFile.SuperWeaponsGeneral
                    .UnionBy(SuperWeaponsGeneral, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                Animations = priorityFile.Animations
                    .UnionBy(Animations, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                ValueTypes = priorityFile.ValueTypes
                    .UnionBy(ValueTypes, k => k.ValueType, StringEqualityComparer.Instance)
                    .ToList(),
                NewAircraft = priorityFile.NewAircraft
                    .UnionBy(NewAircraft, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                NewBuilding = priorityFile.NewBuilding
                    .UnionBy(NewBuilding, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                NewInfantry = priorityFile.NewInfantry
                    .UnionBy(NewInfantry, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                NewVehicle = priorityFile.NewVehicle
                    .UnionBy(NewVehicle, k => k.Key, StringEqualityComparer.Instance)
                    .ToList(),
                AdditionalTypes = priorityFile.AdditionalTypes
                    .Union(AdditionalTypes)
                    .ToList()
            };
        }

        protected void ApplyModuleCategory(string moduleCategory)
        {
            CommonGeneral.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            AIGeneral.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            TiberiumGeneral.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            TiberiumValues.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            AudioVisualValues.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            Sides.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            AllUnits.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            AllMovingUnits.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            InfantryUnits.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            DrivingVehicleUnits.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            AircraftUnits.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            BuildingUnits.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            Weapons.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            Projectiles.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            Warheads.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            SuperWeaponsGeneral.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            SuperWeapons.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
            Animations.ForEach(v => v.ModuleCategory = string.Format(moduleCategory, v.ModuleCategory));
        }
    }
}
