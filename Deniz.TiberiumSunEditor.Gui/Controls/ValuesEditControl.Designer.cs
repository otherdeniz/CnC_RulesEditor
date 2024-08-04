namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class ValuesEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValuesEditControl));
            valuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelValueChooser = new Panel();
            groupBoxValueChooser = new GroupBox();
            lookupValue = new LookupTextControl();
            panelUseDefault = new Panel();
            ButtonUseDefault = new Button();
            panelCloseValue = new Panel();
            ButtonCloseValue = new Button();
            ((System.ComponentModel.ISupportInitialize)valuesGrid).BeginInit();
            panelValueChooser.SuspendLayout();
            groupBoxValueChooser.SuspendLayout();
            panelUseDefault.SuspendLayout();
            panelCloseValue.SuspendLayout();
            SuspendLayout();
            // 
            // valuesGrid
            // 
            ultraGridBand1.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand1.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            valuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            valuesGrid.DisplayLayout.DefaultSelectedBackColor = Color.FromArgb(192, 192, 255);
            valuesGrid.DisplayLayout.DefaultSelectedForeColor = Color.Black;
            valuesGrid.DisplayLayout.GroupByBox.Hidden = true;
            valuesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            valuesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            valuesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            valuesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            valuesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            valuesGrid.DisplayLayout.Override.SelectedAppearancesEnabled = Infragistics.Win.DefaultableBoolean.False;
            valuesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            valuesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            valuesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            valuesGrid.Dock = DockStyle.Fill;
            valuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            valuesGrid.Location = new Point(0, 0);
            valuesGrid.Margin = new Padding(4, 3, 4, 3);
            valuesGrid.Name = "valuesGrid";
            valuesGrid.Size = new Size(690, 522);
            valuesGrid.TabIndex = 1;
            valuesGrid.AfterCellUpdate += valuesGrid_AfterCellUpdate;
            valuesGrid.InitializeRow += valuesGrid_InitializeRow;
            valuesGrid.ClickCell += valuesGrid_ClickCell;
            valuesGrid.MouseDown += valuesGrid_MouseDown;
            // 
            // panelValueChooser
            // 
            panelValueChooser.Controls.Add(groupBoxValueChooser);
            panelValueChooser.Dock = DockStyle.Right;
            panelValueChooser.Location = new Point(392, 0);
            panelValueChooser.Margin = new Padding(4, 3, 4, 3);
            panelValueChooser.Name = "panelValueChooser";
            panelValueChooser.Size = new Size(298, 522);
            panelValueChooser.TabIndex = 8;
            panelValueChooser.Visible = false;
            // 
            // groupBoxValueChooser
            // 
            groupBoxValueChooser.Controls.Add(lookupValue);
            groupBoxValueChooser.Controls.Add(panelUseDefault);
            groupBoxValueChooser.Controls.Add(panelCloseValue);
            groupBoxValueChooser.Dock = DockStyle.Fill;
            groupBoxValueChooser.Location = new Point(0, 0);
            groupBoxValueChooser.Margin = new Padding(4, 3, 4, 3);
            groupBoxValueChooser.Name = "groupBoxValueChooser";
            groupBoxValueChooser.Padding = new Padding(4, 3, 4, 3);
            groupBoxValueChooser.Size = new Size(298, 522);
            groupBoxValueChooser.TabIndex = 0;
            groupBoxValueChooser.TabStop = false;
            groupBoxValueChooser.Text = "Value";
            // 
            // lookupValue
            // 
            lookupValue.Dock = DockStyle.Fill;
            lookupValue.Location = new Point(4, 19);
            lookupValue.Margin = new Padding(5, 3, 5, 3);
            lookupValue.Name = "lookupValue";
            lookupValue.Size = new Size(290, 426);
            lookupValue.TabIndex = 3;
            lookupValue.RefreshEntityValue += lookupValue_RefreshEntityValue;
            lookupValue.SelectedValueChanged += lookupValue_SelectedValueChanged;
            // 
            // panelUseDefault
            // 
            panelUseDefault.Controls.Add(ButtonUseDefault);
            panelUseDefault.Dock = DockStyle.Bottom;
            panelUseDefault.Location = new Point(4, 445);
            panelUseDefault.Margin = new Padding(4, 3, 4, 3);
            panelUseDefault.Name = "panelUseDefault";
            panelUseDefault.Padding = new Padding(5);
            panelUseDefault.Size = new Size(290, 37);
            panelUseDefault.TabIndex = 4;
            // 
            // ButtonUseDefault
            // 
            ButtonUseDefault.Dock = DockStyle.Fill;
            ButtonUseDefault.Image = (Image)resources.GetObject("ButtonUseDefault.Image");
            ButtonUseDefault.Location = new Point(5, 5);
            ButtonUseDefault.Margin = new Padding(4, 3, 4, 3);
            ButtonUseDefault.Name = "ButtonUseDefault";
            ButtonUseDefault.Size = new Size(280, 27);
            ButtonUseDefault.TabIndex = 0;
            ButtonUseDefault.Text = "Use Default";
            ButtonUseDefault.TextAlign = ContentAlignment.MiddleRight;
            ButtonUseDefault.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonUseDefault.UseVisualStyleBackColor = true;
            ButtonUseDefault.Click += ButtonUseDefault_Click;
            // 
            // panelCloseValue
            // 
            panelCloseValue.Controls.Add(ButtonCloseValue);
            panelCloseValue.Dock = DockStyle.Bottom;
            panelCloseValue.Location = new Point(4, 482);
            panelCloseValue.Margin = new Padding(4, 3, 4, 3);
            panelCloseValue.Name = "panelCloseValue";
            panelCloseValue.Padding = new Padding(5);
            panelCloseValue.Size = new Size(290, 37);
            panelCloseValue.TabIndex = 2;
            // 
            // ButtonCloseValue
            // 
            ButtonCloseValue.Dock = DockStyle.Fill;
            ButtonCloseValue.Image = (Image)resources.GetObject("ButtonCloseValue.Image");
            ButtonCloseValue.Location = new Point(5, 5);
            ButtonCloseValue.Margin = new Padding(4, 3, 4, 3);
            ButtonCloseValue.Name = "ButtonCloseValue";
            ButtonCloseValue.Size = new Size(280, 27);
            ButtonCloseValue.TabIndex = 0;
            ButtonCloseValue.Text = "Close";
            ButtonCloseValue.TextAlign = ContentAlignment.MiddleRight;
            ButtonCloseValue.TextImageRelation = TextImageRelation.ImageBeforeText;
            ButtonCloseValue.UseVisualStyleBackColor = true;
            ButtonCloseValue.Click += ButtonCloseValue_Click;
            // 
            // ValuesEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelValueChooser);
            Controls.Add(valuesGrid);
            Name = "ValuesEditControl";
            Size = new Size(690, 522);
            ((System.ComponentModel.ISupportInitialize)valuesGrid).EndInit();
            panelValueChooser.ResumeLayout(false);
            groupBoxValueChooser.ResumeLayout(false);
            panelUseDefault.ResumeLayout(false);
            panelCloseValue.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid valuesGrid;
        private Panel panelValueChooser;
        private GroupBox groupBoxValueChooser;
        private LookupTextControl lookupValue;
        private Panel panelCloseValue;
        private Button ButtonCloseValue;
        private Panel panelUseDefault;
        private Button ButtonUseDefault;
    }
}
