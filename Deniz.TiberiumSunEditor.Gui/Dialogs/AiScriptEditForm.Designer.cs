namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class AiScriptEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiScriptEditForm));
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            panelBottom = new Panel();
            buttonCancel = new Button();
            ultraComboEditor1 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            smallEntityControl1 = new Controls.SmallEntityControl();
            label3 = new Label();
            label2 = new Label();
            groupBoxParam = new GroupBox();
            panelParamBuilding = new Panel();
            paramValuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            label1 = new Label();
            groupBoxAction = new GroupBox();
            actionsGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelParamNumber = new Panel();
            label4 = new Label();
            numericParamValue = new NumericUpDown();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboEditor1).BeginInit();
            groupBoxParam.SuspendLayout();
            panelParamBuilding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)paramValuesGrid).BeginInit();
            groupBoxAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)actionsGrid).BeginInit();
            panelParamNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericParamValue).BeginInit();
            SuspendLayout();
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(4, 614);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1176, 43);
            panelBottom.TabIndex = 2;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.Image = (Image)resources.GetObject("buttonCancel.Image");
            buttonCancel.Location = new Point(1075, 9);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.MiddleRight;
            buttonCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // ultraComboEditor1
            // 
            ultraComboEditor1.Location = new Point(146, 51);
            ultraComboEditor1.Name = "ultraComboEditor1";
            ultraComboEditor1.Size = new Size(189, 25);
            ultraComboEditor1.TabIndex = 2;
            // 
            // smallEntityControl1
            // 
            smallEntityControl1.BorderStyle = BorderStyle.FixedSingle;
            smallEntityControl1.Cursor = Cursors.Hand;
            smallEntityControl1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            smallEntityControl1.Location = new Point(146, 10);
            smallEntityControl1.Name = "smallEntityControl1";
            smallEntityControl1.Size = new Size(189, 35);
            smallEntityControl1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 55);
            label3.Name = "label3";
            label3.Size = new Size(118, 15);
            label3.TabIndex = 0;
            label3.Text = "Target type selection:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 12);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 0;
            label2.Text = "Building:";
            // 
            // groupBoxParam
            // 
            groupBoxParam.Controls.Add(panelParamNumber);
            groupBoxParam.Controls.Add(panelParamBuilding);
            groupBoxParam.Controls.Add(paramValuesGrid);
            groupBoxParam.Dock = DockStyle.Bottom;
            groupBoxParam.Location = new Point(4, 465);
            groupBoxParam.Name = "groupBoxParam";
            groupBoxParam.Size = new Size(1176, 149);
            groupBoxParam.TabIndex = 0;
            groupBoxParam.TabStop = false;
            groupBoxParam.Text = "Parameter";
            // 
            // panelParamBuilding
            // 
            panelParamBuilding.Controls.Add(ultraComboEditor1);
            panelParamBuilding.Controls.Add(smallEntityControl1);
            panelParamBuilding.Controls.Add(label2);
            panelParamBuilding.Controls.Add(label3);
            panelParamBuilding.Location = new Point(560, 22);
            panelParamBuilding.Name = "panelParamBuilding";
            panelParamBuilding.Size = new Size(359, 94);
            panelParamBuilding.TabIndex = 9;
            panelParamBuilding.Visible = false;
            // 
            // paramValuesGrid
            // 
            paramValuesGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand1.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand1.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            paramValuesGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            paramValuesGrid.DisplayLayout.GroupByBox.Hidden = true;
            paramValuesGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            paramValuesGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            paramValuesGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            paramValuesGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance12.TextHAlignAsString = "Left";
            paramValuesGrid.DisplayLayout.Override.HeaderAppearance = appearance12;
            paramValuesGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            paramValuesGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            paramValuesGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            paramValuesGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            paramValuesGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            paramValuesGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            paramValuesGrid.Location = new Point(274, 22);
            paramValuesGrid.Margin = new Padding(4, 3, 4, 3);
            paramValuesGrid.Name = "paramValuesGrid";
            paramValuesGrid.Size = new Size(242, 94);
            paramValuesGrid.TabIndex = 8;
            paramValuesGrid.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 12);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 0;
            label1.Text = "Number:";
            // 
            // groupBoxAction
            // 
            groupBoxAction.Controls.Add(actionsGrid);
            groupBoxAction.Dock = DockStyle.Fill;
            groupBoxAction.Location = new Point(4, 4);
            groupBoxAction.Name = "groupBoxAction";
            groupBoxAction.Size = new Size(1176, 461);
            groupBoxAction.TabIndex = 5;
            groupBoxAction.TabStop = false;
            groupBoxAction.Text = "Action";
            // 
            // actionsGrid
            // 
            actionsGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            ultraGridBand2.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand2.Override.AllowGroupCollapsing = Infragistics.Win.DefaultableBoolean.True;
            actionsGrid.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            actionsGrid.DisplayLayout.GroupByBox.Hidden = true;
            actionsGrid.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            actionsGrid.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            actionsGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            actionsGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance1.TextHAlignAsString = "Left";
            actionsGrid.DisplayLayout.Override.HeaderAppearance = appearance1;
            actionsGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            actionsGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            actionsGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            actionsGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            actionsGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            actionsGrid.Dock = DockStyle.Fill;
            actionsGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            actionsGrid.Location = new Point(3, 19);
            actionsGrid.Margin = new Padding(4, 3, 4, 3);
            actionsGrid.Name = "actionsGrid";
            actionsGrid.Size = new Size(1170, 439);
            actionsGrid.TabIndex = 8;
            // 
            // panelParamNumber
            // 
            panelParamNumber.Controls.Add(numericParamValue);
            panelParamNumber.Controls.Add(label4);
            panelParamNumber.Location = new Point(6, 22);
            panelParamNumber.Name = "panelParamNumber";
            panelParamNumber.Size = new Size(224, 97);
            panelParamNumber.TabIndex = 10;
            panelParamNumber.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 12);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 0;
            label4.Text = "Number:";
            // 
            // numericParamValue
            // 
            numericParamValue.Location = new Point(85, 10);
            numericParamValue.Name = "numericParamValue";
            numericParamValue.Size = new Size(106, 23);
            numericParamValue.TabIndex = 1;
            // 
            // AiScriptEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(1184, 661);
            Controls.Add(groupBoxAction);
            Controls.Add(groupBoxParam);
            Controls.Add(panelBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AiScriptEditForm";
            Padding = new Padding(4);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Script Item";
            Load += AiScriptEditForm_Load;
            panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ultraComboEditor1).EndInit();
            groupBoxParam.ResumeLayout(false);
            panelParamBuilding.ResumeLayout(false);
            panelParamBuilding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)paramValuesGrid).EndInit();
            groupBoxAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)actionsGrid).EndInit();
            panelParamNumber.ResumeLayout(false);
            panelParamNumber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericParamValue).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBottom;
        private Button buttonCancel;
        private GroupBox groupBoxParam;
        private GroupBox groupBoxAction;
        private Label label1;
        private Infragistics.Win.UltraWinGrid.UltraGrid paramValuesGrid;
        private Infragistics.Win.UltraWinGrid.UltraGrid actionsGrid;
        private Controls.SmallEntityControl smallEntityControl1;
        private Label label3;
        private Label label2;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboEditor1;
        private Panel panelParamBuilding;
        private Panel panelParamNumber;
        private NumericUpDown numericParamValue;
        private Label label4;
    }
}