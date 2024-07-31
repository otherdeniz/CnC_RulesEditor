// Decompiled with JetBrains decompiler
// Type: AudioCDReaderLib.Msf
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;

namespace AudioCDReaderLib
{
  internal class Msf : IDisposable
  {
    public const int SamplesPerFrame = 588;
    public const int FramesPerSecond = 75;
    public const int SecondsPerMinute = 60;
    private byte m_m;
    private byte m_s;
    private byte m_f;
    private bool disposedValue = false;

    public Msf()
    {
    }

    public Msf(int lba) => Msf.SetLBA(this, lba);

    public Msf(byte mins, byte secs, byte frms)
    {
      this.M = mins;
      this.S = secs;
      this.F = frms;
    }

    private static void SetLBA(Msf clsMSF, int lba)
    {
      int num = lba < -150 ? 450150 : 150;
      Msf msf = clsMSF;
      msf.M = Convert.ToByte((int) ((double) (lba + num) / 4500.0));
      msf.S = Convert.ToByte((int) ((double) (lba + num - (int) msf.M * 60 * 75) / 75.0));
      msf.F = Convert.ToByte(lba + num - (int) msf.M * 60 * 75 - (int) msf.S * 75);
    }

    public byte M
    {
      get => this.m_m;
      set => this.m_m = value;
    }

    public byte S
    {
      get => this.m_s;
      set => this.m_s = value;
    }

    public byte F
    {
      get => this.m_f;
      set => this.m_f = value;
    }

    public Msf Parse(string strMSF)
    {
      string str1 = "";
      string str2 = "";
      string str3 = "";
      int index1;
      for (index1 = 0; index1 <= strMSF.Length - 1 && char.IsDigit(strMSF[index1]); ++index1)
        str1 += strMSF[index1].ToString();
      int index2;
      for (index2 = index1 + 1; index2 <= strMSF.Length - 1 && char.IsDigit(strMSF[index2]); ++index2)
        str2 += strMSF[index2].ToString();
      for (int index3 = index2 + 1; index3 <= strMSF.Length - 1 && char.IsDigit(strMSF[index3]); ++index3)
        str3 += strMSF[index3].ToString();
      byte mins;
      byte secs;
      byte frms;
      try
      {
        mins = Convert.ToByte(str1);
        secs = Convert.ToByte(str2);
        frms = Convert.ToByte(str3);
      }
      catch (Exception ex)
      {
        throw new FormatException("Ungültiges MSF Format!");
      }
      return Msf.FromMSF(mins, secs, frms);
    }

    public static Msf FromMSF(byte mins, byte secs, byte frms)
    {
      Msf msf = new Msf(mins, secs, frms);
      return new Msf(mins, secs, frms);
    }

    public static Msf FromLBA(int lba) => new Msf(lba);

    public int ToLBA() => Convert.ToInt32(this.M) * 60 * 75 + Convert.ToInt32(this.S) * 75 + Convert.ToInt32(this.F) - 150;

    public override string ToString() => string.Format("M", (object) "00") + ":" + string.Format("S", (object) "00") + "." + string.Format("F", (object) "00");

    public override bool Equals(object obj)
    {
      if (obj == null || obj != (object) typeof (Msf))
        return false;
      Msf msf = (Msf) obj;
      return (int) msf.M == (int) this.M & (int) msf.S == (int) this.S & (int) msf.F == (int) this.F;
    }

    public static string operator +(string strValue, Msf msf1) => strValue + msf1.ToString();

    public static string operator +(Msf msf1, string strValue) => msf1.ToString() + strValue;

    public static implicit operator int(Msf msf1) => msf1.ToLBA();

    public static implicit operator string(Msf msf1) => msf1.ToString();

    public static implicit operator Msf(int value) => new Msf(value);

    public static bool operator ==(Msf msf1, Msf msf2) => (int) msf1.M == (int) msf2.M && (int) msf1.S == (int) msf2.S && (int) msf1.F == (int) msf2.F;

    public static bool operator !=(Msf msf1, Msf msf2) => (int) msf1.M != (int) msf2.M || (int) msf1.S != (int) msf2.S || (int) msf1.F != (int) msf2.F;

    public static bool operator <(Msf msf1, Msf msf2) => msf1.ToLBA() < msf2.ToLBA();

    public static bool operator >(Msf msf1, Msf msf2) => msf1.ToLBA() > msf2.ToLBA();

    public static bool operator <=(Msf msf1, Msf msf2) => msf1 < msf2 | msf1 == msf2;

    public static bool operator >=(Msf msf1, Msf msf2) => msf1 > msf2 | msf1 == msf2;

    public static Msf operator +(Msf msf1, int lba) => Msf.FromLBA(msf1.ToLBA() + lba);

    public static Msf operator +(int lba, Msf msf2) => Msf.FromLBA(lba + msf2.ToLBA());

    public static Msf operator +(Msf msf1, Msf msf2) => Msf.FromLBA(msf1.ToLBA() + msf2.ToLBA());

    public static Msf operator -(Msf msf1, int lba) => Msf.FromLBA(msf1.ToLBA() - lba);

    public static Msf operator -(int lba, Msf msf2) => Msf.FromLBA(lba - msf2.ToLBA());

    public static Msf operator -(Msf msf1, Msf msf2) => Msf.FromLBA(msf1.ToLBA() - msf2.ToLBA());

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue || !disposing)
        ;
      this.disposedValue = true;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }
  }
}
