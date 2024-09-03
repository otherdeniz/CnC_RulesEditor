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
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab17 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab16 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiEditMainControl));
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab13 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            entitiesListScripts = new EntitiesListControl();
            ultraTabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            entitiesListTaskForces = new EntitiesListControl();
            ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            unitsListInfantry = new UnitsListControl();
            ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            unitsListVehicles = new UnitsListControl();
            ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            unitsListAircrafts = new UnitsListControl();
            ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            entitiesListTeams = new EntitiesListControl();
            panelTitle = new Panel();
            labelName = new Label();
            labelType = new Label();
            mainTab = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            ultraTabPageControl1.SuspendLayout();
            ultraTabPageControl7.SuspendLayout();
            ultraTabPageControl2.SuspendLayout();
            ultraTabPageControl3.SuspendLayout();
            ultraTabPageControl4.SuspendLayout();
            ultraTabPageControl5.SuspendLayout();
            panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mainTab).BeginInit();
            mainTab.SuspendLayout();
            SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            ultraTabPageControl1.Controls.Add(entitiesListScripts);
            ultraTabPageControl1.Location = new Point(2, 30);
            ultraTabPageControl1.Name = "ultraTabPageControl1";
            ultraTabPageControl1.Size = new Size(963, 412);
            // 
            // entitiesListScripts
            // 
            entitiesListScripts.Dock = DockStyle.Fill;
            entitiesListScripts.EntityType = "ScriptTypes";
            entitiesListScripts.Location = new Point(0, 0);
            entitiesListScripts.Name = "entitiesListScripts";
            entitiesListScripts.Size = new Size(963, 412);
            entitiesListScripts.TabIndex = 1;
            // 
            // ultraTabPageControl7
            // 
            ultraTabPageControl7.Controls.Add(entitiesListTaskForces);
            ultraTabPageControl7.Location = new Point(-10000, -10000);
            ultraTabPageControl7.Name = "ultraTabPageControl7";
            ultraTabPageControl7.Size = new Size(963, 412);
            // 
            // entitiesListTaskForces
            // 
            entitiesListTaskForces.Dock = DockStyle.Fill;
            entitiesListTaskForces.EntityType = "TaskForces";
            entitiesListTaskForces.Location = new Point(0, 0);
            entitiesListTaskForces.Name = "entitiesListTaskForces";
            entitiesListTaskForces.Size = new Size(963, 412);
            entitiesListTaskForces.TabIndex = 0;
            entitiesListTaskForces.AddEntityManual += entitiesListTaskForces_AddEntityManual;
            // 
            // ultraTabPageControl2
            // 
            ultraTabPageControl2.Controls.Add(unitsListInfantry);
            ultraTabPageControl2.Location = new Point(-10000, -10000);
            ultraTabPageControl2.Name = "ultraTabPageControl2";
            ultraTabPageControl2.Size = new Size(963, 412);
            // 
            // unitsListInfantry
            // 
            unitsListInfantry.Dock = DockStyle.Fill;
            unitsListInfantry.Location = new Point(0, 0);
            unitsListInfantry.Margin = new Padding(4, 3, 4, 3);
            unitsListInfantry.Name = "unitsListInfantry";
            unitsListInfantry.Size = new Size(963, 412);
            unitsListInfantry.TabIndex = 1;
            unitsListInfantry.Tag = "PLAIN";
            // 
            // ultraTabPageControl3
            // 
            ultraTabPageControl3.Controls.Add(unitsListVehicles);
            ultraTabPageControl3.Location = new Point(-10000, -10000);
            ultraTabPageControl3.Name = "ultraTabPageControl3";
            ultraTabPageControl3.Size = new Size(963, 412);
            // 
            // unitsListVehicles
            // 
            unitsListVehicles.Dock = DockStyle.Fill;
            unitsListVehicles.Location = new Point(0, 0);
            unitsListVehicles.Margin = new Padding(4, 3, 4, 3);
            unitsListVehicles.Name = "unitsListVehicles";
            unitsListVehicles.Size = new Size(963, 412);
            unitsListVehicles.TabIndex = 0;
            unitsListVehicles.Tag = "PLAIN";
            // 
            // ultraTabPageControl4
            // 
            ultraTabPageControl4.Controls.Add(unitsListAircrafts);
            ultraTabPageControl4.Location = new Point(-10000, -10000);
            ultraTabPageControl4.Name = "ultraTabPageControl4";
            ultraTabPageControl4.Size = new Size(963, 412);
            // 
            // unitsListAircrafts
            // 
            unitsListAircrafts.Dock = DockStyle.Fill;
            unitsListAircrafts.Location = new Point(0, 0);
            unitsListAircrafts.Margin = new Padding(4, 3, 4, 3);
            unitsListAircrafts.Name = "unitsListAircrafts";
            unitsListAircrafts.Size = new Size(963, 412);
            unitsListAircrafts.TabIndex = 1;
            unitsListAircrafts.Tag = "PLAIN";
            // 
            // ultraTabPageControl5
            // 
            ultraTabPageControl5.Controls.Add(entitiesListTeams);
            ultraTabPageControl5.Location = new Point(-10000, -10000);
            ultraTabPageControl5.Name = "ultraTabPageControl5";
            ultraTabPageControl5.Size = new Size(963, 412);
            // 
            // entitiesListTeams
            // 
            entitiesListTeams.Dock = DockStyle.Fill;
            entitiesListTeams.EntityType = "TeamTypes";
            entitiesListTeams.Location = new Point(0, 0);
            entitiesListTeams.Name = "entitiesListTeams";
            entitiesListTeams.Size = new Size(963, 412);
            entitiesListTeams.TabIndex = 1;
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
            mainTab.Controls.Add(ultraTabPageControl2);
            mainTab.Controls.Add(ultraTabPageControl3);
            mainTab.Controls.Add(ultraTabPageControl4);
            mainTab.Controls.Add(ultraTabPageControl5);
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
            ultraTab17.Key = "Scripts";
            ultraTab17.TabPage = ultraTabPageControl1;
            ultraTab17.Text = "Scripts";
            ultraTab16.Key = "TaskForces";
            ultraTab16.TabPage = ultraTabPageControl7;
            ultraTab16.Text = "Task Forces";
            appearance12.Image = resources.GetObject("appearance12.Image");
            ultraTab1.Appearance = appearance12;
            ultraTab1.Key = "Infantry";
            ultraTab1.TabPage = ultraTabPageControl2;
            ultraTab1.Text = "Infantry Task Forces";
            appearance13.Image = resources.GetObject("appearance13.Image");
            ultraTab11.Appearance = appearance13;
            ultraTab11.Key = "Vehicles";
            ultraTab11.TabPage = ultraTabPageControl3;
            ultraTab11.Text = "Vehicle Task Forces";
            appearance14.Image = resources.GetObject("appearance14.Image");
            ultraTab12.Appearance = appearance14;
            ultraTab12.Key = "Aircrafts";
            ultraTab12.TabPage = ultraTabPageControl4;
            ultraTab12.Text = "Aircraft Task Forces";
            ultraTab13.Key = "Teams";
            ultraTab13.TabPage = ultraTabPageControl5;
            ultraTab13.Text = "All Teams";
            mainTab.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { ultraTab17, ultraTab16, ultraTab1, ultraTab11, ultraTab12, ultraTab13 });
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
            ultraTabPageControl1.ResumeLayout(false);
            ultraTabPageControl7.ResumeLayout(false);
            ultraTabPageControl2.ResumeLayout(false);
            ultraTabPageControl3.ResumeLayout(false);
            ultraTabPageControl4.ResumeLayout(false);
            ultraTabPageControl5.ResumeLayout(false);
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
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl7;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private EntitiesListControl entitiesListTaskForces;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private UnitsListControl unitsListInfantry;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private UnitsListControl unitsListVehicles;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl4;
        private UnitsListControl unitsListAircrafts;
        private EntitiesListControl entitiesListScripts;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl5;
        private EntitiesListControl entitiesListTeams;
        public Infragistics.Win.UltraWinTabControl.UltraTabControl mainTab;
    }
}
