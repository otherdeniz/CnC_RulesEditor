namespace Deniz.TiberiumSunEditor.Gui.Controls.EntityEdit
{
    partial class AiTeamEditControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AiTeamEditControl));
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            unitEdit = new UnitEditControl();
            panelTop = new Panel();
            buttonRefreshName = new Button();
            panelButtons = new Panel();
            ButtonDelete = new Button();
            ButtonCopy = new Button();
            textName = new TextBox();
            labelKey = new Label();
            ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            entitiesListTriggers = new EntitiesListControl();
            ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            ultraTabPageControl1.SuspendLayout();
            panelTop.SuspendLayout();
            panelButtons.SuspendLayout();
            ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ultraTab).BeginInit();
            ultraTab.SuspendLayout();
            SuspendLayout();
            // 
            // ultraTabPageControl1
            // 
            ultraTabPageControl1.Controls.Add(unitEdit);
            ultraTabPageControl1.Controls.Add(panelTop);
            ultraTabPageControl1.Location = new Point(1, 25);
            ultraTabPageControl1.Name = "ultraTabPageControl1";
            ultraTabPageControl1.Size = new Size(687, 342);
            // 
            // unitEdit
            // 
            unitEdit.BackColor = Color.White;
            unitEdit.Dock = DockStyle.Fill;
            unitEdit.Location = new Point(0, 39);
            unitEdit.Margin = new Padding(4, 3, 4, 3);
            unitEdit.Name = "unitEdit";
            unitEdit.ShowHeaderAndFooter = false;
            unitEdit.Size = new Size(687, 303);
            unitEdit.TabIndex = 2;
            unitEdit.Tag = "PLAIN";
            unitEdit.UseValueNameColumn = true;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(buttonRefreshName);
            panelTop.Controls.Add(panelButtons);
            panelTop.Controls.Add(textName);
            panelTop.Controls.Add(labelKey);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(687, 39);
            panelTop.TabIndex = 1;
            // 
            // buttonRefreshName
            // 
            buttonRefreshName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRefreshName.Image = (Image)resources.GetObject("buttonRefreshName.Image");
            buttonRefreshName.Location = new Point(468, 5);
            buttonRefreshName.Name = "buttonRefreshName";
            buttonRefreshName.Size = new Size(27, 27);
            buttonRefreshName.TabIndex = 9;
            buttonRefreshName.TextAlign = ContentAlignment.MiddleRight;
            buttonRefreshName.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonRefreshName.UseVisualStyleBackColor = true;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(ButtonDelete);
            panelButtons.Controls.Add(ButtonCopy);
            panelButtons.Dock = DockStyle.Right;
            panelButtons.Location = new Point(507, 0);
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
            ButtonCopy.Visible = false;
            // 
            // textName
            // 
            textName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textName.Location = new Point(92, 7);
            textName.Name = "textName";
            textName.Size = new Size(376, 23);
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
            // ultraTabPageControl2
            // 
            ultraTabPageControl2.Controls.Add(entitiesListTriggers);
            ultraTabPageControl2.Location = new Point(-10000, -10000);
            ultraTabPageControl2.Name = "ultraTabPageControl2";
            ultraTabPageControl2.Size = new Size(687, 342);
            // 
            // entitiesListTriggers
            // 
            entitiesListTriggers.Dock = DockStyle.Fill;
            entitiesListTriggers.ListOnTop = true;
            entitiesListTriggers.ListSize = 140;
            entitiesListTriggers.Location = new Point(0, 0);
            entitiesListTriggers.Name = "entitiesListTriggers";
            entitiesListTriggers.Size = new Size(687, 342);
            entitiesListTriggers.TabIndex = 0;
            // 
            // ultraTab
            // 
            ultraTab.Controls.Add(ultraTabSharedControlsPage1);
            ultraTab.Controls.Add(ultraTabPageControl1);
            ultraTab.Controls.Add(ultraTabPageControl2);
            ultraTab.Dock = DockStyle.Fill;
            ultraTab.Location = new Point(0, 0);
            ultraTab.Name = "ultraTab";
            appearance11.FontData.BoldAsString = "True";
            ultraTab.SelectedTabAppearance = appearance11;
            ultraTab.SharedControlsPage = ultraTabSharedControlsPage1;
            ultraTab.Size = new Size(691, 370);
            ultraTab.TabIndex = 3;
            ultraTab11.TabPage = ultraTabPageControl1;
            ultraTab11.Text = "Team";
            ultraTab12.TabPage = ultraTabPageControl2;
            ultraTab12.Text = "Triggers";
            ultraTab.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] { ultraTab11, ultraTab12 });
            // 
            // ultraTabSharedControlsPage1
            // 
            ultraTabSharedControlsPage1.Location = new Point(-10000, -10000);
            ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            ultraTabSharedControlsPage1.Size = new Size(687, 342);
            // 
            // AiTeamEditControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ultraTab);
            Name = "AiTeamEditControl";
            Size = new Size(691, 370);
            ultraTabPageControl1.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelButtons.ResumeLayout(false);
            ultraTabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ultraTab).EndInit();
            ultraTab.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button buttonRefreshName;
        private Panel panelButtons;
        private Button ButtonDelete;
        private Button ButtonCopy;
        private TextBox textName;
        private Label labelKey;
        private UnitEditControl unitEdit;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTab;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private EntitiesListControl entitiesListTriggers;
    }
}
