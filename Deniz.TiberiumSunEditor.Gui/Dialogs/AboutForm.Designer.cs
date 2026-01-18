namespace Deniz.TiberiumSunEditor.Gui.Dialogs
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            label6 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            groupBox2 = new GroupBox();
            linkLabelDiscord = new LinkLabel();
            linkLabelCncnet = new LinkLabel();
            linkLabelGithub = new LinkLabel();
            linkLabel1 = new LinkLabel();
            linkLabelRelease = new LinkLabel();
            label9 = new Label();
            labelDownloads = new Label();
            labelReleaseDate = new Label();
            labelVersion = new Label();
            label7 = new Label();
            label5 = new Label();
            label14 = new Label();
            label4 = new Label();
            label3 = new Label();
            label8 = new Label();
            label1 = new Label();
            groupBox3 = new GroupBox();
            textBoxLicense = new TextBox();
            groupBoxCredits = new GroupBox();
            label13 = new Label();
            label12 = new Label();
            label10 = new Label();
            label11 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBoxCredits.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label2);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(104, 161);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Author";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(9, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(80, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 120);
            label6.Name = "label6";
            label6.Size = new Size(91, 15);
            label6.TabIndex = 0;
            label6.Text = "[WH]otherdeniz";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 105);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 0;
            label2.Text = "Deniz Esen";
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(8, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(655, 161);
            panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(linkLabelDiscord);
            groupBox2.Controls.Add(linkLabelCncnet);
            groupBox2.Controls.Add(linkLabelGithub);
            groupBox2.Controls.Add(linkLabel1);
            groupBox2.Controls.Add(linkLabelRelease);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(labelDownloads);
            groupBox2.Controls.Add(labelReleaseDate);
            groupBox2.Controls.Add(labelVersion);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(104, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(551, 161);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Application";
            // 
            // linkLabelDiscord
            // 
            linkLabelDiscord.AutoSize = true;
            linkLabelDiscord.Location = new Point(116, 137);
            linkLabelDiscord.Name = "linkLabelDiscord";
            linkLabelDiscord.Size = new Size(98, 15);
            linkLabelDiscord.TabIndex = 2;
            linkLabelDiscord.TabStop = true;
            linkLabelDiscord.Tag = "discord.gg/dPCcB2kGBs";
            linkLabelDiscord.Text = "C&&C Mod Haven";
            linkLabelDiscord.LinkClicked += linkLabel_LinkClicked;
            // 
            // linkLabelCncnet
            // 
            linkLabelCncnet.AutoSize = true;
            linkLabelCncnet.Location = new Point(116, 118);
            linkLabelCncnet.Name = "linkLabelCncnet";
            linkLabelCncnet.Size = new Size(384, 15);
            linkLabelCncnet.TabIndex = 2;
            linkLabelCncnet.TabStop = true;
            linkLabelCncnet.Text = "forums.cncnet.org/topic/12869-tiberian-sun-rules-editor-version-2024/";
            linkLabelCncnet.LinkClicked += linkLabel_LinkClicked;
            // 
            // linkLabelGithub
            // 
            linkLabelGithub.AutoSize = true;
            linkLabelGithub.Location = new Point(116, 99);
            linkLabelGithub.Name = "linkLabelGithub";
            linkLabelGithub.Size = new Size(222, 15);
            linkLabelGithub.TabIndex = 2;
            linkLabelGithub.TabStop = true;
            linkLabelGithub.Text = "github.com/otherdeniz/CnC_RulesEditor";
            linkLabelGithub.LinkClicked += linkLabel_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(116, 60);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(134, 15);
            linkLabel1.TabIndex = 2;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "ruleseditor.denizesen.ch";
            linkLabel1.LinkClicked += linkLabel_LinkClicked;
            // 
            // linkLabelRelease
            // 
            linkLabelRelease.AutoSize = true;
            linkLabelRelease.Location = new Point(116, 79);
            linkLabelRelease.Name = "linkLabelRelease";
            linkLabelRelease.Size = new Size(268, 15);
            linkLabelRelease.TabIndex = 2;
            linkLabelRelease.TabStop = true;
            linkLabelRelease.Text = "github.com/otherdeniz/CnC_RulesEditor/releases";
            linkLabelRelease.LinkClicked += linkLabel_LinkClicked;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(216, 137);
            label9.Name = "label9";
            label9.Size = new Size(99, 15);
            label9.TabIndex = 0;
            label9.Text = "#cnc-rules-editor";
            // 
            // labelDownloads
            // 
            labelDownloads.AutoSize = true;
            labelDownloads.Location = new Point(174, 22);
            labelDownloads.Name = "labelDownloads";
            labelDownloads.Size = new Size(248, 15);
            labelDownloads.TabIndex = 0;
            labelDownloads.Text = "Total: 0 downloads, This Version: 0 downloads";
            // 
            // labelReleaseDate
            // 
            labelReleaseDate.AutoSize = true;
            labelReleaseDate.Location = new Point(116, 41);
            labelReleaseDate.Name = "labelReleaseDate";
            labelReleaseDate.Size = new Size(49, 15);
            labelReleaseDate.TabIndex = 0;
            labelReleaseDate.Text = "0.0.0000";
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(116, 22);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(37, 15);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "v0.0.0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 137);
            label7.Name = "label7";
            label7.Size = new Size(97, 15);
            label7.TabIndex = 0;
            label7.Text = "Discord Channel:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 118);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 0;
            label5.Text = "Cncnet Forum:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(9, 60);
            label14.Name = "label14";
            label14.Size = new Size(69, 15);
            label14.TabIndex = 0;
            label14.Text = "Homepage:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 99);
            label4.Name = "label4";
            label4.Size = new Size(86, 15);
            label4.TabIndex = 0;
            label4.Text = "Github Project:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 79);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 0;
            label3.Text = "Release Link:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 41);
            label8.Name = "label8";
            label8.Size = new Size(76, 15);
            label8.TabIndex = 0;
            label8.Text = "Release Date:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 22);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 0;
            label1.Text = "Latest Version:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBoxLicense);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(8, 231);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(655, 263);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "License";
            // 
            // textBoxLicense
            // 
            textBoxLicense.Dock = DockStyle.Fill;
            textBoxLicense.Location = new Point(3, 19);
            textBoxLicense.MaxLength = 100000;
            textBoxLicense.Multiline = true;
            textBoxLicense.Name = "textBoxLicense";
            textBoxLicense.ReadOnly = true;
            textBoxLicense.ScrollBars = ScrollBars.Both;
            textBoxLicense.Size = new Size(649, 241);
            textBoxLicense.TabIndex = 0;
            // 
            // groupBoxCredits
            // 
            groupBoxCredits.Controls.Add(label13);
            groupBoxCredits.Controls.Add(label12);
            groupBoxCredits.Controls.Add(label10);
            groupBoxCredits.Controls.Add(label11);
            groupBoxCredits.Dock = DockStyle.Top;
            groupBoxCredits.Location = new Point(8, 169);
            groupBoxCredits.Name = "groupBoxCredits";
            groupBoxCredits.Size = new Size(655, 62);
            groupBoxCredits.TabIndex = 3;
            groupBoxCredits.TabStop = false;
            groupBoxCredits.Text = "Credits";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(9, 38);
            label13.Name = "label13";
            label13.Size = new Size(79, 15);
            label13.TabIndex = 0;
            label13.Text = "Feature ideas:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(113, 38);
            label12.Name = "label12";
            label12.Size = new Size(169, 15);
            label12.TabIndex = 0;
            label12.Text = "GOD-EMPEROR, E1 Elite, Wirus";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(9, 19);
            label10.Name = "label10";
            label10.Size = new Size(85, 15);
            label10.TabIndex = 0;
            label10.Text = "Special thanks:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(113, 19);
            label11.Name = "label11";
            label11.Size = new Size(172, 15);
            label11.TabIndex = 0;
            label11.Text = "[WH]ela, RAmpastring, Kerbiter";
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(671, 502);
            Controls.Add(groupBox3);
            Controls.Add(groupBoxCredits);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AboutForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            Load += AboutForm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBoxCredits.ResumeLayout(false);
            groupBoxCredits.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private GroupBox groupBox2;
        private LinkLabel linkLabelCncnet;
        private LinkLabel linkLabelGithub;
        private LinkLabel linkLabelRelease;
        private Label labelDownloads;
        private Label labelVersion;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private LinkLabel linkLabelDiscord;
        private Label label7;
        private GroupBox groupBox3;
        private Label label6;
        private Label label2;
        private Label label9;
        private Label labelReleaseDate;
        private Label label8;
        private TextBox textBoxLicense;
        private GroupBox groupBoxCredits;
        private Label label13;
        private Label label12;
        private Label label10;
        private Label label11;
        private LinkLabel linkLabel1;
        private Label label14;
    }
}