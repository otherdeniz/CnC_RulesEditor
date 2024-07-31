// Decompiled with JetBrains decompiler
// Type: Microncode.Lic.frmLicense
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Microncode.Lic
{
  internal class frmLicense : Form
  {
    private string _AppName;
    private string _AppVersion;
    private string _AppDescription;
    private string _AppCompany;
    private string _AppRegisterLicense;
    private string _AppWebsite;
    private string _AppHomepage;
    private IContainer components = (IContainer) null;
    internal Label lblWiserbit;
    internal Label lblDescription;
    internal Label lblTitle;
    internal PictureBox PictureBox1;
    internal TextBox txtPurchaseLink;
    internal LinkLabel lnkCompany;
    internal Button cmdContinueTry;
    internal Button cmdGetRegKey;
    private PictureBox pictureBox2;

    internal frmLicense(
      string AppName,
      string AppVersion,
      string AppDescription,
      string AppCompany,
      string AppRegisterLicense,
      string AppWebsite,
      string AppHomepage)
    {
      try
      {
        this.InitializeComponent();
        this._AppName = AppName;
        this._AppVersion = AppVersion;
        this._AppDescription = AppDescription;
        this._AppCompany = AppCompany;
        this._AppRegisterLicense = AppRegisterLicense;
        this._AppWebsite = AppWebsite;
        this._AppHomepage = AppHomepage;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
    }

    private void frmLicense_Load(object sender, EventArgs e)
    {
      try
      {
        this.lblTitle.Text = this._AppName;
        this.lblDescription.Text = this._AppDescription;
        this.lnkCompany.Text = this._AppCompany;
        this.txtPurchaseLink.Text = this._AppRegisterLicense;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
    }

    private void cmdContinueTry_Click(object sender, EventArgs e) => this.Close();

    private void cmdGetRegKey_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start(this.txtPurchaseLink.Text);
        this.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
    }

    private void lnkCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        Process.Start(this.lnkCompany.Text);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmLicense));
      this.lblWiserbit = new Label();
      this.lblDescription = new Label();
      this.lblTitle = new Label();
      this.PictureBox1 = new PictureBox();
      this.txtPurchaseLink = new TextBox();
      this.lnkCompany = new LinkLabel();
      this.cmdContinueTry = new Button();
      this.cmdGetRegKey = new Button();
      this.pictureBox2 = new PictureBox();
      ((ISupportInitialize) this.PictureBox1).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.lblWiserbit.AutoSize = true;
      this.lblWiserbit.Font = new Font("Tahoma", 8.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.lblWiserbit.ForeColor = Color.Silver;
      this.lblWiserbit.Location = new Point(371, 177);
      this.lblWiserbit.Name = "lblWiserbit";
      this.lblWiserbit.Size = new Size(130, 13);
      this.lblWiserbit.TabIndex = 38;
      this.lblWiserbit.Text = "A WISERBIT PRODUCT";
      this.lblDescription.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 177);
      this.lblDescription.Location = new Point(21, 50);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new Size(336, 67);
      this.lblDescription.TabIndex = 37;
      this.lblDescription.Text = "Description";
      this.lblTitle.AutoSize = true;
      this.lblTitle.BackColor = Color.FromArgb(64, 64, 64);
      this.lblTitle.Font = new Font("Verdana", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTitle.ForeColor = SystemColors.ControlLightLight;
      this.lblTitle.Location = new Point(21, 9);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(46, 18);
      this.lblTitle.TabIndex = 36;
      this.lblTitle.Text = "Title";
      this.PictureBox1.BackColor = Color.FromArgb(64, 64, 64);
      this.PictureBox1.Location = new Point(-5, -12);
      this.PictureBox1.Name = "PictureBox1";
      this.PictureBox1.Size = new Size(521, 49);
      this.PictureBox1.TabIndex = 35;
      this.PictureBox1.TabStop = false;
      this.txtPurchaseLink.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtPurchaseLink.Location = new Point(24, 120);
      this.txtPurchaseLink.Multiline = true;
      this.txtPurchaseLink.Name = "txtPurchaseLink";
      this.txtPurchaseLink.Size = new Size(463, 37);
      this.txtPurchaseLink.TabIndex = 34;
      this.txtPurchaseLink.Text = "PURCHASE URL";
      this.txtPurchaseLink.Visible = false;
      this.lnkCompany.AutoSize = true;
      this.lnkCompany.Font = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lnkCompany.Location = new Point(21, 177);
      this.lnkCompany.Name = "lnkCompany";
      this.lnkCompany.Size = new Size(31, 13);
      this.lnkCompany.TabIndex = 33;
      this.lnkCompany.TabStop = true;
      this.lnkCompany.Text = "URL";
      this.lnkCompany.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkCompany_LinkClicked);
      this.cmdContinueTry.Location = new Point(24, 120);
      this.cmdContinueTry.Name = "cmdContinueTry";
      this.cmdContinueTry.Size = new Size(169, 34);
      this.cmdContinueTry.TabIndex = 32;
      this.cmdContinueTry.Text = "Continue try";
      this.cmdContinueTry.UseVisualStyleBackColor = true;
      this.cmdContinueTry.Click += new EventHandler(this.cmdContinueTry_Click);
      this.cmdGetRegKey.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.cmdGetRegKey.Location = new Point(199, 120);
      this.cmdGetRegKey.Name = "cmdGetRegKey";
      this.cmdGetRegKey.Size = new Size(291, 34);
      this.cmdGetRegKey.TabIndex = 31;
      this.cmdGetRegKey.Text = "Order a licenese online";
      this.cmdGetRegKey.UseVisualStyleBackColor = true;
      this.cmdGetRegKey.Click += new EventHandler(this.cmdGetRegKey_Click);
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.Location = new Point(363, 41);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(138, 133);
      this.pictureBox2.TabIndex = 39;
      this.pictureBox2.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(513, 205);
      this.Controls.Add((Control) this.lblTitle);
      this.Controls.Add((Control) this.cmdGetRegKey);
      this.Controls.Add((Control) this.lblWiserbit);
      this.Controls.Add((Control) this.lblDescription);
      this.Controls.Add((Control) this.lnkCompany);
      this.Controls.Add((Control) this.cmdContinueTry);
      this.Controls.Add((Control) this.txtPurchaseLink);
      this.Controls.Add((Control) this.PictureBox1);
      this.Controls.Add((Control) this.pictureBox2);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmLicense);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "License";
      this.Load += new EventHandler(this.frmLicense_Load);
      ((ISupportInitialize) this.PictureBox1).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
