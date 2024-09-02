using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class ScriptActionDefinition
    {
        [DisplayName("Nr")]
        public string Number { get; set; } = string.Empty;

        [DisplayName("Action")]
        public string ActionName { get; set; } = string.Empty;

        [Browsable(false)]
        public string ParameterValue { get; set; } = string.Empty;

        [DisplayName("Parameter")]
        public string ParameterName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Browsable(false)]
        public string? GameKeyFilter { get; set; }

        [Browsable(false)]
        public List<ScriptParameterValueDefinition> ParameterValues { get; set; } = new();

        public IEnumerable<ScriptParameterValueDefinition> GetParameterValuesFiltered(string gameKey)
        {
            return ParameterValues.Where(v =>
                v.GameKeyFilter == null || v.GameKeyFilter.Split(",").Any(g => g == gameKey));
        }
    }
}
