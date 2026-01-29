using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;
using Deniz.TiberiumSunEditor.Gui.Model.Interface;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class GameEntityModel
    {
        private readonly IniFileSection? _rulesFileSection;
        private int? _typesIndex;

        public GameEntityModel(RulesRootModel rulesRootModel,
            IRootModel rootModel,
            string entityType,
            IniFileSection fileSection,
            IniFileSection? defaultSection,
            List<CategorizedValueDefinition>? unitValueList,
            IniFileSection? rulesFileSection = null)
        {
            _rulesFileSection = rulesFileSection;
            RulesRootModel = rulesRootModel;
            RootModel = rootModel;
            EntityType = entityType;
            FileSection = fileSection;
            DefaultSection = defaultSection;
            if (unitValueList == null)
            {
                EntityValueList = new List<EntityValueModel>();
                return;
            }
            EntityValueList = unitValueList.Select(u =>
                new EntityValueModel(
                    this,
                    u.Category,
                    fileSection,
                    defaultSection,
                    u.UnitValueDefinition.Key,
                    u.UnitValueDefinition))
                .Union(fileSection.KeyValues.Where(k =>
                    !unitValueList.Any(v => k.Key == v.UnitValueDefinition.Key 
                                            || k.Key == "BaseSection"
                                            || k.Key == "$Inherits"))
                    .Select(k => new EntityValueModel(
                        this,
                        "9) Other values",
                        fileSection,
                        defaultSection,
                        k.Key,
                        new UnitValueDefinition {Key = k.Key, DetectTypeAtRuntime = true})))
                .ToList();
            if (rootModel is AiRootModel) return;
            if (rulesRootModel.UseSectionInheritance)
            {
                EntityValueList.Add(new EntityValueModel(
                    this,
                    "0) Section Inheritance",
                    fileSection,
                    defaultSection,
                    "BaseSection",
                    new UnitValueDefinition
                    {
                        Key = "BaseSection", 
                        LookupType = "self",
                        Description = "The base section from which this entity inherits all values"
                    }
                ));
            }
            else if (rulesRootModel.UsePhobosSectionInheritance)
            {
                EntityValueList.Add(new EntityValueModel(
                    this,
                    "0) Section Inheritance",
                    fileSection,
                    defaultSection,
                    "$Inherits",
                    new UnitValueDefinition
                    {
                        Key = "$Inherits",
                        LookupType = "self",
                        MultipleValues = true,
                        Description = "List of base sections from which this entity inherits all values (logic: first wins, depth-first)"
                    }
                ));
            }
        }

        public RulesRootModel RulesRootModel { get; }

        public IRootModel RootModel { get; set; }

        public string EntityType { get; }

        public int? TypesIndex
        {
            get
            {
                if (_typesIndex == null)
                {
                    _typesIndex = RulesRootModel.DefaultFile.GetSection(EntityType)?.KeyValues.FindIndex(k =>
                                      k.Value.Equals(EntityKey, StringComparison.InvariantCultureIgnoreCase))
                                  ?? -1;
                }
                return _typesIndex == -1
                    ? null
                    : _typesIndex;
            }
        }

        public string EntityKey => FileSection.SectionName ?? "_";

        public IniFileSection FileSection { get; }

        public IniFileSection? DefaultSection { get; }

        public IniFileSection RulesFileSection => _rulesFileSection ?? FileSection;

        public List<EntityValueModel> EntityValueList { get; }

        public string EntityName => GetRulesFileValue("Name")?.Value
                                    ?? (DefaultSection ?? FileSection).HeaderComments.FirstOrDefault()?.Comment
                                    ?? "";

        public string GetNameOrKey()
        {
            var name = EntityName;
            return string.IsNullOrEmpty(name) 
                ? EntityKey 
                : name;
        }

        public bool TechLevelBuildable
        {
            get
            {
                var techLevelValue =
                    (RulesFileSection.GetValue("TechLevel") ?? DefaultSection?.GetValue("TechLevel"))?.Value;
                if (!string.IsNullOrEmpty(techLevelValue)
                    && int.TryParse(techLevelValue, out var techLevelNumber))
                {
                    return techLevelNumber > 0;
                }
                return false;
            }
        }

        public List<string> Sides
        {
            get
            {
                var houses = GetRulesFileValue("Owner")?.Value.Split(",");
                if (houses?.Any() == true)
                {
                    return RulesRootModel.Sides
                        .Where(s => s.Value.Any(v => houses.Contains(v, StringEqualityComparer.Instance)))
                        .Select(s => s.Key)
                        .ToList();
                }
                return new List<string>();
            }
        }

        public ThumbnailModel? Thumbnail => GetThumbnail(false);

        public int ModificationCount =>
            FileSection.KeyValues.Count(v => 
                !string.Equals(v.Value, 
                    DefaultSection?.GetValue(v.Key)?.Value ?? "", 
                    StringComparison.InvariantCultureIgnoreCase));

        public Func<int>? InfoNumberFunction { get; set; }

        public bool Favorite
        {
            get => UserSettingsFile.Instance.SectionsSettings.IsFavorite(FileSection.SectionName ?? "_");
            set
            {
                UserSettingsFile.Instance.SectionsSettings.SetFavorite(FileSection.SectionName ?? "_", value);
                UserSettingsFile.Instance.Save();
            }
        }

        public ThumbnailModel? GetThumbnail(bool forceReload)
        {
            if (EntityType == "Warheads")
            {
                var animKeys = GetRulesFileValue("AnimList")?.Value;
                if (!string.IsNullOrEmpty(animKeys))
                {
                    return new ThumbnailModel(animKeys);
                }
                return null;
            }
            if (EntityType == "Weapons")
            {
                var warheadValue = GetRulesFileValue("Warhead")?.Value;
                if (warheadValue != null)
                {
                    var animKeys = RulesRootModel.FindSection(warheadValue)?.GetValue("AnimList")?.Value;
                    if (!string.IsNullOrEmpty(animKeys))
                    {
                        return new ThumbnailModel(animKeys);
                    }
                    return null;
                }
            }
            if (EntityType == "Projectiles"
                || EntityType == "Particles")
            {
                var animKey = GetRulesFileValue("Image")?.Value;
                if (!string.IsNullOrEmpty(animKey))
                {
                    return new ThumbnailModel(animKey);
                }
                var trailerKey = FileSection.GetValue("Trailer")?.Value;
                if (!string.IsNullOrEmpty(trailerKey))
                {
                    return new ThumbnailModel(trailerKey);
                }
                return null;
            }
            if (EntityType == "ParticleSystems")
            {
                var particleValue = GetRulesFileValue("HoldsWhat")?.Value;
                if (particleValue != null)
                {
                    var animKey = RulesRootModel.FindSection(particleValue)?.GetValue("Image")?.Value;
                    if (!string.IsNullOrEmpty(animKey))
                    {
                        return new ThumbnailModel(animKey);
                    }
                    return null;
                }
            }
            if (EntityType == "Animations")
            {
                return new ThumbnailModel(EntityKey);
            }
            if (EntityType == "Sides")
            {
                var sideName = GetRulesFileValue("Side")?.Value;
                var sideDefinition = RulesRootModel.FileType.GameDefinition.Sides
                    .FirstOrDefault(d => d.Name.Equals(sideName, StringComparison.InvariantCultureIgnoreCase));
                if (sideDefinition != null)
                {
                    return new ThumbnailModel(BitmapRepository.Instance.BlankImage.OverlayImage(sideDefinition.GetLogoImage()));
                }
                return null;
            }
            var sidebarImageShpName = GetRulesFileValue("SidebarImage")?.Value;
            if (sidebarImageShpName != null)
            {
                var shpImage = CCGameRepository.Instance.GetCameoByShp(sidebarImageShpName, false);
                if (shpImage != null)
                {
                    return new ThumbnailModel(shpImage);
                }
            }
            var sidebarImagePcxName = GetRulesFileValue("SidebarPCX")?.Value;
            if (sidebarImagePcxName != null)
            {
                var pcxImage = CCGameRepository.Instance.GetCameoByPcx(sidebarImagePcxName, false);
                if (pcxImage != null)
                {
                    return new ThumbnailModel(pcxImage);
                }
            }
            var imageKey = GetRulesFileValue("Image")?.Value;
            if (string.IsNullOrEmpty(imageKey) || imageKey == "null")
            {
                imageKey = EntityKey;
            }

            var artSection = RootModel is ArtRootModel ? FileSection : null;
            var image = CCGameRepository.Instance.GetCameo(imageKey, true, artSection)
                        ?? BitmapRepository.Instance.GetBitmap(imageKey);
            return image != null
                ? new ThumbnailModel(image)
                : null;
        }

        public int GetInfoNumber()
        {
            return InfoNumberFunction?.Invoke() 
                   ?? ModificationCount;
        }

        public bool IsBuildableByHouse(string houseKey)
        {
            var allowedByOwner = TechLevelBuildable
                                 && (RulesFileSection.GetValue("Owner") ?? DefaultSection?.GetValue("Owner"))?
                                 .Value.Split(",")
                                 .Any(v => v.Equals(houseKey, StringComparison.InvariantCultureIgnoreCase)) == true;
            if (allowedByOwner)
            {
                var requiredByHousesValue = RulesFileSection.GetValue("RequiredHouses")?.Value;
                if (requiredByHousesValue != null)
                {
                    return requiredByHousesValue.Split(",")
                        .Any(v => v.Equals(houseKey, StringComparison.InvariantCultureIgnoreCase));
                }
                var forbiddenByHousesValue = RulesFileSection.GetValue("ForbiddenHouses")?.Value;
                if (forbiddenByHousesValue != null)
                {
                    return !forbiddenByHousesValue.Split(",")
                        .Any(v => v.Equals(houseKey, StringComparison.InvariantCultureIgnoreCase));
                }
                // check if has prerequisite that is not owned
                if ((RulesFileSection.GetValue("Prerequisite") ?? DefaultSection?.GetValue("Prerequisite"))?
                        .Value.Split(",")
                        .Select(p =>
                            RulesRootModel.File.GetSection(p) ??
                            RulesRootModel.DefaultFile.GetSection(p))
                        .Where(s => s != null)
                        .Any(s => s!.GetValue("Owner")?.Value.Split(",")
                            .Any(v => v.Equals(houseKey, StringComparison.InvariantCultureIgnoreCase)) == false
                            ) == true)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private IniFileLineKeyValue? GetRulesFileValue(string key)
        {
            return FileSection != RulesFileSection 
                ? RulesFileSection.GetValue(key)
                : FileSection.GetValue(key) ?? DefaultSection?.GetValue(key);
        }
    }
}
