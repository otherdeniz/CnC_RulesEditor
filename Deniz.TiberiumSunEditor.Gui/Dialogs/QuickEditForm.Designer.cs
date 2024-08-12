namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class QuickEditForm
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
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickEditForm));
            ultraTabEntities = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            buttonClose = new Button();
            ((System.ComponentModel.ISupportInitialize)ultraTabEntities).BeginInit();
            ultraTabEntities.SuspendLayout();
            SuspendLayout();
            // 
            // ultraTabEntities
            // 
            ultraTabEntities.Controls.Add(ultraTabSharedControlsPage1);
            ultraTabEntities.Dock = DockStyle.Fill;
            ultraTabEntities.Location = new Point(4, 4);
            ultraTabEntities.Name = "ultraTabEntities";
            appearance11.FontData.BoldAsString = "True";
            ultraTabEntities.SelectedTabAppearance = appearance11;
            ultraTabEntities.SharedControlsPage = ultraTabSharedControlsPage1;
            ultraTabEntities.Size = new Size(939, 698);
            ultraTabEntities.TabIndex = 0;
            ultraTabEntities.TabPadding = new Size(2, 2);
            // 
            // ultraTabSharedControlsPage1
            // 
            ultraTabSharedControlsPage1.Location = new Point(1, 20);
            ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            ultraTabSharedControlsPage1.Size = new Size(935, 675);
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonClose.Image = (Image)resources.GetObject("buttonClose.Image");
            buttonClose.Location = new Point(840, 666);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(90, 26);
            buttonClose.TabIndex = 2;
            buttonClose.Text = "Close";
            buttonClose.TextAlign = ContentAlignment.MiddleRight;
            buttonClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // QuickEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(947, 706);
            Controls.Add(buttonClose);
            Controls.Add(ultraTabEntities);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "QuickEditForm";
            Padding = new Padding(4);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quick Edit";
            Load += QuickEditForm_Load;
            ((System.ComponentModel.ISupportInitialize)ultraTabEntities).EndInit();
            ultraTabEntities.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabEntities;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Button buttonClose;
    }
}