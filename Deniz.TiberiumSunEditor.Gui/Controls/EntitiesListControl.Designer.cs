namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class EntitiesListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntitiesListControl));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            panelLeft = new Panel();
            toolStripAdd = new ToolStrip();
            buttonAddEmpty = new ToolStripButton();
            entitiesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            splitterUnitPicker = new Splitter();
            panelLeft.SuspendLayout();
            toolStripAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)entitiesGrid).BeginInit();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(entitiesGrid);
            panelLeft.Controls.Add(toolStripAdd);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(299, 558);
            panelLeft.TabIndex = 0;
            // 
            // toolStripAdd
            // 
            toolStripAdd.Items.AddRange(new ToolStripItem[] { buttonAddEmpty });
            toolStripAdd.Location = new Point(0, 0);
            toolStripAdd.Name = "toolStripAdd";
            toolStripAdd.Size = new Size(299, 25);
            toolStripAdd.TabIndex = 4;
            toolStripAdd.Text = "toolStrip1";
            // 
            // buttonAddEmpty
            // 
            buttonAddEmpty.Image = (Image)resources.GetObject("buttonAddEmpty.Image");
            buttonAddEmpty.ImageTransparentColor = Color.Magenta;
            buttonAddEmpty.Name = "buttonAddEmpty";
            buttonAddEmpty.Size = new Size(49, 22);
            buttonAddEmpty.Text = "Add";
            // 
            // entitiesGrid
            // 
            entitiesGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand2.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand2.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            entitiesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            entitiesGrid.DisplayLayout.GroupByBox.Hidden = true;
            entitiesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            entitiesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            entitiesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            entitiesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            entitiesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            entitiesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            entitiesGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            entitiesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            entitiesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            entitiesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            entitiesGrid.Dock = DockStyle.Fill;
            entitiesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            entitiesGrid.Location = new Point(0, 25);
            entitiesGrid.Margin = new Padding(4, 3, 4, 3);
            entitiesGrid.Name = "entitiesGrid";
            entitiesGrid.Size = new Size(299, 533);
            entitiesGrid.TabIndex = 8;
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Location = new Point(299, 0);
            splitterUnitPicker.Margin = new Padding(4, 3, 4, 3);
            splitterUnitPicker.Name = "splitterUnitPicker";
            splitterUnitPicker.Size = new Size(6, 558);
            splitterUnitPicker.TabIndex = 3;
            splitterUnitPicker.TabStop = false;
            // 
            // AiEntitiesListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitterUnitPicker);
            Controls.Add(panelLeft);
            Name = "AiEntitiesListControl";
            Size = new Size(845, 558);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            toolStripAdd.ResumeLayout(false);
            toolStripAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)entitiesGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeft;
        private ToolStrip toolStripAdd;
        private ToolStripButton buttonAddEmpty;
        private Infragistics.Win.UltraWinGrid.UltraGrid entitiesGrid;
        private Splitter splitterUnitPicker;
    }
}
