// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.Properties.Resources
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace CSAudioPlayer.Properties
{
  [CompilerGenerated]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (CSAudioPlayer.Properties.Resources.resourceMan == null)
          CSAudioPlayer.Properties.Resources.resourceMan = new ResourceManager("CSAudioPlayer.Properties.Resources", typeof (CSAudioPlayer.Properties.Resources).Assembly);
        return CSAudioPlayer.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => CSAudioPlayer.Properties.Resources.resourceCulture;
      set => CSAudioPlayer.Properties.Resources.resourceCulture = value;
    }

    internal static string Formats => CSAudioPlayer.Properties.Resources.ResourceManager.GetString(nameof (Formats), CSAudioPlayer.Properties.Resources.resourceCulture);
  }
}
