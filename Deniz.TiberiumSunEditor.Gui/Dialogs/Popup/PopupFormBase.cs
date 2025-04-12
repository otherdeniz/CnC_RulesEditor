namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    /// <summary>
    /// Basis Form für Popup's
    /// Die 'MayClose()' methode macht den Dialog 1 sek verzögert zu, ausser der user hat die Maus über dem Popup dann gehts erst zu wenn die maus raus ist
    /// </summary>
    public partial class PopupFormBase : Form
    {
        private bool _isClosed;

        public PopupFormBase()
        {
            InitializeComponent();
        }

        public void MayClose()
        {
            this.timerClose.Enabled = true;
        }

        public void ForceClose()
        {
            if (!_isClosed)
            {
                this.timerClose.Enabled = false;
                Close();
            }
            _isClosed = true;
        }

        private void PopupFormBase_Deactivate(object sender, EventArgs e)
        {
            ForceClose();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            try
            {
                var mouseClientPos = PointToClient(MousePosition);
                if (mouseClientPos.X < 0 || mouseClientPos.Y < 0 || mouseClientPos.X > Width || mouseClientPos.Y > Height)
                {
                    ForceClose();
                }
            }
            catch (Exception)
            {
                // object disposed
            }
        }

        private void PopupFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timerClose.Enabled = false;
        }
    }
}