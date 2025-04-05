namespace Deniz.TiberiumSunEditor.Gui.Utils.UserSettings
{
    public class EntityGroupSetting
    {
        public static readonly string NewGroupName = "new group";

        /// <summary>
        /// GameKey or Mod-Key
        /// </summary>
        public string GameKey { get; set; } = string.Empty;

        public string EntityType { get; set; } = string.Empty;

        public string GroupName { get; set; } = NewGroupName;

        public Color GroupColor { get; set; } = Color.Red;

        public List<string> Keys { get; set; } = new();
    }
}
