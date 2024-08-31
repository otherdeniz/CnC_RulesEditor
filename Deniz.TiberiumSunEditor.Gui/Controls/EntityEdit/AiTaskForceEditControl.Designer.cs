namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    partial class AiTaskForceEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiTaskForceEditControl));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            panelTop = new Panel();
            buttonCreateName = new Button();
            panelButtons = new Panel();
            ButtonDelete = new Button();
            ButtonCopy = new Button();
            textName = new TextBox();
            labelKey = new Label();
            groupBoxTeams = new GroupBox();
            entitiesListTeams = new EntitiesListControl();
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            splitterUnitPicker = new Splitter();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            groupBoxTeams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(buttonCreateName);
            panelTop.Controls.Add(panelButtons);
            panelTop.Controls.Add(textName);
            panelTop.Controls.Add(labelKey);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(640, 39);
            panelTop.TabIndex = 0;
            // 
            // buttonCreateName
            // 
            buttonCreateName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCreateName.Image = (Image)resources.GetObject("buttonCreateName.Image");
            buttonCreateName.Location = new Point(421, 5);
            buttonCreateName.Name = "buttonCreateName";
            buttonCreateName.Size = new Size(27, 27);
            buttonCreateName.TabIndex = 9;
            buttonCreateName.TextAlign = ContentAlignment.MiddleRight;
            buttonCreateName.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCreateName.UseVisualStyleBackColor = true;
            buttonCreateName.Click += buttonCreateName_Click;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(ButtonDelete);
            panelButtons.Controls.Add(ButtonCopy);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(460, 0);
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
            ButtonCopy.Click += ButtonCopy_Click;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(92, 7);
            textName.Name = "textName";
            textName.Size = new Size(329, 23);
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
            // groupBoxTeams
            // 
            groupBoxTeams.Controls.Add(entitiesListTeams);
            groupBoxTeams.Dock = DockStyle.Fill;
            groupBoxTeams.Location = new Point(0, 275);
            groupBoxTeams.Name = "groupBoxTeams";
            groupBoxTeams.Size = new Size(640, 238);
            groupBoxTeams.TabIndex = 2;
            groupBoxTeams.TabStop = false;
            groupBoxTeams.Text = "Teams";
            // 
            // entitiesListTeams
            // 
            entitiesListTeams.Dock = DockStyle.Fill;
            entitiesListTeams.Location = new Point(3, 19);
            entitiesListTeams.Name = "entitiesListTeams";
            entitiesListTeams.Size = new Size(634, 216);
            entitiesListTeams.TabIndex = 0;
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
            valuesGrid.Dock = DockStyle.Top;
            valuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            valuesGrid.Location = new Point(0, 39);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(640, 230);
            valuesGrid.TabIndex = 9;
            valuesGrid.InitializeRow += valuesGrid_InitializeRow;
            valuesGrid.ClickCell += valuesGrid_ClickCell;
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Dock = DockStyle.Top;
            splitterUnitPicker.Location = new Point(0, 269);
            splitterUnitPicker.Margin = new Padding(4, 3, 4, 3);
            splitterUnitPicker.Name = "splitterUnitPicker";
            splitterUnitPicker.Size = new Size(640, 6);
            splitterUnitPicker.TabIndex = 10;
            splitterUnitPicker.TabStop = false;
            // 
            // AiTaskForceEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBoxTeams);
            Controls.Add(splitterUnitPicker);
            Controls.Add(valuesGrid);
            Controls.Add(panelTop);
            Name = "AiTaskForceEditControl";
            Size = new Size(640, 513);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelButtons.ResumeLayout(false);
            groupBoxTeams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label labelKey;
        private TextBox textName;
        private Panel panelButtons;
        private Button ButtonDelete;
        private Button ButtonCopy;
        private GroupBox groupBoxTeams;
        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private Button buttonCreateName;
        private EntitiesListControl entitiesListTeams;
        private Splitter splitterUnitPicker;
    }
}
