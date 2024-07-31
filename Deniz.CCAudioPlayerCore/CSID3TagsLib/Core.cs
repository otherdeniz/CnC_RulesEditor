// Decompiled with JetBrains decompiler
// Type: CSID3TagsLib.Core
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace CSID3TagsLib
{
  public class Core
  {
    public static DialogResult InputBox(
      string title,
      string promptText,
      ref string value)
    {
      Form form = new Form();
      Label label = new Label();
      TextBox textBox = new TextBox();
      Button button1 = new Button();
      Button button2 = new Button();
      form.Text = title;
      label.Text = promptText;
      textBox.Text = value;
      button1.Text = "OK";
      button2.Text = "Cancel";
      button1.DialogResult = DialogResult.OK;
      button2.DialogResult = DialogResult.Cancel;
      label.SetBounds(9, 20, 372, 13);
      textBox.SetBounds(12, 36, 372, 20);
      button1.SetBounds(228, 72, 75, 23);
      button2.SetBounds(309, 72, 75, 23);
      label.AutoSize = true;
      textBox.Anchor |= AnchorStyles.Right;
      button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      form.ClientSize = new Size(396, 107);
      form.Controls.AddRange(new Control[4]
      {
        (Control) label,
        (Control) textBox,
        (Control) button1,
        (Control) button2
      });
      form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
      form.FormBorderStyle = FormBorderStyle.FixedDialog;
      form.StartPosition = FormStartPosition.CenterScreen;
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.AcceptButton = (IButtonControl) button1;
      form.CancelButton = (IButtonControl) button2;
      DialogResult dialogResult = form.ShowDialog();
      value = textBox.Text;
      return dialogResult;
    }

    public static List<T> Switch<T>(List<T> array, int index1, int index2)
    {
      T obj = array[index1];
      array[index1] = array[index2];
      array[index2] = obj;
      return array;
    }

    public static string GetMineType(Image image)
    {
      ImageFormat format = image.RawFormat;
      return ((IEnumerable<ImageCodecInfo>) ImageCodecInfo.GetImageDecoders()).First<ImageCodecInfo>((Func<ImageCodecInfo, bool>) (c => c.FormatID == format.Guid)).MimeType;
    }
  }
}
