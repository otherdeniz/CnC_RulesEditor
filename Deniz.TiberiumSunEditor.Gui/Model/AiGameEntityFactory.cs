using Deniz.TiberiumSunEditor.Gui.Utils;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class AiGameEntityFactory
    {
        private readonly AiRootModel _aiRootModel;

        public AiGameEntityFactory(AiRootModel aiRootModel)
        {
            _aiRootModel = aiRootModel;
        }

        public GameEntityModel AddNewGameEntity(string entityType, GameEntityModel? parentEntity)
        {
            if (entityType == "AITriggerTypes")
            {
                return AddNewTrigger(parentEntity);
            }
            var newTeamEntity = _aiRootModel.AddGameEntity(entityType);
            List<CategorizedValueDefinition>? defaultValueDefinitions = null;
            var newName = $"unnamed {entityType}";
            var parentKey = string.Empty;
            switch (entityType)
            {
                case "TeamTypes":
                    defaultValueDefinitions = _aiRootModel.TeamUnitValueDefinitions;
                    newName = "unnamed Team";
                    switch (parentEntity?.EntityType)
                    {
                        case "TaskForces":
                            parentKey = "TaskForce";
                            break;
                        case "ScriptTypes":
                            parentKey = "Script";
                            break;
                    }
                    break;
            }
            if (defaultValueDefinitions != null)
            {
                foreach (var valueDefinition in defaultValueDefinitions)
                {
                    if (valueDefinition.UnitValueDefinition.Default != string.Empty)
                    {
                        newTeamEntity.EntityModel.FileSection.SetValue(valueDefinition.UnitValueDefinition.Key,
                            valueDefinition.UnitValueDefinition.Default);
                    }
                }
            }
            newTeamEntity.EntityModel.FileSection.SetValue("Name", newName);
            if (parentKey != string.Empty && parentEntity != null)
            {
                newTeamEntity.EntityModel.FileSection.SetValue(parentKey, parentEntity.EntityKey);
            }
            return newTeamEntity.EntityModel;
        }

        public GameEntityModel AddNewTrigger(GameEntityModel? parentTeamEntity)
        {
            var newKeyPrefix = _aiRootModel.FileType.BaseType == FileBaseType.Ai
                ? "A4"
                : "B4";
            var newKeySufix = _aiRootModel.FileType.BaseType == FileBaseType.Ai
                ? "-G"
                : "";
            string newKey;
            do
            {
                newKey = $"{newKeyPrefix}{HexGenerator.GenerateRandomHex(6)}{newKeySufix}";
            } while (_aiRootModel.TriggerEntities.Any(e => e.EntityModel.EntityKey == newKey));

            var triggerSection = _aiRootModel.File.GetSection("AITriggerTypes")
                                 ?? _aiRootModel.File.AddSection("AITriggerTypes");
            triggerSection.SetValue(newKey, string.Empty, removeEmptyRuntimeAdded:false);
            var triggerKeyValue = triggerSection.GetValue(newKey)!;
            var triggerGameEntity = new AiRootModel.VirtualTriggerGameEntitiyModel(_aiRootModel, triggerKeyValue, null);
            if (parentTeamEntity != null)
            {
                triggerGameEntity.FileSection.SetValue("Team1", parentTeamEntity.EntityKey);
            }
            _aiRootModel.TriggerEntities.Add(new AiTriggerListItemModel(newKey, triggerGameEntity));
            return triggerGameEntity;
        }
    }
}
