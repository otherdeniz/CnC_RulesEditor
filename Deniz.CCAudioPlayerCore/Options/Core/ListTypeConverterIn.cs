// Decompiled with JetBrains decompiler
// Type: Options.Core.ListTypeConverterIn
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore.CoreAudioAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Options.Core
{
  internal class ListTypeConverterIn : TypeConverter
  {
    private List<string> m_list = new List<string>();

    public ListTypeConverterIn()
    {
      MMDeviceCollection source = MMDeviceEnumerator.EnumerateDevices(true ? DataFlow.Capture : DataFlow.Render, DeviceState.Active);
      if (!source.Any<MMDevice>())
      {
        Console.WriteLine("No devices found.");
      }
      else
      {
        for (int index = 0; index < source.Count; ++index)
          this.m_list.Add(source[index].FriendlyName);
      }
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

    private TypeConverter.StandardValuesCollection GetValues() => new TypeConverter.StandardValuesCollection((ICollection) this.m_list);

    protected void SetList(List<string> list) => this.m_list = list;

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      return this.GetValues();
    }
  }
}
