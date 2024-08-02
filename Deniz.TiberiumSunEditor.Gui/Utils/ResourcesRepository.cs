namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class ResourcesRepository
    {
        private static ResourcesRepository? _instance;
        public static ResourcesRepository Instance => _instance ??= new ResourcesRepository();

        private readonly string _resourcesPath;

        public ResourcesRepository()
        {
            _resourcesPath = Path.Combine(Application.StartupPath, "Resources");
        }

        public string ResourcesPath => _resourcesPath;

        public byte[] ReadResourcesFile(string fileName)
        {
            return File.ReadAllBytes(Path.Combine(_resourcesPath, fileName));
        }

        public FileStream ReadResourcesFileStream(string fileName)
        {
            return File.OpenRead(Path.Combine(_resourcesPath, fileName));
        }

        public FileStream ReadRandomResourcesFileStream(string searchPattern)
        {
            var matchedFiles = Directory.GetFiles(_resourcesPath, searchPattern);
            var rnd = matchedFiles.Length > 1 
                ? Random.Shared.Next(0, matchedFiles.Length - 1)
                : 0;
            return File.OpenRead(matchedFiles[rnd]);
        }
    }
}
