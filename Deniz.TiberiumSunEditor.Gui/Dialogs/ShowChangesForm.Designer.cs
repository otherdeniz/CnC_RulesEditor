namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class ShowChangesForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowChangesForm));
            panelBottom = new Panel();
            buttonClose = new Button();
            rulesEdit = new Controls.RulesEditMainControl();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonClose);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 499);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(819, 43);
            panelBottom.TabIndex = 2;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.Image = (Image)resources.GetObject("buttonClose.Image");
            buttonClose.Location = new Point(717, 9);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(90, 26);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "Close";
            buttonClose.TextAlign = ContentAlignment.MiddleRight;
            buttonClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // rulesEdit
            // 
            rulesEdit.Dock = DockStyle.Fill;
            rulesEdit.Location = new Point(0, 0);
            rulesEdit.Margin = new Padding(4, 3, 4, 3);
            rulesEdit.Name = "rulesEdit";
            rulesEdit.ReadonlyMode = true;
            rulesEdit.Size = new Size(819, 499);
            rulesEdit.TabIndex = 3;
            rulesEdit.TitleVisible = false;
            // 
            // ShowChangesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonClose;
            ClientSize = new Size(819, 542);
            Controls.Add(rulesEdit);
            Controls.Add(panelBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ShowChangesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "All Changes";
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBottom;
        private Button buttonClose;
        private Controls.RulesEditMainControl rulesEdit;
    }
}