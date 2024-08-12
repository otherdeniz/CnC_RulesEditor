namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class BalancingToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BalancingToolForm));
            Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem12 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem13 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem14 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            panelBottom = new Panel();
            buttonClose = new Button();
            splitContainerMain = new SplitContainer();
            sideBalanceLeft = new Controls.SideBalanceControl();
            sideBalanceRight = new Controls.SideBalanceControl();
            panelTop = new Panel();
            panelCheckboxes = new Panel();
            checkBoxPrimRof = new CheckBox();
            checkBoxPrimDamage = new CheckBox();
            checkBoxPowerOutput = new CheckBox();
            checkBoxSpeed = new CheckBox();
            checkBoxPoints = new CheckBox();
            checkBoxStrength = new CheckBox();
            ultraComboTypes = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            label1 = new Label();
            panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            panelTop.SuspendLayout();
            panelCheckboxes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboTypes).BeginInit();
            SuspendLayout();
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonClose);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(8, 676);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1148, 37);
            panelBottom.TabIndex = 1;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.Image = (Image)resources.GetObject("buttonClose.Image");
            buttonClose.Location = new Point(1055, 8);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(90, 26);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "Close";
            buttonClose.TextAlign = ContentAlignment.MiddleRight;
            buttonClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // splitContainerMain
            // 
            splitContainerMain.BackColor = Color.DarkGray;
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Location = new Point(8, 38);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(sideBalanceLeft);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(sideBalanceRight);
            splitContainerMain.Size = new Size(1148, 638);
            splitContainerMain.SplitterDistance = 565;
            splitContainerMain.SplitterWidth = 6;
            splitContainerMain.TabIndex = 2;
            // 
            // sideBalanceLeft
            // 
            sideBalanceLeft.BackColor = SystemColors.Control;
            sideBalanceLeft.BorderStyle = BorderStyle.FixedSingle;
            sideBalanceLeft.Dock = DockStyle.Fill;
            sideBalanceLeft.Location = new Point(0, 0);
            sideBalanceLeft.Name = "sideBalanceLeft";
            sideBalanceLeft.Size = new Size(565, 638);
            sideBalanceLeft.TabIndex = 0;
            sideBalanceLeft.AfterEntityValueChanged += sideBalanceLeft_AfterEntityValueChanged;
            // 
            // sideBalanceRight
            // 
            sideBalanceRight.BackColor = SystemColors.Control;
            sideBalanceRight.BorderStyle = BorderStyle.FixedSingle;
            sideBalanceRight.Dock = DockStyle.Fill;
            sideBalanceRight.Location = new Point(0, 0);
            sideBalanceRight.Name = "sideBalanceRight";
            sideBalanceRight.Size = new Size(577, 638);
            sideBalanceRight.TabIndex = 1;
            sideBalanceRight.AfterEntityValueChanged += sideBalanceRight_AfterEntityValueChanged;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(panelCheckboxes);
            panelTop.Controls.Add(ultraComboTypes);
            panelTop.Controls.Add(label1);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(8, 8);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1148, 30);
            panelTop.TabIndex = 3;
            // 
            // panelCheckboxes
            // 
            panelCheckboxes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelCheckboxes.Controls.Add(checkBoxPrimRof);
            panelCheckboxes.Controls.Add(checkBoxPrimDamage);
            panelCheckboxes.Controls.Add(checkBoxPowerOutput);
            panelCheckboxes.Controls.Add(checkBoxSpeed);
            panelCheckboxes.Controls.Add(checkBoxPoints);
            panelCheckboxes.Controls.Add(checkBoxStrength);
            panelCheckboxes.Location = new Point(287, 0);
            panelCheckboxes.Name = "panelCheckboxes";
            panelCheckboxes.Size = new Size(858, 30);
            panelCheckboxes.TabIndex = 3;
            // 
            // checkBoxPrimRof
            // 
            checkBoxPrimRof.AutoSize = true;
            checkBoxPrimRof.Dock = DockStyle.Left;
            checkBoxPrimRof.Location = new Point(487, 0);
            checkBoxPrimRof.Name = "checkBoxPrimRof";
            checkBoxPrimRof.Padding = new Padding(0, 0, 8, 0);
            checkBoxPrimRof.Size = new Size(147, 30);
            checkBoxPrimRof.TabIndex = 8;
            checkBoxPrimRof.Text = "Primary Weapon ROF";
            checkBoxPrimRof.UseVisualStyleBackColor = true;
            checkBoxPrimRof.CheckedChanged += checkBox_CheckedChanged;
            // 
            // checkBoxPrimDamage
            // 
            checkBoxPrimDamage.AutoSize = true;
            checkBoxPrimDamage.Checked = true;
            checkBoxPrimDamage.CheckState = CheckState.Checked;
            checkBoxPrimDamage.Dock = DockStyle.Left;
            checkBoxPrimDamage.Location = new Point(318, 0);
            checkBoxPrimDamage.Name = "checkBoxPrimDamage";
            checkBoxPrimDamage.Padding = new Padding(0, 0, 8, 0);
            checkBoxPrimDamage.Size = new Size(169, 30);
            checkBoxPrimDamage.TabIndex = 7;
            checkBoxPrimDamage.Text = "Primary Weapon Damage";
            checkBoxPrimDamage.UseVisualStyleBackColor = true;
            checkBoxPrimDamage.CheckedChanged += checkBox_CheckedChanged;
            // 
            // checkBoxPowerOutput
            // 
            checkBoxPowerOutput.AutoSize = true;
            checkBoxPowerOutput.Dock = DockStyle.Left;
            checkBoxPowerOutput.Location = new Point(212, 0);
            checkBoxPowerOutput.Name = "checkBoxPowerOutput";
            checkBoxPowerOutput.Padding = new Padding(0, 0, 8, 0);
            checkBoxPowerOutput.Size = new Size(106, 30);
            checkBoxPowerOutput.TabIndex = 6;
            checkBoxPowerOutput.Text = "Power output";
            checkBoxPowerOutput.UseVisualStyleBackColor = true;
            checkBoxPowerOutput.CheckedChanged += checkBox_CheckedChanged;
            // 
            // checkBoxSpeed
            // 
            checkBoxSpeed.AutoSize = true;
            checkBoxSpeed.Dock = DockStyle.Left;
            checkBoxSpeed.Location = new Point(146, 0);
            checkBoxSpeed.Name = "checkBoxSpeed";
            checkBoxSpeed.Padding = new Padding(0, 0, 8, 0);
            checkBoxSpeed.Size = new Size(66, 30);
            checkBoxSpeed.TabIndex = 5;
            checkBoxSpeed.Text = "Speed";
            checkBoxSpeed.UseVisualStyleBackColor = true;
            checkBoxSpeed.CheckedChanged += checkBox_CheckedChanged;
            // 
            // checkBoxPoints
            // 
            checkBoxPoints.AutoSize = true;
            checkBoxPoints.Dock = DockStyle.Left;
            checkBoxPoints.Location = new Point(79, 0);
            checkBoxPoints.Name = "checkBoxPoints";
            checkBoxPoints.Padding = new Padding(0, 0, 8, 0);
            checkBoxPoints.Size = new Size(67, 30);
            checkBoxPoints.TabIndex = 4;
            checkBoxPoints.Text = "Points";
            checkBoxPoints.UseVisualStyleBackColor = true;
            checkBoxPoints.CheckedChanged += checkBox_CheckedChanged;
            // 
            // checkBoxStrength
            // 
            checkBoxStrength.AutoSize = true;
            checkBoxStrength.Checked = true;
            checkBoxStrength.CheckState = CheckState.Checked;
            checkBoxStrength.Dock = DockStyle.Left;
            checkBoxStrength.Location = new Point(0, 0);
            checkBoxStrength.Name = "checkBoxStrength";
            checkBoxStrength.Padding = new Padding(0, 0, 8, 0);
            checkBoxStrength.Size = new Size(79, 30);
            checkBoxStrength.TabIndex = 3;
            checkBoxStrength.Text = "Strength";
            checkBoxStrength.UseVisualStyleBackColor = true;
            checkBoxStrength.CheckedChanged += checkBox_CheckedChanged;
            // 
            // ultraComboTypes
            // 
            ultraComboTypes.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            appearance11.Image = resources.GetObject("appearance11.Image");
            valueListItem11.Appearance = appearance11;
            valueListItem11.DataValue = "BuildingTypes";
            valueListItem11.DisplayText = "Buildings";
            appearance12.Image = resources.GetObject("appearance12.Image");
            valueListItem12.Appearance = appearance12;
            valueListItem12.DataValue = "InfantryTypes";
            valueListItem12.DisplayText = "Infantry";
            appearance13.Image = resources.GetObject("appearance13.Image");
            valueListItem13.Appearance = appearance13;
            valueListItem13.DataValue = "VehicleTypes";
            valueListItem13.DisplayText = "Vehicles";
            appearance14.Image = resources.GetObject("appearance14.Image");
            valueListItem14.Appearance = appearance14;
            valueListItem14.DataValue = "AircraftTypes";
            valueListItem14.DisplayText = "Aircrafts";
            ultraComboTypes.Items.AddRange(new Infragistics.Win.ValueListItem[] { valueListItem11, valueListItem12, valueListItem13, valueListItem14 });
            ultraComboTypes.Location = new Point(70, 2);
            ultraComboTypes.Name = "ultraComboTypes";
            ultraComboTypes.Size = new Size(196, 25);
            ultraComboTypes.TabIndex = 2;
            ultraComboTypes.ValueChanged += ultraComboTypes_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 6);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Compare:";
            // 
            // BalancingToolForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1164, 721);
            Controls.Add(splitContainerMain);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "BalancingToolForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Balancing Tool";
            Load += BalancingToolForm_Load;
            panelBottom.ResumeLayout(false);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelCheckboxes.ResumeLayout(false);
            panelCheckboxes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ultraComboTypes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBottom;
        private Button buttonClose;
        private SplitContainer splitContainerMain;
        private Controls.SideBalanceControl sideBalanceLeft;
        private Controls.SideBalanceControl sideBalanceRight;
        private Panel panelTop;
        private Label label1;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboTypes;
        private CheckBox checkBoxStrength;
        private Panel panelCheckboxes;
        private CheckBox checkBoxSpeed;
        private CheckBox checkBoxPoints;
        private CheckBox checkBoxPowerOutput;
        private CheckBox checkBoxPrimRof;
        private CheckBox checkBoxPrimDamage;
    }
}