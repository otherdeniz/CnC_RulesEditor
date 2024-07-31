// Decompiled with JetBrains decompiler
// Type: Options.Core.PathSelectorTypeEditor
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Options.Core
{
  internal class PathSelectorTypeEditor : UITypeEditor
  {
    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return context == null || context.Instance == null ? base.GetEditStyle(context) : UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      IServiceProvider provider,
      object value)
    {
      if (context == null || context.Instance == null || provider == null)
        return value;
      try
      {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.SelectedPath = "D:\\audio\\";
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
        {
          int num = (int) MessageBox.Show("Files found: " + Directory.GetFiles(folderBrowserDialog.SelectedPath).Length.ToString(), "Message");
        }
        return (object) folderBrowserDialog.SelectedPath;
      }
      finally
      {
      }
    }
  }
}
