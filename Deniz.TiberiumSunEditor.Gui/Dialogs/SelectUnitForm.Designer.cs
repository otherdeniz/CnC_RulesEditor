namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class SelectUnitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectUnitForm));
            panelBottom = new Panel();
            buttonCancel = new Button();
            ultraPanelUnits = new Infragistics.Win.Misc.UltraPanel();
            toolStripPaging = new ToolStrip();
            toolStripButtonPrev = new ToolStripButton();
            toolStripLabelItem = new ToolStripLabel();
            toolStripButtonNext = new ToolStripButton();
            toolStripLabelTotal = new ToolStripLabel();
            panelBottom.SuspendLayout();
            ultraPanelUnits.SuspendLayout();
            toolStripPaging.SuspendLayout();
            SuspendLayout();
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonCancel);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(4, 527);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(907, 43);
            panelBottom.TabIndex = 1;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.Image = (Image)resources.GetObject("buttonCancel.Image");
            buttonCancel.Location = new Point(805, 9);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 26);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.TextAlign = ContentAlignment.MiddleRight;
            buttonCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // ultraPanelUnits
            // 
            ultraPanelUnits.AutoScroll = true;
            ultraPanelUnits.Dock = DockStyle.Fill;
            ultraPanelUnits.Location = new Point(4, 4);
            ultraPanelUnits.Name = "ultraPanelUnits";
            ultraPanelUnits.Size = new Size(907, 498);
            ultraPanelUnits.TabIndex = 2;
            // 
            // toolStripPaging
            // 
            toolStripPaging.Dock = DockStyle.Bottom;
            toolStripPaging.Items.AddRange(new ToolStripItem[] { toolStripButtonPrev, toolStripLabelItem, toolStripButtonNext, toolStripLabelTotal });
            toolStripPaging.Location = new Point(4, 502);
            toolStripPaging.Name = "toolStripPaging";
            toolStripPaging.Size = new Size(907, 25);
            toolStripPaging.TabIndex = 3;
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
            // SelectUnitForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(915, 574);
            Controls.Add(ultraPanelUnits);
            Controls.Add(toolStripPaging);
            Controls.Add(panelBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SelectUnitForm";
            Padding = new Padding(4);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Techno";
            Load += SelectUnitForm_Load;
            panelBottom.ResumeLayout(false);
            ultraPanelUnits.ResumeLayout(false);
            toolStripPaging.ResumeLayout(false);
            toolStripPaging.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelBottom;
        private Button buttonCancel;
        private Infragistics.Win.Misc.UltraPanel ultraPanelUnits;
        private ToolStrip toolStripPaging;
        private ToolStripButton toolStripButtonPrev;
        private ToolStripLabel toolStripLabelItem;
        private ToolStripButton toolStripButtonNext;
        private ToolStripLabel toolStripLabelTotal;
    }
}