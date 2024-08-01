using Deniz.TiberiumSunEditor.Gui.Utils;
using Deniz.TiberiumSunEditor.Gui.Utils.Datastructure;
using Deniz.TiberiumSunEditor.Gui.Utils.EqualityComparer;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;
using Deniz.TiberiumSunEditor.Gui.Utils.UserSettings;

namespace Deniz.TiberiumSunEditor.Gui.Model
{
    public class GameEntityModel
    {
        public GameEntityModel(RootModel rootModel,
            string entityType,
            IniFileSection fileSection,
            IniFileSection? defaultSection,
            List<UnitValueModel> unitValueList)
        {
            RootModel = rootModel;
            EntityType = entityType;
            FileSection = fileSection;
            DefaultSection = defaultSection;
            EntityValueList = unitValueList.Select(u =>
                new EntityValueModel(
                    this,
                    u.Category,
                    fileSection,
                    defaultSection,
                    u.UnitValueDefinition.Key,
                    u.UnitValueDefinition))
                .Union(fileSection.KeyValues.Where(k =>
                    !unitValueList.Any(v => k.Key == v.UnitValueDefinition.Key))
                    .Select(k => new EntityValueModel(
                        this,
                        "Other values",
                        fileSection,
                        defaultSection,
                        k.Key,
                        new UnitValueDefinition {Key = k.Key})))
                .ToList();
        }

        public RootModel RootModel { get; }

        public string EntityType { get; }

        public string EntityKey => FileSection.SectionName ?? "_";

        public string EntityName => FileSection.GetValue("Name")?.Value
                                    ?? (DefaultSection ?? FileSection).HeaderComments.FirstOrDefault()?.Comment
                                    ?? "";

        public List<string> Sides
        {
            get
            {
                var houses = FileSection.GetValue("Owner")?.Value.Split(",");
                if (houses?.Any() == true)
                {
                    return RootModel.Sides
                        .Where(s => s.Value.Any(v => houses.Contains(v, StringEqualityComparer.Instance)))
                        .Select(s => s.Key)
                        .ToList();
                }
                return new List<string>();
            }
        }

        public ThumbnailModel? Thumbnail
        {
            get
            {
                if (EntityType == "Warheads")
                {
                    var animKeys = FileSection.GetValue("AnimList")?.Value;
                    if (!string.IsNullOrEmpty(animKeys))
                    {
                        return new ThumbnailModel(animKeys);
                    }
                    return null;
                }
                if (EntityType == "Weapons")
                {
                    var warheadValue = FileSection.GetValue("Warhead")?.Value;
                    if (warheadValue != null)
                    {
                        var animKeys = RootModel.FindSection(warheadValue)?.GetValue("AnimList")?.Value;
                        if (!string.IsNullOrEmpty(animKeys))
                        {
                            return new ThumbnailModel(animKeys);
                        }
                        return null;
                    }
                }
                var imageKey = FileSection.GetValue("Image")?.Value;
                if (string.IsNullOrEmpty(imageKey) || imageKey == "null")
                {
                    imageKey = EntityKey;
                }

                var image = CCGameRepository.Instance.GetCameo(imageKey)
                            ?? BitmapRepository.Instance.GetBitmap(imageKey);
                return image != null 
                    ? new ThumbnailModel(image) 
                    : null;
            }
        }

        public int ModificationCount =>
            FileSection.KeyValues.Count(v => 
                !string.Equals(v.Value, 
                    DefaultSection?.GetValue(v.Key)?.Value ?? "", 
                    StringComparison.InvariantCultureIgnoreCase));

        public IniFileSection FileSection { get; }

        public IniFileSection? DefaultSection { get; }

        public List<EntityValueModel> EntityValueList { get; }

        public bool Favorite
        {
            get => UserSettingsFile.Instance.SectionsSettings.IsFavorite(FileSection.SectionName ?? "_");
            set
            {
                UserSettingsFile.Instance.SectionsSettings.SetFavorite(FileSection.SectionName ?? "_", value);
                UserSettingsFile.Instance.Save();
            }
        }

    }
}
