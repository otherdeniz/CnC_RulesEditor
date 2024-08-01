using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using System.Linq;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class DatastructureFile : JsonFileBase
    {
        private static DatastructureFile? _instance;
        private static DatastructureFile? _instanceAres;
        private static DatastructureFile? _instancePhobos;

        public static DatastructureFile Instance => _instance ??= LoadFile("Datastructure.json", null);
        public static DatastructureFile InstanceAres => _instanceAres ??= LoadFile("DatastructureAres.json", "Ares");
        public static DatastructureFile InstancePhobos => _instancePhobos ??= LoadFile("DatastructurePhobos.json", "Phobos");

        private static DatastructureFile LoadFile(string fileName, string? moduleCategory)
        {
            using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream(fileName))
            {
                var file = Load<DatastructureFile>(fileStream);
                if (moduleCategory != null)
                {
                    file.ApplyModuleCategory($" [{moduleCategory}]");
                }
                return file;
            }
        }

        public List<CommonValueDefinition> CommonGeneral { get; set; } = new();

        public List<CommonValueDefinition> AIGeneral { get; set; } = new();

        public List<CommonValueDefinition> TiberiumGeneral { get; set; } = new();

        public List<CommonValueDefinition> TiberiumValues { get; set; } = new();

        public List<UnitValueDefinition> AllUnits { get; set; } = new();

        public List<UnitValueDefinition> AllMovingUnits { get; set; } = new();

        public List<UnitValueDefinition> InfantryUnits { get; set; } = new();

        public List<UnitValueDefinition> DrivingVehicleUnits { get; set; } = new();

        public List<UnitValueDefinition> AircraftUnits { get; set; } = new();

        public List<UnitValueDefinition> BuildingUnits { get; set; } = new();

        public List<UnitValueDefinition> Weapons { get; set; } = new();

        public List<UnitValueDefinition> Warheads { get; set; } = new();

        public List<CommonValueDefinition> SuperWeaponsGeneral { get; set; } = new();

        public List<UnitValueDefinition> SuperWeapons { get; set; } = new();

        public List<ValueLookupDefinition> ValueTypes { get; set; } = new();

        public List<KeyValueDefinition> NewBuilding { get; set; } = new();

        public List<KeyValueDefinition> NewInfantry { get; set; } = new();

        public List<KeyValueDefinition> NewVehicle { get; set; } = new();

        public List<KeyValueDefinition> NewAircraft { get; set; } = new();

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
            };
        }

        private void ApplyModuleCategory(string moduleCategory)
        {
            CommonGeneral.ForEach(v => v.ModuleCategory = moduleCategory);
            AIGeneral.ForEach(v => v.ModuleCategory = moduleCategory);
            TiberiumGeneral.ForEach(v => v.ModuleCategory = moduleCategory);
            TiberiumValues.ForEach(v => v.ModuleCategory = moduleCategory);
            AllUnits.ForEach(v => v.ModuleCategory = moduleCategory);
            AllMovingUnits.ForEach(v => v.ModuleCategory = moduleCategory);
            InfantryUnits.ForEach(v => v.ModuleCategory = moduleCategory);
            DrivingVehicleUnits.ForEach(v => v.ModuleCategory = moduleCategory);
            AircraftUnits.ForEach(v => v.ModuleCategory = moduleCategory);
            BuildingUnits.ForEach(v => v.ModuleCategory = moduleCategory);
            Weapons.ForEach(v => v.ModuleCategory = moduleCategory);
            Warheads.ForEach(v => v.ModuleCategory = moduleCategory);
            SuperWeaponsGeneral.ForEach(v => v.ModuleCategory = moduleCategory);
            SuperWeapons.ForEach(v => v.ModuleCategory = moduleCategory);
        }
    }
}
