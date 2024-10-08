﻿namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
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
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            toolStripAdd = new ToolStrip();
            buttonAddNew = new ToolStripButton();
            panelTop = new Panel();
            panelName = new Panel();
            textName = new TextBox();
            labelKey = new Label();
            panelButtons = new Panel();
            ButtonDelete = new Button();
            ButtonCopy = new Button();
            entitiesListTeams = new EntitiesListControl();
            panelScript = new Panel();
            groupBoxTeams = new GroupBox();
            splitterUnitPicker = new Splitter();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            toolStripAdd.SuspendLayout();
            panelTop.SuspendLayout();
            panelName.SuspendLayout();
            panelButtons.SuspendLayout();
            panelScript.SuspendLayout();
            groupBoxTeams.SuspendLayout();
            SuspendLayout();
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
            valuesGrid.Size = new Size(774, 156);
            valuesGrid.TabIndex = 3;
            valuesGrid.InitializeRow += valuesGrid_InitializeRow;
            valuesGrid.ClickCell += valuesGrid_ClickCell;
            // 
            // toolStripAdd
            // 
            toolStripAdd.Items.AddRange(new ToolStripItem[] { buttonAddNew });
            toolStripAdd.Location = new Point(0, 39);
            toolStripAdd.Name = "toolStripAdd";
            toolStripAdd.Size = new Size(774, 25);
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
            panelTop.Controls.Add(panelName);
            panelTop.Controls.Add(panelButtons);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(774, 39);
            panelTop.TabIndex = 2;
            // 
            // panelName
            // 
            panelName.Controls.Add(textName);
            panelName.Controls.Add(labelKey);
            panelName.Dock = DockStyle.Fill;
            panelName.Location = new Point(0, 0);
            panelName.Name = "panelName";
            panelName.Size = new Size(594, 39);
            panelName.TabIndex = 10;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(92, 8);
            textName.Name = "textName";
            textName.Size = new Size(487, 23);
            textName.TabIndex = 1;
            textName.TextChanged += textName_TextChanged;
            // 
            // labelKey
            // 
            labelKey.AutoSize = true;
            labelKey.Location = new Point(3, 11);
            labelKey.Name = "labelKey";
            labelKey.Size = new Size(68, 15);
            labelKey.TabIndex = 0;
            labelKey.Text = "01234567-G";
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(ButtonDelete);
            panelButtons.Controls.Add(ButtonCopy);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(594, 0);
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
            // entitiesListTeams
            // 
            entitiesListTeams.Dock = DockStyle.Fill;
            entitiesListTeams.EntityType = "TeamTypes";
            entitiesListTeams.Location = new Point(3, 19);
            entitiesListTeams.Name = "entitiesListTeams";
            entitiesListTeams.Size = new Size(768, 281);
            entitiesListTeams.TabIndex = 1;
            entitiesListTeams.AddedEntity += entitiesListTeams_AddedEntity;
            // 
            // panelScript
            // 
            panelScript.Controls.Add(valuesGrid);
            panelScript.Controls.Add(toolStripAdd);
            panelScript.Controls.Add(panelTop);
            panelScript.Dock = DockStyle.Top;
            panelScript.Location = new Point(0, 0);
            panelScript.Name = "panelScript";
            panelScript.Size = new Size(774, 220);
            panelScript.TabIndex = 7;
            // 
            // groupBoxTeams
            // 
            groupBoxTeams.Controls.Add(entitiesListTeams);
            groupBoxTeams.Dock = DockStyle.Fill;
            groupBoxTeams.Location = new Point(0, 226);
            groupBoxTeams.Name = "groupBoxTeams";
            groupBoxTeams.Size = new Size(774, 303);
            groupBoxTeams.TabIndex = 8;
            groupBoxTeams.TabStop = false;
            groupBoxTeams.Text = "Teams";
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Dock = DockStyle.Top;
            splitterUnitPicker.Location = new Point(0, 220);
            splitterUnitPicker.Margin = new Padding(4, 3, 4, 3);
            splitterUnitPicker.Name = "splitterUnitPicker";
            splitterUnitPicker.Size = new Size(774, 6);
            splitterUnitPicker.TabIndex = 11;
            splitterUnitPicker.TabStop = false;
            // 
            // AiScriptEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBoxTeams);
            Controls.Add(splitterUnitPicker);
            Controls.Add(panelScript);
            Name = "AiScriptEditControl";
            Size = new Size(774, 529);
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            toolStripAdd.ResumeLayout(false);
            toolStripAdd.PerformLayout();
            panelTop.ResumeLayout(false);
            panelName.ResumeLayout(false);
            panelName.PerformLayout();
            panelButtons.ResumeLayout(false);
            panelScript.ResumeLayout(false);
            panelScript.PerformLayout();
            groupBoxTeams.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Panel panelButtons;
        private Button ButtonDelete;
        private Button ButtonCopy;
        private TextBox textName;
        private Label labelKey;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private ToolStrip toolStripAdd;
        private ToolStripButton buttonAddNew;
        private EntitiesListControl entitiesListTeams;
        private Panel panelScript;
        private GroupBox groupBoxTeams;
        private Splitter splitterUnitPicker;
        private Panel panelName;
    }
}
