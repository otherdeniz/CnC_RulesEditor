namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    partial class AiScriptEditControl
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiScriptEditControl));
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            toolStripAdd = new ToolStrip();
            buttonAddNew = new ToolStripButton();
            panelTop = new Panel();
            buttonRefreshName = new Button();
            panelButtons = new Panel();
            ButtonDelete = new Button();
            ButtonCopy = new Button();
            textName = new TextBox();
            labelKey = new Label();
            ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            entitiesListTeams = new EntitiesListControl();
            ultraTabScript = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            toolStripAdd.SuspendLayout();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraTabScript).BeginInit();
            ultraTabScript.SuspendLayout();
            SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            ultraTabPageControl1.Controls.Add(valuesGrid);
            ultraTabPageControl1.Controls.Add(toolStripAdd);
            ultraTabPageControl1.Controls.Add(panelTop);
            ultraTabPageControl1.Location = new Point(-10000, -10000);
            ultraTabPageControl1.Name = "ultraTabPageControl1";
            ultraTabPageControl1.Size = new Size(770, 501);
            // 
            // valuesGrid
            // 
            ultraGridBand1.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand1.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            valuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            valuesGrid.DisplayLayout.GroupByBox.Hidden = true;
            valuesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            valuesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            valuesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            valuesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            valuesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            valuesGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed;
            valuesGrid.DisplayLayout.Override.RowSizingArea = Infragistics.Win.UltraWinGrid.RowSizingArea.EntireRow;
            valuesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            valuesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            valuesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            valuesGrid.Dock = DockStyle.Fill;
            valuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            valuesGrid.Location = new Point(0, 64);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(770, 437);
            valuesGrid.TabIndex = 3;
            valuesGrid.InitializeRow += valuesGrid_InitializeRow;
            valuesGrid.ClickCell += valuesGrid_ClickCell;
            // 
            // toolStripAdd
            // 
            toolStripAdd.Items.AddRange(new ToolStripItem[] { buttonAddNew });
            toolStripAdd.Location = new Point(0, 39);
            toolStripAdd.Name = "toolStripAdd";
            toolStripAdd.Size = new Size(770, 25);
            toolStripAdd.TabIndex = 5;
            toolStripAdd.Text = "toolStrip1";
            // 
            // buttonAddNew
            // 
            buttonAddNew.Image = (Image)resources.GetObject("buttonAddNew.Image");
            buttonAddNew.ImageTransparentColor = Color.Magenta;
            buttonAddNew.Name = "buttonAddNew";
            buttonAddNew.Size = new Size(165, 22);
            buttonAddNew.Text = "Append new Script Action";
            buttonAddNew.Click += buttonAddNew_Click;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(buttonRefreshName);
            panelTop.Controls.Add(panelButtons);
            panelTop.Controls.Add(textName);
            panelTop.Controls.Add(labelKey);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(770, 39);
            panelTop.TabIndex = 2;
            // 
            // buttonRefreshName
            // 
            buttonRefreshName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRefreshName.Image = (Image)resources.GetObject("buttonRefreshName.Image");
            buttonRefreshName.Location = new Point(551, 5);
            buttonRefreshName.Name = "buttonRefreshName";
            buttonRefreshName.Size = new Size(27, 27);
            buttonRefreshName.TabIndex = 9;
            buttonRefreshName.TextAlign = ContentAlignment.MiddleRight;
            buttonRefreshName.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRefreshName.UseVisualStyleBackColor = true;
            buttonRefreshName.Visible = false;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(ButtonDelete);
            panelButtons.Controls.Add(ButtonCopy);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(590, 0);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(180, 39);
            panelButtons.TabIndex = 8;
            // 
            // ButtonDelete
            // 
            ButtonDelete.Image = (Image)resources.GetObject("ButtonDelete.Image");
            ButtonDelete.Location = new Point(92, 5);
            ButtonDelete.Name = "ButtonDelete";
            ButtonDelete.Size = new Size(76, 27);
            ButtonDelete.TabIndex = 6;
            ButtonDelete.Text = "Delete";
            ButtonDelete.TextAlign = ContentAlignment.MiddleRight;
            ButtonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonDelete.UseVisualStyleBackColor = true;
            ButtonDelete.Click += ButtonDelete_Click;
            // 
            // ButtonCopy
            // 
            ButtonCopy.Image = (Image)resources.GetObject("ButtonCopy.Image");
            ButtonCopy.Location = new Point(16, 5);
            ButtonCopy.Name = "ButtonCopy";
            ButtonCopy.Size = new Size(76, 27);
            ButtonCopy.TabIndex = 7;
            ButtonCopy.Text = "Copy";
            ButtonCopy.TextAlign = ContentAlignment.MiddleRight;
            ButtonCopy.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonCopy.UseVisualStyleBackColor = true;
            ButtonCopy.Visible = false;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(92, 7);
            textName.Name = "textName";
            textName.Size = new Size(459, 23);
            textName.TabIndex = 1;
            textName.TextChanged += textName_TextChanged;
            // 
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.Location = new Point(3, 10);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(68, 15);
            labelKey.TabIndex = 0;
            labelKey.Text = "01234567-G";
            // 
            // ultraTabPageControl2
            // 
            ultraTabPageControl2.Controls.Add(entitiesListTeams);
            ultraTabPageControl2.Location = new Point(1, 25);
            ultraTabPageControl2.Name = "ultraTabPageControl2";
            ultraTabPageControl2.Size = new Size(770, 501);
            // 
            // entitiesListTeams
            // 
            entitiesListTeams.Dock = DockStyle.Fill;
            entitiesListTeams.EntityType = "TeamTypes";
            entitiesListTeams.Location = new Point(0, 0);
            entitiesListTeams.Name = "entitiesListTeams";
            entitiesListTeams.Size = new Size(770, 501);
            entitiesListTeams.TabIndex = 1;
            entitiesListTeams.AddedEntity += entitiesListTeams_AddedEntity;
            // 
            // ultraTabScript
            // 
            ultraTabScript.Controls.Add(ultraTabSharedControlsPage1);
            ultraTabScript.Controls.Add(ultraTabPageControl1);
            ultraTabScript.Controls.Add(ultraTabPageControl2);
            ultraTabScript.Dock = DockStyle.Fill;
            ultraTabScript.Location = new Point(0, 0);
            ultraTabScript.Name = "ultraTabScript";
            appearance11.FontData.BoldAsString = "True";
            ultraTabScript.SelectedTabAppearance = appearance11;
            ultraTabScript.SharedControlsPage = ultraTabSharedControlsPage1;
            ultraTabScript.Size = new Size(774, 529);
            ultraTabScript.TabIndex = 6;
            ultraTab11.Key = "Script";
            ultraTab11.TabPage = ultraTabPageControl1;
            ultraTab11.Text = "Script";
            ultraTab12.Key = "Teams";
            ultraTab12.TabPage = ultraTabPageControl2;
            ultraTab12.Text = "Teams";
            ultraTabScript.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { ultraTab11, ultraTab12 });
            // 
            // ultraTabSharedControlsPage1
            // 
            ultraTabSharedControlsPage1.Location = new Point(-10000, -10000);
            ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            ultraTabSharedControlsPage1.Size = new Size(770, 501);
            // 
            // AiScriptEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ultraTabScript);
            Name = "AiScriptEditControl";
            Size = new Size(774, 529);
            ultraTabPageControl1.ResumeLayout(false);
            ultraTabPageControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            toolStripAdd.ResumeLayout(false);
            toolStripAdd.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelButtons.ResumeLayout(false);
            ultraTabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ultraTabScript).EndInit();
            ultraTabScript.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button buttonRefreshName;
        private Panel panelButtons;
        private Button ButtonDelete;
        private Button ButtonCopy;
        private TextBox textName;
        private Label labelKey;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private ToolStrip toolStripAdd;
        private ToolStripButton buttonAddNew;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabScript;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private EntitiesListControl entitiesListTeams;
    }
}
