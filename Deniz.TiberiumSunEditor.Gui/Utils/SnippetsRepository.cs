using Deniz.TiberiumSunEditor.Gui.Model;
using Deniz.TiberiumSunEditor.Gui.Utils.Files;

namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class SnippetsRepository
    {
        private static SnippetsRepository? _instance;
        public static SnippetsRepository Instance => _instance ??= new SnippetsRepository();

        private readonly string _snippetsPath;

        public SnippetsRepository()
        {
            _snippetsPath = Path.Combine(Application.StartupPath, "Snippets");
        }

        public List<SnippetModel> GetSnippets(string editionFolder, IniFile defaulFile)
        {
            var snippetsList = new List<SnippetModel>();
            var snippetsFolder = Path.Combine(_snippetsPath, editionFolder);
            if (Directory.Exists(snippetsFolder))
            {
                foreach (var filePath in Directory.GetFiles(snippetsFolder, "*.ini"))
                {
                    snippetsList.Add(new SnippetModel("[root]",
                        Path.GetFileNameWithoutExtension(filePath),
                        IniFile.Load(filePath),
                        defaulFile));
                }
                foreach (var subDir in Directory.GetDirectories(snippetsFolder))
                {
                    foreach (var filePath in Directory.GetFiles(subDir, "*.ini"))
                    {
                        snippetsList.Add(new SnippetModel(Path.GetFileName(subDir),
                            Path.GetFileNameWithoutExtension(filePath),
                            IniFile.Load(filePath),
                            defaulFile));
                    }
                }
            }
            else
            {
                MessageBox.Show($"The directory '{snippetsFolder}' does not exist.");
            }
            return snippetsList;
        }
    }
}
