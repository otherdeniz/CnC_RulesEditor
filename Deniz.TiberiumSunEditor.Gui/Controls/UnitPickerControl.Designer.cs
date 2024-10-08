﻿namespace Deniz.TiberiumSunEditor.Gui.Controls
{
    partial class UnitPickerControl
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
            DisposeManaged();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitPickerControl));
            pictureFavorite = new PictureBox();
            labelModifications = new Label();
            pictureThumbnail = new PictureBox();
            contextMenu = new ContextMenuStrip(components);
            popupButtonNewGroup = new ToolStripMenuItem();
            popupButtonRemoveFromGroup = new ToolStripMenuItem();
            ultraToolTips = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
            ((System.ComponentModel.ISupportInitialize)pictureFavorite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).BeginInit();
            contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pictureFavorite
            // 
            pictureFavorite.Cursor = Cursors.Hand;
            pictureFavorite.Image = (Image)resources.GetObject("pictureFavorite.Image");
            pictureFavorite.Location = new Point(78, 0);
            pictureFavorite.Margin = new Padding(4, 3, 4, 3);
            pictureFavorite.Name = "pictureFavorite";
            pictureFavorite.Size = new Size(23, 23);
            pictureFavorite.SizeMode = PictureBoxSizeMode.Zoom;
            pictureFavorite.TabIndex = 2;
            pictureFavorite.TabStop = false;
            pictureFavorite.Tag = "";
            pictureFavorite.Click += pictureFavorite_Click;
            // 
            // labelModifications
            // 
            labelModifications.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelModifications.ForeColor = Color.Red;
            labelModifications.Location = new Point(78, 29);
            labelModifications.Margin = new Padding(4, 0, 4, 0);
            labelModifications.Name = "labelModifications";
            labelModifications.Size = new Size(33, 23);
            labelModifications.TabIndex = 3;
            labelModifications.Text = "1";
            labelModifications.Visible = false;
            labelModifications.Click += labelModifications_Click;
            // 
            // pictureThumbnail
            // 
            pictureThumbnail.BackColor = Color.Transparent;
            pictureThumbnail.BackgroundImageLayout = ImageLayout.Center;
            pictureThumbnail.Image = (Image)resources.GetObject("pictureThumbnail.Image");
            pictureThumbnail.Location = new Point(-1, -1);
            pictureThumbnail.Margin = new Padding(4, 3, 4, 3);
            pictureThumbnail.Name = "pictureThumbnail";
            pictureThumbnail.Size = new Size(78, 58);
            pictureThumbnail.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureThumbnail.TabIndex = 4;
            pictureThumbnail.TabStop = false;
            pictureThumbnail.Visible = false;
            pictureThumbnail.Click += pictureThumbnail_Click;
            pictureThumbnail.MouseDown += pictureThumbnail_MouseDown;
            // 
            // contextMenu
            // 
            contextMenu.Items.AddRange(new ToolStripItem[] { popupButtonNewGroup, popupButtonRemoveFromGroup });
            contextMenu.Name = "contextMenu";
            contextMenu.Size = new Size(182, 48);
            // 
            // popupButtonNewGroup
            // 
            popupButtonNewGroup.Name = "popupButtonNewGroup";
            popupButtonNewGroup.Size = new Size(181, 22);
            popupButtonNewGroup.Text = "Add to new group";
            popupButtonNewGroup.Click += popupButtonNewGroup_Click;
            // 
            // popupButtonRemoveFromGroup
            // 
            popupButtonRemoveFromGroup.Name = "popupButtonRemoveFromGroup";
            popupButtonRemoveFromGroup.Size = new Size(181, 22);
            popupButtonRemoveFromGroup.Text = "Remove from group";
            popupButtonRemoveFromGroup.Click += popupButtonRemoveFromGroup_Click;
            // 
            // ultraToolTips
            // 
            ultraToolTips.ContainingControl = this;
            // 
            // UnitPickerControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pictureThumbnail);
            Controls.Add(pictureFavorite);
            Controls.Add(labelModifications);
            Margin = new Padding(4, 3, 4, 3);
            Name = "UnitPickerControl";
            Size = new Size(102, 90);
            Tag = "KEEP";
            Click += UnitPickerControl_Click;
            MouseDown += UnitPickerControl_MouseDown;
            ((System.ComponentModel.ISupportInitialize)pictureFavorite).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumbnail).EndInit();
            contextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureFavorite;
        private Label labelModifications;
        private PictureBox pictureThumbnail;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem popupButtonNewGroup;
        private ToolStripMenuItem popupButtonRemoveFromGroup;
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager ultraToolTips;
    }
}
