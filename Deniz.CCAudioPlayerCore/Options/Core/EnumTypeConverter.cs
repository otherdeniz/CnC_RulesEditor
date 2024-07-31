// Decompiled with JetBrains decompiler
// Type: Options.Core.EnumTypeConverter
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Options.Core
{
  internal class EnumTypeConverter : EnumConverter
  {
    private Type m_EnumType;

    public EnumTypeConverter(Type type)
      : base(type)
    {
      this.m_EnumType = type;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destType) => destType == typeof (string);

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destType)
    {
      DescriptionAttribute customAttribute = (DescriptionAttribute) Attribute.GetCustomAttribute((MemberInfo) this.m_EnumType.GetField(Enum.GetName(this.m_EnumType, value)), typeof (DescriptionAttribute));
      return customAttribute != null ? (object) customAttribute.Description : (object) value.ToString();
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType) => srcType == typeof (string);

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      foreach (FieldInfo field in this.m_EnumType.GetFields())
      {
        DescriptionAttribute customAttribute = (DescriptionAttribute) Attribute.GetCustomAttribute((MemberInfo) field, typeof (DescriptionAttribute));
        if (customAttribute != null && (string) value == customAttribute.Description)
          return Enum.Parse(this.m_EnumType, field.Name);
      }
      return Enum.Parse(this.m_EnumType, (string) value);
    }
  }
}
