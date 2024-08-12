namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            groupBoxGames = new GroupBox();
            toolStrip1 = new ToolStrip();
            buttonAddCustomMod = new ToolStripButton();
            panelBottom = new Panel();
            buttonClose = new Button();
            panelGames = new Infragistics.Win.Misc.UltraPanel();
            groupBoxGames.SuspendLayout();
            toolStrip1.SuspendLayout();
            panelBottom.SuspendLayout();
            panelGames.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxGames
            // 
            groupBoxGames.Controls.Add(panelGames);
            groupBoxGames.Controls.Add(toolStrip1);
            groupBoxGames.Dock = DockStyle.Fill;
            groupBoxGames.Location = new Point(4, 4);
            groupBoxGames.Name = "groupBoxGames";
            groupBoxGames.Size = new Size(815, 504);
            groupBoxGames.TabIndex = 1;
            groupBoxGames.TabStop = false;
            groupBoxGames.Text = "Games and Mods";
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { buttonAddCustomMod });
            toolStrip1.Location = new Point(3, 19);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(809, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // buttonAddCustomMod
            // 
            buttonAddCustomMod.Image = (Image)resources.GetObject("buttonAddCustomMod.Image");
            buttonAddCustomMod.ImageTransparentColor = Color.Magenta;
            buttonAddCustomMod.Name = "buttonAddCustomMod";
            buttonAddCustomMod.Size = new Size(120, 22);
            buttonAddCustomMod.Text = "Add custom Mod";
            buttonAddCustomMod.Click += buttonAddCustomMod_Click;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(buttonClose);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(4, 508);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(815, 43);
            panelBottom.TabIndex = 0;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.Image = (Image)resources.GetObject("buttonClose.Image");
            buttonClose.Location = new Point(711, 9);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(90, 26);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "Close";
            buttonClose.TextAlign = ContentAlignment.MiddleRight;
            buttonClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // panelGames
            // 
            panelGames.AutoScroll = true;
            panelGames.Dock = DockStyle.Fill;
            panelGames.Location = new Point(3, 44);
            panelGames.Name = "panelGames";
            panelGames.Size = new Size(809, 457);
            panelGames.TabIndex = 2;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 555);
            Controls.Add(groupBoxGames);
            Controls.Add(panelBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsForm";
            Padding = new Padding(4);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Games Settings";
            Load += SettingsForm_Load;
            groupBoxGames.ResumeLayout(false);
            groupBoxGames.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelGames.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxGames;
        private Panel panelBottom;
        private Button buttonClose;
        private ToolStrip toolStrip1;
        private ToolStripButton buttonAddCustomMod;
        private Infragistics.Win.Misc.UltraPanel panelGames;
    }
}