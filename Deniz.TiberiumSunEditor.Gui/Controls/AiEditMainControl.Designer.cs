namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class AiEditMainControl
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
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab16 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab17 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            ultraTabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            entitiesListTaskForces = new EntitiesListControl();
            ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            panelTitle = new Panel();
            labelName = new Label();
            labelType = new Label();
            mainTab = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            ultraTabPageControl7.SuspendLayout();
            panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainTab).BeginInit();
            mainTab.SuspendLayout();
            SuspendLayout();
            // 
            // ultraTabPageControl7
            // 
            ultraTabPageControl7.Controls.Add(entitiesListTaskForces);
            ultraTabPageControl7.Location = new Point(2, 30);
            ultraTabPageControl7.Name = "ultraTabPageControl7";
            ultraTabPageControl7.Size = new Size(963, 412);
            // 
            // entitiesListTaskForces
            // 
            entitiesListTaskForces.Dock = DockStyle.Fill;
            entitiesListTaskForces.Location = new Point(0, 0);
            entitiesListTaskForces.Name = "entitiesListTaskForces";
            entitiesListTaskForces.Size = new Size(963, 412);
            entitiesListTaskForces.TabIndex = 0;
            // 
            // ultraTabPageControl1
            // 
            ultraTabPageControl1.Location = new Point(-10000, -10000);
            ultraTabPageControl1.Name = "ultraTabPageControl1";
            ultraTabPageControl1.Size = new Size(963, 412);
            // 
            // panelTitle
            // 
            panelTitle.Controls.Add(labelName);
            panelTitle.Controls.Add(labelType);
            panelTitle.Dock = DockStyle.Top;
            panelTitle.Location = new Point(0, 0);
            panelTitle.Margin = new Padding(4, 3, 4, 3);
            panelTitle.Name = "panelTitle";
            panelTitle.Padding = new Padding(2);
            panelTitle.Size = new Size(967, 23);
            panelTitle.TabIndex = 4;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Dock = DockStyle.Left;
            labelName.Location = new Point(50, 2);
            labelName.Margin = new Padding(4, 0, 4, 0);
            labelName.Name = "labelName";
            labelName.Size = new Size(45, 15);
            labelName.TabIndex = 1;
            labelName.Text = "(name)";
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.Dock = DockStyle.Left;
            labelType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            labelType.Location = new Point(2, 2);
            labelType.Margin = new Padding(4, 0, 4, 0);
            labelType.Name = "labelType";
            labelType.Padding = new Padding(2, 0, 6, 0);
            labelType.Size = new Size(48, 15);
            labelType.TabIndex = 0;
            labelType.Text = "(type)";
            // 
            // mainTab
            // 
            mainTab.Controls.Add(ultraTabSharedControlsPage1);
            mainTab.Controls.Add(ultraTabPageControl7);
            mainTab.Controls.Add(ultraTabPageControl1);
            mainTab.Dock = DockStyle.Fill;
            mainTab.Location = new Point(0, 23);
            mainTab.Margin = new Padding(4, 3, 4, 3);
            mainTab.Name = "mainTab";
            appearance11.FontData.BoldAsString = "True";
            mainTab.SelectedTabAppearance = appearance11;
            mainTab.SharedControlsPage = ultraTabSharedControlsPage1;
            mainTab.Size = new Size(967, 444);
            mainTab.TabIndex = 5;
            mainTab.TabLayoutStyle = Infragistics.Win.UltraWinTabs.TabLayoutStyle.MultiRowAutoSize;
            mainTab.TabPadding = new Size(1, 3);
            ultraTab16.Key = "TaskForces";
            ultraTab16.TabPage = ultraTabPageControl7;
            ultraTab16.Text = "Task Forces";
            ultraTab17.Key = "Scripts";
            ultraTab17.TabPage = ultraTabPageControl1;
            ultraTab17.Text = "Scripts";
            mainTab.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { ultraTab16, ultraTab17 });
            mainTab.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // ultraTabSharedControlsPage1
            // 
            ultraTabSharedControlsPage1.Location = new Point(-10000, -10000);
            ultraTabSharedControlsPage1.Margin = new Padding(4, 3, 4, 3);
            ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            ultraTabSharedControlsPage1.Size = new Size(963, 412);
            // 
            // AiEditMainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainTab);
            Controls.Add(panelTitle);
            Name = "AiEditMainControl";
            Size = new Size(967, 467);
            ultraTabPageControl7.ResumeLayout(false);
            panelTitle.ResumeLayout(false);
            panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mainTab).EndInit();
            mainTab.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTitle;
        private Label labelName;
        private Label labelType;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl mainTab;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl7;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private EntitiesListControl entitiesListTaskForces;
    }
}
