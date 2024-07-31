namespace Deniz.TiberiumSunEditor.Gui.Utils
{
    public class LogoRepository
    {
        private static LogoRepository? _instance;
        public static LogoRepository Instance => _instance ??= new LogoRepository();

        private readonly string _logosPath;

        public LogoRepository()
        {
            _logosPath = Path.Combine(Application.StartupPath, "Resources\\Logos");
        }

        public Image GetLogo(string fileName)
        {
            return Image.FromFile(Path.Combine(_logosPath, fileName));
        }
    }
}
