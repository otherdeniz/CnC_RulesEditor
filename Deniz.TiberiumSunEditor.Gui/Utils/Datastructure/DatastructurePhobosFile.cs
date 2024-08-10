using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using System.Text.RegularExpressions;
using Deniz.TiberiumSunEditor.Gui.Utils.Extensions;

namespace Deniz.TiberiumSunEditor.Gui.Utils.Datastructure
{
    public class DatastructurePhobosFile : DatastructureFile
    {
        private static readonly Regex ValueListRegEx = new Regex(@"\((.+\|.+)\)", RegexOptions.Compiled);

        private static DatastructurePhobosFile? _artInstancePhobos;
        private static DatastructurePhobosFile? _instancePhobos;
        public static DatastructurePhobosFile ArtInstancePhobos
        {
            get
            {
                if (_artInstancePhobos == null)
                {
                    _artInstancePhobos = new DatastructurePhobosFile();
                    _artInstancePhobos.LoadTemplateInis("Phobos", "Art.*.ini");
                    _artInstancePhobos.ApplyModuleCategory("PHOBOS{0}: ");
                }
                return _artInstancePhobos;
            }
        }
        public static DatastructurePhobosFile InstancePhobos
        {
            get
            {
                if (_instancePhobos == null)
                {
                    using (var fileStream = ResourcesRepository.Instance.ReadResourcesFileStream("DatastructurePhobos.json"))
                    {
                        _instancePhobos = Load<DatastructurePhobosFile>(fileStream);
                        _instancePhobos.LoadTemplateInis("Phobos", "Rules.*.ini");
                        _instancePhobos.ApplyModuleCategory("PHOBOS{0}: ");
                    }
                }
                return _instancePhobos;
            }
        }

        private void LoadTemplateInis(string subFolder, string searchPattern)
        {
            var templatePath = Path.Combine(ResourcesRepository.Instance.ResourcesPath, subFolder);
            var templateInis = Directory.GetFiles(templatePath, searchPattern).Select(IniFile.Load).ToList();
            // load all Types sections
            var additionalTypesSections = templateInis
                .SelectMany(i => i.Sections.Where(s => s.SectionName != null 
                                                       && s.SectionName.EndsWith("Types"))).ToList();
            foreach (var additionalTypesSection in additionalTypesSections)
            {
                var templateName = additionalTypesSection.KeyValues.FirstOrDefault()?.Key == "0"
                    ? additionalTypesSection.KeyValues.FirstOrDefault()?.Value
                    : null;
                if (templateName != null)
                {
                    AdditionalTypes.Add(new AdditionalTypesSectionDefinition
                    {
                        Module = "PHOBOS",
                        TemplateSection = templateName,
                        TypesName = additionalTypesSection.SectionName!,
                        ValueDefinitions = new List<UnitValueDefinition>()
                    });
                }
            }
            // load all value sections
            foreach (var valueSection in templateInis.SelectMany(i =>
                         i.Sections.Where(s => additionalTypesSections.All(a => a.SectionName != null
                             && a.SectionName != s.SectionName))
                             .Select(s => new { Section = s, File = i})
                         )
                     )
            {
                var commonValueDeinitions = GetCommonValueDefinitions(valueSection.Section.SectionName!);
                if (commonValueDeinitions != null)
                {
                    foreach (var commonKeyValue in valueSection.Section.KeyValues)
                    {
                        commonValueDeinitions.Add(new CommonValueDefinition
                        {
                            ModuleCategory = $", {GetFileDescription(valueSection.File.OriginalFileName)}",
                            Category = "General",
                            Default = commonKeyValue.Value,
                            Description = commonKeyValue.Comment,
                            Key = commonKeyValue.Key,
                            Section = valueSection.Section.SectionName!,
                            ValueList = commonKeyValue.Comment != null ? DetectValueList(commonKeyValue.Comment) : null,
                            LookupType = commonKeyValue.Comment != null ? DetectLookupType(commonKeyValue.Comment) : null,
                            MultipleValues = commonKeyValue.Comment?.Contains("list of ") ?? false
                        });
                    }
                }
                var unitValueDefinitions = GetUnitValueDefinitions(valueSection.Section.SectionName!);
                if (unitValueDefinitions != null)
                {
                    foreach (var unitKeyValue in valueSection.Section.KeyValues)
                    {
                        unitValueDefinitions.Add(new UnitValueDefinition
                        {
                            ModuleCategory = $", {GetFileDescription(valueSection.File.OriginalFileName)}",
                            Default = unitKeyValue.Value,
                            Description = unitKeyValue.Comment,
                            Key = unitKeyValue.Key,
                            ValueList = unitKeyValue.Comment != null ? DetectValueList(unitKeyValue.Comment) : null,
                            LookupType = unitKeyValue.Comment != null ? DetectLookupType(unitKeyValue.Comment) : null,
                            MultipleValues = unitKeyValue.Comment?.StartsWith("list of ") ?? false
                        });
                    }
                }
            }
        }

        private List<CommonValueDefinition>? GetCommonValueDefinitions(string sectionName)
        {
            switch (sectionName)
            {
                case "General":
                    return CommonGeneral;
                case "AI":
                    return AIGeneral;
                case "AudioVisual":
                    return AudioVisualValues;
            }

            return null;
        }

        private List<UnitValueDefinition>? GetUnitValueDefinitions(string sectionName)
        {
            switch (sectionName)
            {
                case "SOMETECHNO":
                case "SOMETECHNO1":
                    return AllUnits;
                case "SOMEWEAPON":
                    return Weapons;
                case "SOMEWARHEAD":
                    return Warheads;
                case "SOMEBUILDING":
                case "UPGRADENAME":
                    return BuildingUnits;
                case "SOMEINFANTRY":
                    return InfantryUnits;
                case "SOMEPROJECTILE":
                    return Projectiles;
                case "SOMESW":
                    return SuperWeapons;
                case "SOMEANIM":
                    return Animations;
            }
            return AdditionalTypes.FirstOrDefault(t => t.TemplateSection == sectionName)?.ValueDefinitions;
        }

        private string GetFileDescription(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName.TrimStart("Rules.").TrimStart("Art."));
        }

        private string? DetectValueList(string comment)
        {
            var valueListMatch = ValueListRegEx.Match(comment);
            if (valueListMatch.Success)
            {
                return string.Join(",",
                    valueListMatch.Groups[1].Value.Split(new[] { "|", "/" }, StringSplitOptions.RemoveEmptyEntries));
            }
            return null;
        }

        private string? DetectLookupType(string comment)
        {
            var trimmedComment = comment.StartsWith("list of ", StringComparison.InvariantCultureIgnoreCase)
                ? comment.Substring(8)
                : comment;
            if (trimmedComment.EndsWith("Type") && !trimmedComment.Contains(" "))
            {
                return $"{trimmedComment}s";
            }
            if (trimmedComment.EndsWith("Types") && !trimmedComment.Contains(" "))
            {
                return trimmedComment;
            }
            if (trimmedComment == "Animation")
            {
                return "Animations";
            }
            if (trimmedComment == "Sound")
            {
                return "Sounds";
            }
            if (trimmedComment == "ParticleSystem")
            {
                return "ParticleSystems";
            }
            return null;
        }
    }
}
