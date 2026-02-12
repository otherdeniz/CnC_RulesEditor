namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class RulesToArtFileAssignmentControl
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
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelOriginalPath = new Label();
            labelOriginalTitle = new Label();
            labelModify = new Label();
            SuspendLayout();
            // 
            // labelOriginalPath
            // 
            labelOriginalPath.AutoSize = true;
            labelOriginalPath.Dock = DockStyle.Left;
            labelOriginalPath.Location = new Point(59, 0);
            labelOriginalPath.Margin = new Padding(4, 0, 4, 0);
            labelOriginalPath.Name = "labelOriginalPath";
            labelOriginalPath.Size = new Size(55, 15);
            labelOriginalPath.TabIndex = 7;
            labelOriginalPath.Text = "(original)";
            // 
            // labelOriginalTitle
            // 
            labelOriginalTitle.AutoSize = true;
            labelOriginalTitle.Dock = DockStyle.Left;
            labelOriginalTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelOriginalTitle.Location = new Point(0, 0);
            labelOriginalTitle.Margin = new Padding(4, 0, 4, 0);
            labelOriginalTitle.Name = "labelOriginalTitle";
            labelOriginalTitle.Padding = new Padding(8, 0, 0, 0);
            labelOriginalTitle.Size = new Size(59, 15);
            labelOriginalTitle.TabIndex = 6;
            labelOriginalTitle.Text = "Art-File:";
            // 
            // labelModify
            // 
            labelModify.AutoSize = true;
            labelModify.Cursor = Cursors.Hand;
            labelModify.Dock = DockStyle.Left;
            labelModify.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            labelModify.ForeColor = Color.DarkBlue;
            labelModify.Location = new Point(114, 0);
            labelModify.Name = "labelModify";
            labelModify.Size = new Size(53, 15);
            labelModify.TabIndex = 8;
            labelModify.Text = "MODIFY";
            labelModify.Visible = false;
            labelModify.Click += labelModify_Click;
            // 
            // RulesToArtFileAssignmentControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(labelModify);
            Controls.Add(labelOriginalPath);
            Controls.Add(labelOriginalTitle);
            Name = "RulesToArtFileAssignmentControl";
            Size = new Size(300, 23);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelOriginalPath;
        private Label labelOriginalTitle;
        private Label labelModify;
    }
}
