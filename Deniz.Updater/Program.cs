namespace Deniz.Updater
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// args:
        /// 1 : download url to zip-fil
        /// 2 : (optional) exe filename to autostart after update
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (args.Length == 0)
            {
                MessageBox.Show("download URL parameter missing");
                return;
            }
            var downloadUrl = args[0];
            string? autoStartExe = null;
            if (args.Length == 2)
            {
                autoStartExe = args[1];
            }
            var updateForm = new UpdateForm();
            updateForm.InitialiseUpdate(downloadUrl, autoStartExe);
            Application.Run(updateForm);
        }
    }
}