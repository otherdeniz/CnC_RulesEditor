using System.ComponentModel;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class ScriptParameterValueDefinition
    {
        public string Value { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Browsable(false)]
        public string? GameKeyFilter { get; set; }
    }
}
