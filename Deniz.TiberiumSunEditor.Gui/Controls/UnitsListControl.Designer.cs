namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class UnitsListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitsListControl));
            unitsLayoutPanel = new FlowLayoutPanel();
            unitPickerControl1 = new UnitPickerControl();
            splitterUnitPicker = new Splitter();
            unitEdit = new UnitEditControl();
            panelLeft = new Panel();
            ultraPanelScroll = new Infragistics.Win.Misc.UltraPanel();
            checkBoxOnlyModified = new CheckBox();
            toolStripAdd = new ToolStrip();
            buttonAddUnit = new ToolStripButton();
            buttonAddEmpty = new ToolStripButton();
            toolStripPaging = new ToolStrip();
            toolStripButtonPrev = new ToolStripButton();
            toolStripLabelItem = new ToolStripLabel();
            toolStripButtonNext = new ToolStripButton();
            toolStripLabelTotal = new ToolStripLabel();
            unitsLayoutPanel.SuspendLayout();
            panelLeft.SuspendLayout();
            ultraPanelScroll.ClientArea.SuspendLayout();
            ultraPanelScroll.SuspendLayout();
            toolStripAdd.SuspendLayout();
            toolStripPaging.SuspendLayout();
            SuspendLayout();
            // 
            // unitsLayoutPanel
            // 
            unitsLayoutPanel.AutoSize = true;
            unitsLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            unitsLayoutPanel.Controls.Add(unitPickerControl1);
            unitsLayoutPanel.Dock = DockStyle.Top;
            unitsLayoutPanel.Location = new Point(0, 0);
            unitsLayoutPanel.Margin = new Padding(4, 3, 4, 3);
            unitsLayoutPanel.Name = "unitsLayoutPanel";
            unitsLayoutPanel.Size = new Size(240, 96);
            unitsLayoutPanel.TabIndex = 1;
            // 
            // unitPickerControl1
            // 
            unitPickerControl1.BackColor = Color.White;
            unitPickerControl1.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl1.Location = new Point(5, 3);
            unitPickerControl1.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl1.Name = "unitPickerControl1";
            unitPickerControl1.Size = new Size(101, 90);
            unitPickerControl1.TabIndex = 0;
            unitPickerControl1.Tag = "KEEP";
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Location = new Point(240, 0);
            splitterUnitPicker.Margin = new Padding(4, 3, 4, 3);
            splitterUnitPicker.Name = "splitterUnitPicker";
            splitterUnitPicker.Size = new Size(6, 444);
            splitterUnitPicker.TabIndex = 2;
            splitterUnitPicker.TabStop = false;
            // 
            // unitEdit
            // 
            unitEdit.BackColor = Color.White;
            unitEdit.Dock = DockStyle.Fill;
            unitEdit.Location = new Point(246, 0);
            unitEdit.Margin = new Padding(5, 3, 5, 3);
            unitEdit.Name = "unitEdit";
            unitEdit.Size = new Size(502, 444);
            unitEdit.TabIndex = 3;
            unitEdit.Tag = "PLAIN";
            unitEdit.Visible = false;
            unitEdit.FavoriteClick += unitEdit_FavoriteClick;
            unitEdit.UnitModificationsChanged += unitEdit_UnitModificationsChanged;
            unitEdit.UnitCreateCopy += unitEdit_UnitCreateCopy;
            unitEdit.UnitDelete += unitEdit_UnitDelete;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(ultraPanelScroll);
            panelLeft.Controls.Add(checkBoxOnlyModified);
            panelLeft.Controls.Add(toolStripAdd);
            panelLeft.Controls.Add(toolStripPaging);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.MinimumSize = new Size(128, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(240, 444);
            panelLeft.TabIndex = 4;
            // 
            // ultraPanelScroll
            // 
            ultraPanelScroll.AutoScroll = true;
            // 
            // ultraPanelScroll.ClientArea
            // 
            ultraPanelScroll.ClientArea.Controls.Add(unitsLayoutPanel);
            ultraPanelScroll.Dock = DockStyle.Fill;
            ultraPanelScroll.Location = new Point(0, 43);
            ultraPanelScroll.Name = "ultraPanelScroll";
            ultraPanelScroll.Size = new Size(240, 376);
            ultraPanelScroll.TabIndex = 5;
            // 
            // checkBoxOnlyModified
            // 
            checkBoxOnlyModified.Dock = DockStyle.Top;
            checkBoxOnlyModified.Location = new Point(0, 25);
            checkBoxOnlyModified.Name = "checkBoxOnlyModified";
            checkBoxOnlyModified.Padding = new Padding(6, 0, 0, 0);
            checkBoxOnlyModified.Size = new Size(240, 18);
            checkBoxOnlyModified.TabIndex = 4;
            checkBoxOnlyModified.Text = "Show only modified items";
            checkBoxOnlyModified.UseVisualStyleBackColor = true;
            checkBoxOnlyModified.CheckedChanged += checkBoxOnlyModified_CheckedChanged;
            // 
            // toolStripAdd
            // 
            toolStripAdd.Items.AddRange(new ToolStripItem[] { buttonAddUnit, buttonAddEmpty });
            toolStripAdd.Location = new Point(0, 0);
            toolStripAdd.Name = "toolStripAdd";
            toolStripAdd.Size = new Size(240, 25);
            toolStripAdd.TabIndex = 3;
            toolStripAdd.Text = "toolStrip1";
            // 
            // buttonAddUnit
            // 
            buttonAddUnit.Image = (Image)resources.GetObject("buttonAddUnit.Image");
            buttonAddUnit.ImageTransparentColor = Color.Magenta;
            buttonAddUnit.Name = "buttonAddUnit";
            buttonAddUnit.Size = new Size(118, 22);
            buttonAddUnit.Text = "Add unlisted unit";
            buttonAddUnit.Click += buttonAddUnit_Click;
            // 
            // buttonAddEmpty
            // 
            buttonAddEmpty.Image = (Image)resources.GetObject("buttonAddEmpty.Image");
            buttonAddEmpty.ImageTransparentColor = Color.Magenta;
            buttonAddEmpty.Name = "buttonAddEmpty";
            buttonAddEmpty.Size = new Size(49, 22);
            buttonAddEmpty.Text = "Add";
            buttonAddEmpty.Click += buttonAddEmpty_Click;
            // 
            // toolStripPaging
            // 
            toolStripPaging.Dock = DockStyle.Bottom;
            toolStripPaging.Items.AddRange(new ToolStripItem[] { toolStripButtonPrev, toolStripLabelItem, toolStripButtonNext, toolStripLabelTotal });
            toolStripPaging.Location = new Point(0, 419);
            toolStripPaging.Name = "toolStripPaging";
            toolStripPaging.Size = new Size(240, 25);
            toolStripPaging.TabIndex = 2;
            // 
            // toolStripButtonPrev
            // 
            toolStripButtonPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonPrev.Enabled = false;
            toolStripButtonPrev.Image = (Image)resources.GetObject("toolStripButtonPrev.Image");
            toolStripButtonPrev.ImageTransparentColor = Color.Magenta;
            toolStripButtonPrev.Name = "toolStripButtonPrev";
            toolStripButtonPrev.Size = new Size(23, 22);
            toolStripButtonPrev.Text = "toolStripButton1";
            toolStripButtonPrev.Click += toolStripButtonPrev_Click;
            // 
            // toolStripLabelItem
            // 
            toolStripLabelItem.Name = "toolStripLabelItem";
            toolStripLabelItem.Size = new Size(54, 22);
            toolStripLabelItem.Text = "001 - 250";
            // 
            // toolStripButtonNext
            // 
            toolStripButtonNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonNext.Enabled = false;
            toolStripButtonNext.Image = (Image)resources.GetObject("toolStripButtonNext.Image");
            toolStripButtonNext.ImageTransparentColor = Color.Magenta;
            toolStripButtonNext.Name = "toolStripButtonNext";
            toolStripButtonNext.Size = new Size(23, 22);
            toolStripButtonNext.Text = "toolStripButton2";
            toolStripButtonNext.Click += toolStripButtonNext_Click;
            // 
            // toolStripLabelTotal
            // 
            toolStripLabelTotal.Name = "toolStripLabelTotal";
            toolStripLabelTotal.Size = new Size(66, 22);
            toolStripLabelTotal.Text = "1'000 Items";
            // 
            // UnitsListControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(unitEdit);
            Controls.Add(splitterUnitPicker);
            Controls.Add(panelLeft);
            Margin = new Padding(4, 3, 4, 3);
            Name = "UnitsListControl";
            Size = new Size(748, 444);
            Tag = "PLAIN";
            Load += UnitsListControl_Load;
            unitsLayoutPanel.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ultraPanelScroll.ClientArea.ResumeLayout(false);
            ultraPanelScroll.ClientArea.PerformLayout();
            ultraPanelScroll.ResumeLayout(false);
            toolStripAdd.ResumeLayout(false);
            toolStripAdd.PerformLayout();
            toolStripPaging.ResumeLayout(false);
            toolStripPaging.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel unitsLayoutPanel;
        private UnitPickerControl unitPickerControl1;
        private Splitter splitterUnitPicker;
        private UnitEditControl unitEdit;
        private Panel panelLeft;
        private ToolStrip toolStripPaging;
        private ToolStripButton toolStripButtonPrev;
        private ToolStripLabel toolStripLabelItem;
        private ToolStripButton toolStripButtonNext;
        private ToolStripLabel toolStripLabelTotal;
        private ToolStrip toolStripAdd;
        private ToolStripButton buttonAddUnit;
        private ToolStripButton buttonAddEmpty;
        private CheckBox checkBoxOnlyModified;
        private Infragistics.Win.Misc.UltraPanel ultraPanelScroll;
    }
}
