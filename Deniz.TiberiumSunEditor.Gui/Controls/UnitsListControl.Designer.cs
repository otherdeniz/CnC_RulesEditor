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
            unitPickerControl2 = new UnitPickerControl();
            unitPickerControl3 = new UnitPickerControl();
            unitPickerControl4 = new UnitPickerControl();
            splitterUnitPicker = new Splitter();
            unitEdit = new UnitEditControl();
            panelLeft = new Panel();
            toolStripAdd = new ToolStrip();
            buttonAddUnit = new ToolStripButton();
            toolStripPaging = new ToolStrip();
            toolStripButtonPrev = new ToolStripButton();
            toolStripLabelItem = new ToolStripLabel();
            toolStripButtonNext = new ToolStripButton();
            toolStripLabelTotal = new ToolStripLabel();
            unitsLayoutPanel.SuspendLayout();
            panelLeft.SuspendLayout();
            toolStripAdd.SuspendLayout();
            toolStripPaging.SuspendLayout();
            SuspendLayout();
            // 
            // unitsLayoutPanel
            // 
            unitsLayoutPanel.AutoScroll = true;
            unitsLayoutPanel.Controls.Add(unitPickerControl1);
            unitsLayoutPanel.Controls.Add(unitPickerControl2);
            unitsLayoutPanel.Controls.Add(unitPickerControl3);
            unitsLayoutPanel.Controls.Add(unitPickerControl4);
            unitsLayoutPanel.Dock = DockStyle.Fill;
            unitsLayoutPanel.Location = new Point(0, 25);
            unitsLayoutPanel.Margin = new Padding(4, 3, 4, 3);
            unitsLayoutPanel.Name = "unitsLayoutPanel";
            unitsLayoutPanel.Size = new Size(238, 394);
            unitsLayoutPanel.TabIndex = 1;
            // 
            // unitPickerControl1
            // 
            unitPickerControl1.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl1.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl1.Location = new Point(5, 3);
            unitPickerControl1.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl1.Name = "unitPickerControl1";
            unitPickerControl1.Size = new Size(101, 90);
            unitPickerControl1.TabIndex = 0;
            // 
            // unitPickerControl2
            // 
            unitPickerControl2.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl2.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl2.Location = new Point(116, 3);
            unitPickerControl2.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl2.Name = "unitPickerControl2";
            unitPickerControl2.Size = new Size(101, 90);
            unitPickerControl2.TabIndex = 1;
            // 
            // unitPickerControl3
            // 
            unitPickerControl3.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl3.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl3.Location = new Point(5, 99);
            unitPickerControl3.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl3.Name = "unitPickerControl3";
            unitPickerControl3.Size = new Size(101, 90);
            unitPickerControl3.TabIndex = 2;
            // 
            // unitPickerControl4
            // 
            unitPickerControl4.BackColor = Color.FromArgb(0, 255, 255, 255);
            unitPickerControl4.BorderStyle = BorderStyle.FixedSingle;
            unitPickerControl4.Location = new Point(116, 99);
            unitPickerControl4.Margin = new Padding(5, 3, 5, 3);
            unitPickerControl4.Name = "unitPickerControl4";
            unitPickerControl4.Size = new Size(101, 90);
            unitPickerControl4.TabIndex = 3;
            // 
            // splitterUnitPicker
            // 
            splitterUnitPicker.BackColor = SystemColors.ActiveBorder;
            splitterUnitPicker.Location = new Point(238, 0);
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
            unitEdit.Location = new Point(244, 0);
            unitEdit.Margin = new Padding(5, 3, 5, 3);
            unitEdit.Name = "unitEdit";
            unitEdit.Size = new Size(504, 444);
            unitEdit.TabIndex = 3;
            unitEdit.Visible = false;
            unitEdit.FavoriteClick += unitEdit_FavoriteClick;
            unitEdit.UnitModificationsChanged += unitEdit_UnitModificationsChanged;
            unitEdit.UnitCreateCopy += unitEdit_UnitCreateCopy;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(unitsLayoutPanel);
            panelLeft.Controls.Add(toolStripAdd);
            panelLeft.Controls.Add(toolStripPaging);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.MinimumSize = new Size(128, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(238, 444);
            panelLeft.TabIndex = 4;
            // 
            // toolStripAdd
            // 
            toolStripAdd.Items.AddRange(new ToolStripItem[] { buttonAddUnit });
            toolStripAdd.Location = new Point(0, 0);
            toolStripAdd.Name = "toolStripAdd";
            toolStripAdd.Size = new Size(238, 25);
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
            // toolStripPaging
            // 
            toolStripPaging.Dock = DockStyle.Bottom;
            toolStripPaging.Items.AddRange(new ToolStripItem[] { toolStripButtonPrev, toolStripLabelItem, toolStripButtonNext, toolStripLabelTotal });
            toolStripPaging.Location = new Point(0, 419);
            toolStripPaging.Name = "toolStripPaging";
            toolStripPaging.Size = new Size(238, 25);
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
            BackColor = Color.White;
            Controls.Add(unitEdit);
            Controls.Add(splitterUnitPicker);
            Controls.Add(panelLeft);
            Margin = new Padding(4, 3, 4, 3);
            Name = "UnitsListControl";
            Size = new Size(748, 444);
            Load += UnitsListControl_Load;
            unitsLayoutPanel.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            toolStripAdd.ResumeLayout(false);
            toolStripAdd.PerformLayout();
            toolStripPaging.ResumeLayout(false);
            toolStripPaging.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel unitsLayoutPanel;
        private UnitPickerControl unitPickerControl1;
        private UnitPickerControl unitPickerControl2;
        private UnitPickerControl unitPickerControl3;
        private UnitPickerControl unitPickerControl4;
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
    }
}
