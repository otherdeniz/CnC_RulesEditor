// Decompiled with JetBrains decompiler
// Type: CSAudioPlayer.cSilentPlayer
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

namespace CSAudioPlayer
{
  internal class cSilentPlayer
  {
    public static MMDevice GetDefaultRenderDevice()
    {
      using (MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator())
        return deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
    }

    public static bool IsAudioPlaying(MMDevice device)
    {
      using (AudioMeterInformation meterInformation = AudioMeterInformation.FromDevice(device))
        return (double) meterInformation.PeakValue > 0.0;
    }

    public void Play()
    {
      IWaveSource source = (IWaveSource) new SilenceGenerator();
      MMDevice enumerateDevice = MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active)[1];
      using (WasapiOut wasapiOut = new WasapiOut()
      {
        Latency = 100,
        Device = enumerateDevice
      })
      {
        wasapiOut.Initialize(source);
        wasapiOut.Play();
      }
    }
  }
}
