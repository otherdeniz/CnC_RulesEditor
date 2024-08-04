namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    /// <summary>
    /// Basis Form für Popup's
    /// Die 'MayClose()' methode macht den Dialog 1 sek verzögert zu, ausser der user hat die Maus über dem Popup dann gehts erst zu wenn die maus raus ist
    /// </summary>
    public partial class PopupFormBase : Form
    {
        public PopupFormBase()
        {
            InitializeComponent();
        }

        public void MayClose()
        {
            this.timerClose.Enabled = true;
        }

        private void PopupFormBase_Deactivate(object sender, EventArgs e)
        {
            this.timerClose.Enabled = false;
            Close();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            try
            {
                var mouseClientPos = PointToClient(MousePosition);
                if (mouseClientPos.X < 0 || mouseClientPos.Y < 0 || mouseClientPos.X > Width || mouseClientPos.Y > Height)
                {
                    Close();
                }
            }
            catch (Exception)
            {
                // object disposed
            }
        }

        private void PopupFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timerClose.Stop();
        }
    }
}