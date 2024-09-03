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
            comboGroup = new ComboBox();
            label1 = new Label();
            buttonRefreshName = new Button();
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
            panelTop.Controls.Add(comboGroup);
            panelTop.Controls.Add(label1);
            panelTop.Controls.Add(buttonRefreshName);
            panelTop.Controls.Add(panelButtons);
            panelTop.Controls.Add(textName);
            panelTop.Controls.Add(labelKey);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(640, 39);
            panelTop.TabIndex = 0;
            // 
            // comboGroup
            // 
            comboGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboGroup.FormattingEnabled = true;
            comboGroup.Items.AddRange(new object[] { "-1" });
            comboGroup.Location = new Point(375, 7);
            comboGroup.Name = "comboGroup";
            comboGroup.Size = new Size(61, 23);
            comboGroup.TabIndex = 2;
            comboGroup.TextChanged += comboGroup_TextChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(326, 10);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 10;
            label1.Text = "Group:";
            // 
            // buttonRefreshName
            // 
            buttonRefreshName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRefreshName.Image = (Image)resources.GetObject("buttonRefreshName.Image");
            buttonRefreshName.Location = new Point(286, 5);
            buttonRefreshName.Name = "buttonRefreshName";
            buttonRefreshName.Size = new Size(27, 27);
            buttonRefreshName.TabIndex = 1;
            buttonRefreshName.TextAlign = ContentAlignment.MiddleRight;
            buttonRefreshName.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRefreshName.UseVisualStyleBackColor = true;
            buttonRefreshName.Click += buttonRefreshName_Click;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(ButtonDelete);
            panelButtons.Controls.Add(ButtonCopy);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(460, 0);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(180, 39);
            panelButtons.TabIndex = 3;
            // 
            // ButtonDelete
            // 
            ButtonDelete.Image = (Image)resources.GetObject("ButtonDelete.Image");
            ButtonDelete.Location = new Point(92, 5);
            ButtonDelete.Name = "ButtonDelete";
            ButtonDelete.Size = new Size(76, 27);
            ButtonDelete.TabIndex = 1;
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
            ButtonCopy.TabIndex = 0;
            ButtonCopy.Text = "Copy";
            ButtonCopy.TextAlign = ContentAlignment.MiddleRight;
            ButtonCopy.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonCopy.UseVisualStyleBackColor = true;
            ButtonCopy.Visible = false;
            ButtonCopy.Click += ButtonCopy_Click;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(92, 7);
            textName.Name = "textName";
            textName.Size = new Size(194, 23);
            textName.TabIndex = 0;
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
            groupBoxTeams.Location = new Point(0, 265);
            groupBoxTeams.Name = "groupBoxTeams";
            groupBoxTeams.Size = new Size(640, 248);
            groupBoxTeams.TabIndex = 2;
            groupBoxTeams.TabStop = false;
            groupBoxTeams.Text = "Teams";
            // 
            // entitiesListTeams
            // 
            entitiesListTeams.Dock = DockStyle.Fill;
            entitiesListTeams.EntityType = "TeamTypes";
            entitiesListTeams.Location = new Point(3, 19);
            entitiesListTeams.Name = "entitiesListTeams";
            entitiesListTeams.Size = new Size(634, 226);
            entitiesListTeams.TabIndex = 0;
            entitiesListTeams.AddedEntity += entitiesListTeams_AddedEntity;
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
            valuesGrid.Size = new Size(640, 220);
            valuesGrid.TabIndex = 1;
            valuesGrid.InitializeRow += valuesGrid_InitializeRow;
            valuesGrid.ClickCell += valuesGrid_ClickCell;
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Dock = DockStyle.Top;
            splitterUnitPicker.Location = new Point(0, 259);
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
        private Button buttonRefreshName;
        private EntitiesListControl entitiesListTeams;
        private Splitter splitterUnitPicker;
        private ComboBox comboGroup;
        private Label label1;
    }
}
