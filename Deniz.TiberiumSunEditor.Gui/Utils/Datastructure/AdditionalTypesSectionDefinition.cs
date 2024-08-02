namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class AdditionalTypesSectionDefinition
    {
        public string Module { get; set; }

        public string TypesName { get; set; }

        public string TemplateSection { get; set; }

        public List<UnitValueDefinition> ValueDefinitions { get; set; } = new();

    }
}
