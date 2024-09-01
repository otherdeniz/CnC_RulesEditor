using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Model.Interface
{
    public interface IRootModel
    {
        public event EventHandler<GlobalEntityNotificationEventArgs>? GlobalEntityNotification;

        public IniFile File { get; }

        public IniFile DefaultFile { get; }

        public FileTypeModel FileType { get; }

        public RulesRootModel RulesModel { get; }

        public List<LookupItemModel> LookupItems { get; }

        public List<EntityTypeEditControlTypeModel> EntityTypeEditControl { get; }

        public Dictionary<string, List<GameEntityModel>> LookupEntities { get; }

        public void RaiseGlobalEntityNotification(string entitiyKey, string notificationName);
    }

    public class GlobalEntityNotificationEventArgs : EventArgs
    {
        public GlobalEntityNotificationEventArgs(string entitiyKey, string notificationName)
        {
            EntitiyKey = entitiyKey;
            NotificationName = notificationName;
        }

        public string EntitiyKey { get; }

        public string NotificationName { get; }
    }
}
