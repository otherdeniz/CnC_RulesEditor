using System.ComponentModel;
using System.Text.RegularExpressions;
using Deniz.TiberiumSunEditor.Gui.Controls;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class AiScriptKeyValueModel
    {
        private readonly AiRootModel _aiRootModel;
        private static readonly Regex KeyValueRegex = new Regex(@"(\d+),(-?\d+)", RegexOptions.Compiled);
        private readonly Action? _valueChangedAction;

        public AiScriptKeyValueModel(GameEntityModel entityModel, string key, Action? valueChangedAction)
        {
            EntityModel = entityModel;
            _aiRootModel = (AiRootModel)EntityModel.RootModel;
            Key = key;
            ReadValue();
            _valueChangedAction = valueChangedAction;
        }

        [Browsable(false)]
        public GameEntityModel EntityModel { get; }

        public string Key { get; set; }

        [Browsable(false)]
        public string? ActionValue { get; set; }

        public string Action { get; private set; } = string.Empty;

        [Browsable(false)]
        public string? ParameterValue { get; set; }

        [Browsable(false)]
        public string? CommentValue { get; set; }

        [DisplayName("Parameter Name")]
        public string ParameterName { get; private set; } = string.Empty;

        [DisplayName("Parameter Value")]
        public string Parameter { get; private set; } = string.Empty;

        [DisplayName("Building target selection")]
        public string Parameter2 { get; private set; } = string.Empty;

        [DisplayName(" ")]
        public Image? UpImage => Key == "0"
            ? null
            : ImageListComponent.Instance.Symbols16.Images[4];

        [DisplayName(" ")]
        public Image? DownImage => Key == (EntityModel.FileSection.GetMaxKeyValue()?.ToString() ?? "0")
            ? null
            : ImageListComponent.Instance.Symbols16.Images[5];

        [DisplayName(" ")]
        public Image DeleteImage => ImageListComponent.Instance.Symbols16.Images[1];

        public void ReadValue()
        {
            var keyValue = EntityModel.FileSection.GetValue(Key);
            if (keyValue != null)
            {
                var valueMatch = KeyValueRegex.Match(keyValue.Value);
                if (valueMatch.Success)
                {
                    ActionValue = valueMatch.Groups[1].Value;
                    ParameterValue = valueMatch.Groups[2].Value;
                    CommentValue = keyValue.Comment;
                    ParseActionValue();
                    return;
                }
            }
            ActionValue = null;
            ParameterValue = null;
            CommentValue = null;
            ParseActionValue();
        }

        public void WriteValue()
        {
            if (string.IsNullOrEmpty(ActionValue))
            {
                EntityModel.FileSection.SetValue(Key, string.Empty);
            }
            else
            {
                if (string.IsNullOrEmpty(ParameterValue))
                {
                    EntityModel.FileSection.SetValue(Key, $"{ActionValue},0");
                }
                else
                {
                    EntityModel.FileSection.SetValue(Key, $"{ActionValue},{ParameterValue}", CommentValue);
                }
            }
            _valueChangedAction?.Invoke();
            ParseActionValue();
        }

        public void ParseActionValue()
        {
            Action = string.Empty;
            Parameter = string.Empty;
            ParameterName = string.Empty;
            Parameter2 = string.Empty;
            var gameKey = EntityModel.RootModel.FileType.GameDefinition.GameKey;
            var actionDefinition = _aiRootModel.Aistructure
                .GetScriptActionsFiltered(gameKey)
                .FirstOrDefault(a => a.Number == ActionValue);
            if (actionDefinition == null)
            {
                Action = ActionValue ?? string.Empty;
                Parameter = ParameterValue ?? string.Empty;
                return;
            }
            Action = actionDefinition.ActionName;
            if (!int.TryParse(ParameterValue, out var parameterNumber) || actionDefinition.ParameterValue == "0") return;
            ParameterName = actionDefinition.ParameterName;
            switch (actionDefinition.ParameterValue)
            {
                case "n":
                    Parameter = parameterNumber.ToString("0");
                    break;
                case "v":
                {
                    var valueDefinition = actionDefinition.GetParameterValuesFiltered(gameKey)
                        .FirstOrDefault(v => v.Value == parameterNumber.ToString("0"));
                    Parameter = valueDefinition == null 
                        ? parameterNumber.ToString("0") 
                        : valueDefinition.Name;
                    break;
                }
                case "BuildingTypes":
                {
                    var resolvedBuildingParameter = ResolveBuildingParameter(_aiRootModel,
                        parameterNumber, CommentValue);
                    if (resolvedBuildingParameter.Building != null 
                        && resolvedBuildingParameter.Parameter2 != null)
                    {
                        var buildingName = resolvedBuildingParameter.Building.EntityName;
                        Parameter = buildingName == string.Empty
                            ? resolvedBuildingParameter.Building.EntityKey
                            : $"{resolvedBuildingParameter.Building.EntityKey} [{buildingName}]";
                        Parameter2 = resolvedBuildingParameter.Parameter2.Name;
                    }
                    //var parameter2Definition = AistructureFile.Instance.ScriptBuildingParameter2
                    //    .LastOrDefault(p => p.AddValue <= parameterNumber);
                    //if (parameter2Definition != null)
                    //{
                    //    Parameter2 = parameter2Definition.Name;
                    //    GameEntityModel? buildingEntity = null;
                    //    if (!string.IsNullOrEmpty(CommentValue))
                    //    {
                    //        buildingEntity =
                    //            EntityModel.RootModel.RulesModel.BuildingEntities.FirstOrDefault(b =>
                    //                b.EntityKey == CommentValue);
                    //        //TODO: check index and re-write value if miss-matches
                    //    }
                    //    if (buildingEntity == null)
                    //    {
                    //        var buildingIndex = parameterNumber - parameter2Definition.AddValue;
                    //        var buildingKeyValues = EntityModel.RootModel.RulesModel.DefaultFile
                    //            .GetSection("BuildingTypes")?.KeyValues;
                    //        if (buildingKeyValues != null
                    //            && buildingIndex >= 0
                    //            && buildingIndex < buildingKeyValues.Count)
                    //        {
                    //            buildingEntity = EntityModel.RootModel.RulesModel.BuildingEntities.FirstOrDefault(b =>
                    //                b.EntityKey == buildingKeyValues[buildingIndex].Value);
                    //        }
                    //    }
                    //    if (buildingEntity != null)
                    //    {
                    //        var buildingName = buildingEntity.EntityName;
                    //        Parameter = buildingName == string.Empty
                    //            ? buildingEntity.EntityKey
                    //            : $"{buildingEntity.EntityKey} [{buildingName}]";
                    //    }
                    //}
                    break;
                }
            }
        }

        public static (GameEntityModel? Building, ScriptBuildingParameter2Definition? Parameter2) ResolveBuildingParameter(
            AiRootModel aiRootModel, int parameterValue, string? comment)
        {
            var parameter2Definition = aiRootModel.Aistructure.ScriptBuildingParameter2
                .LastOrDefault(p => p.AddValue <= parameterValue);
            if (parameter2Definition != null)
            {
                //Parameter2 = parameter2Definition.Name;
                GameEntityModel? buildingEntity = null;
                if (!string.IsNullOrEmpty(comment))
                {
                    buildingEntity =
                        aiRootModel.RulesModel.BuildingEntities.FirstOrDefault(b =>
                            b.EntityKey == comment);
                    //TODO: check index and re-write value if miss-matches
                }
                if (buildingEntity == null)
                {
                    var buildingIndex = parameterValue - parameter2Definition.AddValue;
                    var buildingKeyValues = aiRootModel.RulesModel.DefaultFile
                        .GetSection("BuildingTypes")?.KeyValues;
                    if (buildingKeyValues != null
                        && buildingIndex >= 0
                        && buildingIndex < buildingKeyValues.Count)
                    {
                        buildingEntity = aiRootModel.RulesModel.BuildingEntities.FirstOrDefault(b =>
                            b.EntityKey == buildingKeyValues[buildingIndex].Value);
                    }
                }
                if (buildingEntity != null)
                {
                    return (buildingEntity, parameter2Definition);
                //    var buildingName = buildingEntity.EntityName;
                //    Parameter = buildingName == string.Empty
                //        ? buildingEntity.EntityKey
                //        : $"{buildingEntity.EntityKey} [{buildingName}]";
                }
            }
            return (null, null);
        }
    }
}
