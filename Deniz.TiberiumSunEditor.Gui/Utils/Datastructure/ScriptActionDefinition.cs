using Deniz.TiberiumSunEditor.Gui.Model.Interface;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class ScriptActionDefinition
    {
        public string Number { get; set; } = string.Empty;

        public string ActionName { get; set; } = string.Empty;

        public string ParameterValue { get; set; } = string.Empty;

        public string ParameterName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? GameKeyFilter { get; set; }

        public List<ScriptParameterValueDefinition> ParameterValues { get; set; } = new();

        public IEnumerable<ScriptParameterValueDefinition> GetParameterValuesFiltered(string gameKey)
        {
            return ParameterValues.Where(v =>
                v.GameKeyFilter == null || v.GameKeyFilter.Split(",").Any(g => g == gameKey));
        }
    }
}
