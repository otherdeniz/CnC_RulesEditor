using Newtonsoft.Json;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class UnitValueDefinition
    {
        public string Key { get; set; } = null!;

        public string? Description { get; set; }

        public string Default { get; set; } = string.Empty;

        public string? ValueList { get; set; }

        public string? ValueType { get; set; }

        public string? LookupType { get; set; }

        public bool MultipleValues { get; set; }

        public string? GameKeyFilter { get; set; }

        public virtual string Category { get; set; } = string.Empty;

        [JsonIgnore] 
        public string ModuleCategory { get; set; } = string.Empty;

        [JsonIgnore]
        public bool DetectTypeAtRuntime { get; set; }
    }
}
