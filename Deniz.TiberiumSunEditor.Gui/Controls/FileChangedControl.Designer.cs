namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class FileChangedControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            StopFileWatcher();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelMessage = new Label();
            labelReload = new Label();
            SuspendLayout();
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.Dock = DockStyle.Left;
            labelMessage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelMessage.ForeColor = Color.FromArgb(192, 0, 0);
            labelMessage.Location = new Point(0, 0);
            labelMessage.Margin = new Padding(4, 0, 4, 0);
            labelMessage.Name = "labelMessage";
            labelMessage.Padding = new Padding(2, 0, 6, 0);
            labelMessage.Size = new Size(139, 15);
            labelMessage.TabIndex = 1;
            labelMessage.Tag = "MODIFIED";
            labelMessage.Text = "Changes not saved yet";
            // 
            // labelReload
            // 
            labelReload.AutoSize = true;
            labelReload.Cursor = Cursors.Hand;
            labelReload.Dock = DockStyle.Left;
            labelReload.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            labelReload.ForeColor = Color.DarkBlue;
            labelReload.Location = new Point(139, 0);
            labelReload.Name = "labelReload";
            labelReload.Size = new Size(146, 15);
            labelReload.TabIndex = 7;
            labelReload.Text = "RELOAD FILE FROM DISK";
            labelReload.Visible = false;
            labelReload.Click += labelReload_Click;
            // 
            // FileChangedControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(labelReload);
            Controls.Add(labelMessage);
            Name = "FileChangedControl";
            Size = new Size(300, 23);
            Load += FileChangedControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelMessage;
        private Label labelReload;
    }
}
