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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiScriptEditForm));
            ultraComboBuildingTarget = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            smallEntityBuilding = new Controls.SmallEntityControl();
            label3 = new Label();
            label2 = new Label();
            groupBoxParam = new GroupBox();
            panelCustomAction = new Panel();
            numericCustomParameter = new NumericUpDown();
            label6 = new Label();
            numericCustomAction = new NumericUpDown();
            label5 = new Label();
            panelParamNumber = new Panel();
            numericParamValue = new NumericUpDown();
            label4 = new Label();
            panelParamBuilding = new Panel();
            paramValuesGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            label1 = new Label();
            groupBoxAction = new GroupBox();
            actionsGrid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            panelBottom = new Panel();
            panelOkCancel = new Panel();
            buttonOk = new Button();
            buttonCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)ultraComboBuildingTarget).BeginInit();
            groupBoxParam.SuspendLayout();
            panelCustomAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericCustomParameter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericCustomAction).BeginInit();
            panelParamNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericParamValue).BeginInit();
            panelParamBuilding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)paramValuesGrid).BeginInit();
            groupBoxAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)actionsGrid).BeginInit();
            panelBottom.SuspendLayout();
            panelOkCancel.SuspendLayout();
            SuspendLayout();
            // 
            // ultraComboBuildingTarget
            // 
            ultraComboBuildingTarget.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            ultraComboBuildingTarget.Location = new Point(146, 51);
            ultraComboBuildingTarget.Name = "ultraComboBuildingTarget";
            ultraComboBuildingTarget.Size = new Size(189, 25);
            ultraComboBuildingTarget.TabIndex = 2;
            ultraComboBuildingTarget.ValueChanged += ultraComboBuildingTarget_ValueChanged;
            // 
            // smallEntityBuilding
            // 
            smallEntityBuilding.BorderStyle = BorderStyle.FixedSingle;
            smallEntityBuilding.Cursor = Cursors.Hand;
            smallEntityBuilding.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            smallEntityBuilding.Location = new Point(146, 10);
            smallEntityBuilding.Name = "smallEntityBuilding";
            smallEntityBuilding.Size = new Size(189, 35);
            smallEntityBuilding.TabIndex = 1;
            smallEntityBuilding.Click += smallEntityBuilding_Click;
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
            groupBoxParam.Controls.Add(panelCustomAction);
            groupBoxParam.Controls.Add(panelParamNumber);
            groupBoxParam.Controls.Add(panelParamBuilding);
            groupBoxParam.Controls.Add(paramValuesGrid);
            groupBoxParam.Dock = DockStyle.Bottom;
            groupBoxParam.Location = new Point(4, 433);
            groupBoxParam.Name = "groupBoxParam";
            groupBoxParam.Size = new Size(1176, 189);
            groupBoxParam.TabIndex = 1;
            groupBoxParam.TabStop = false;
            groupBoxParam.Text = "Parameter";
            // 
            // panelCustomAction
            // 
            panelCustomAction.Controls.Add(numericCustomParameter);
            panelCustomAction.Controls.Add(label6);
            panelCustomAction.Controls.Add(numericCustomAction);
            panelCustomAction.Controls.Add(label5);
            panelCustomAction.Location = new Point(930, 22);
            panelCustomAction.Name = "panelCustomAction";
            panelCustomAction.Size = new Size(224, 97);
            panelCustomAction.TabIndex = 10;
            panelCustomAction.Visible = false;
            // 
            // numericCustomParameter
            // 
            numericCustomParameter.Location = new Point(97, 39);
            numericCustomParameter.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericCustomParameter.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            numericCustomParameter.Name = "numericCustomParameter";
            numericCustomParameter.Size = new Size(109, 23);
            numericCustomParameter.TabIndex = 1;
            numericCustomParameter.ValueChanged += numericCustomParameter_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 41);
            label6.Name = "label6";
            label6.Size = new Size(64, 15);
            label6.TabIndex = 0;
            label6.Text = "Parameter:";
            // 
            // numericCustomAction
            // 
            numericCustomAction.Location = new Point(97, 10);
            numericCustomAction.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericCustomAction.Name = "numericCustomAction";
            numericCustomAction.Size = new Size(109, 23);
            numericCustomAction.TabIndex = 0;
            numericCustomAction.ValueChanged += numericCustomAction_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 12);
            label5.Name = "label5";
            label5.Size = new Size(45, 15);
            label5.TabIndex = 0;
            label5.Text = "Action:";
            // 
            // panelParamNumber
            // 
            panelParamNumber.Controls.Add(numericParamValue);
            panelParamNumber.Controls.Add(label4);
            panelParamNumber.Location = new Point(19, 22);
            panelParamNumber.Name = "panelParamNumber";
            panelParamNumber.Size = new Size(224, 97);
            panelParamNumber.TabIndex = 10;
            panelParamNumber.Visible = false;
            // 
            // numericParamValue
            // 
            numericParamValue.Location = new Point(85, 10);
            numericParamValue.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numericParamValue.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            numericParamValue.Name = "numericParamValue";
            numericParamValue.Size = new Size(106, 23);
            numericParamValue.TabIndex = 1;
            numericParamValue.ValueChanged += numericParamValue_ValueChanged;
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
            // panelParamBuilding
            // 
            panelParamBuilding.Controls.Add(ultraComboBuildingTarget);
            panelParamBuilding.Controls.Add(smallEntityBuilding);
            panelParamBuilding.Controls.Add(label2);
            panelParamBuilding.Controls.Add(label3);
            panelParamBuilding.Location = new Point(538, 22);
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
            paramValuesGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
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
            paramValuesGrid.Location = new Point(266, 22);
            paramValuesGrid.Margin = new Padding(4, 3, 4, 3);
            paramValuesGrid.Name = "paramValuesGrid";
            paramValuesGrid.Size = new Size(242, 94);
            paramValuesGrid.TabIndex = 8;
            paramValuesGrid.Visible = false;
            paramValuesGrid.AfterSelectChange += paramValuesGrid_AfterSelectChange;
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
            groupBoxAction.Size = new Size(1176, 429);
            groupBoxAction.TabIndex = 0;
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
            actionsGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            actionsGrid.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            actionsGrid.DisplayLayout.Override.GroupByRowInitialExpansionState = Infragistics.Win.UltraWinGrid.GroupByRowInitialExpansionState.Expanded;
            appearance1.TextHAlignAsString = "Left";
            actionsGrid.DisplayLayout.Override.HeaderAppearance = appearance1;
            actionsGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
            actionsGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            actionsGrid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFree;
            actionsGrid.DisplayLayout.ShowDeleteRowsPrompt = false;
            actionsGrid.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            actionsGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            actionsGrid.Dock = DockStyle.Fill;
            actionsGrid.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            actionsGrid.Location = new Point(3, 19);
            actionsGrid.Margin = new Padding(4, 3, 4, 3);
            actionsGrid.Name = "actionsGrid";
            actionsGrid.Size = new Size(1170, 407);
            actionsGrid.TabIndex = 0;
            actionsGrid.AfterSelectChange += actionsGrid_AfterSelectChange;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(panelOkCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(4, 622);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1176, 35);
            panelBottom.TabIndex = 11;
            // 
            // panelOkCancel
            // 
            panelOkCancel.Controls.Add(buttonOk);
            panelOkCancel.Controls.Add(buttonCancel);
            panelOkCancel.Dock = DockStyle.Right;
            panelOkCancel.Location = new Point(968, 0);
            panelOkCancel.Name = "panelOkCancel";
            panelOkCancel.Size = new Size(208, 35);
            panelOkCancel.TabIndex = 2;
            // 
            // buttonOk
            // 
            buttonOk.Enabled = false;
            buttonOk.Image = (Image)resources.GetObject("buttonOk.Image");
            buttonOk.Location = new Point(19, 5);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(90, 26);
            buttonOk.TabIndex = 0;
            buttonOk.Text = "Ok";
            buttonOk.TextAlign = ContentAlignment.MiddleRight;
            buttonOk.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Image = (Image)resources.GetObject("buttonCancel.Image");
            buttonCancel.Location = new Point(115, 5);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.MiddleRight;
            buttonCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // AiScriptEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
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
            ((System.ComponentModel.ISupportInitialize)ultraComboBuildingTarget).EndInit();
            groupBoxParam.ResumeLayout(false);
            panelCustomAction.ResumeLayout(false);
            panelCustomAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericCustomParameter).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericCustomAction).EndInit();
            panelParamNumber.ResumeLayout(false);
            panelParamNumber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericParamValue).EndInit();
            panelParamBuilding.ResumeLayout(false);
            panelParamBuilding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)paramValuesGrid).EndInit();
            groupBoxAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)actionsGrid).EndInit();
            panelBottom.ResumeLayout(false);
            panelOkCancel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBoxParam;
        private GroupBox groupBoxAction;
        private Label label1;
        private Infragistics.Win.UltraWinGrid.UltraGrid paramValuesGrid;
        private Infragistics.Win.UltraWinGrid.UltraGrid actionsGrid;
        private Controls.SmallEntityControl smallEntityBuilding;
        private Label label3;
        private Label label2;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboBuildingTarget;
        private Panel panelParamBuilding;
        private Panel panelParamNumber;
        private NumericUpDown numericParamValue;
        private Label label4;
        private Panel panelBottom;
        private Panel panelOkCancel;
        private Button buttonOk;
        private Button buttonCancel;
        private Panel panelCustomAction;
        private NumericUpDown numericCustomParameter;
        private Label label6;
        private NumericUpDown numericCustomAction;
        private Label label5;
    }
}