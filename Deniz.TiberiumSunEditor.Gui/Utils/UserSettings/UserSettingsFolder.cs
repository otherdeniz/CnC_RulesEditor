namespace Deniz.TiberiumSunEditor.Gui.Utils.UserSettings
{
    public class UserSettingsFolder
    {
        private static UserSettingsFolder? _instance;
        public static UserSettingsFolder Instance => _instance ??= InitFolder();

        public static UserSettingsFolder InitFolder()
        {
            var appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TiberiumSunEditor");
            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
            return new UserSettingsFolder(appDataFolder);
        }

        public UserSettingsFolder(string appDataFolder)
        {
            AppDataFolder = appDataFolder;
        }

        public string AppDataFolder { get; }

        public string SaveFile(string fileName, byte[] fileData)
        {
            var filePath = Path.Combine(AppDataFolder, fileName);
            using (var fs = File.Create(filePath))
            {
                fs.Write(fileData);
            }
            return filePath;
        }

        public byte[] LoadFile(string fileName)
        {
            return File.ReadAllBytes(Path.Combine(AppDataFolder, fileName));
        }
    }
}
