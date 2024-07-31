using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class DatastructureFile : JsonFileBase
    {
        private static DatastructureFile? _instance;
        public static DatastructureFile Instance => _instance ??= LoadFile();
        private static DatastructureFile LoadFile()
        {
            using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream("Datastructure.json"))
            {
                return Load<DatastructureFile>(fileStream);
            }
        }

        public List<CommonValueDefinition> CommonGeneral { get; set; } = null!;

        public List<CommonValueDefinition> AIGeneral { get; set; } = null!;

        public List<CommonValueDefinition> TiberiumGeneral { get; set; } = null!;

        public List<CommonValueDefinition> TiberiumValues { get; set; } = null!;

        public List<UnitValueDefinition> AllUnits { get; set; } = null!;

        public List<UnitValueDefinition> AllMovingUnits { get; set; } = null!;

        public List<UnitValueDefinition> InfantryUnits { get; set; } = null!;

        public List<UnitValueDefinition> DrivingVehicleUnits { get; set; } = null!;

        public List<UnitValueDefinition> AircraftUnits { get; set; } = null!;

        public List<UnitValueDefinition> BuildingUnits { get; set; } = null!;

        public List<UnitValueDefinition> Weapons { get; set; } = null!;

        public List<UnitValueDefinition> Warheads { get; set; } = null!;

        public List<CommonValueDefinition> SuperWeaponsGeneral { get; set; } = null!;

        public List<UnitValueDefinition> SuperWeapons { get; set; } = null!;

        public List<KeyValueDefinition> NewBuilding { get; set; } = null!;

        public List<KeyValueDefinition> NewInfantry{ get; set; } = null!;

        public List<KeyValueDefinition> NewVehicle { get; set; } = null!;

        public List<KeyValueDefinition> NewAircraft { get; set; } = null!;

    }
}
