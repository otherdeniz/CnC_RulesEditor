namespace Deniz.TiberiumSunEditor.Gui.Dialogs.Popup
{
    partial class PopupFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerClose = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // timerClose
            // 
            timerClose.Tick += timerClose_Tick;
            // 
            // PopupFormBase
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(384, 347);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PopupFormBase";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Popup";
            Deactivate += PopupFormBase_Deactivate;
            FormClosed += PopupFormBase_FormClosed;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerClose;
    }
}