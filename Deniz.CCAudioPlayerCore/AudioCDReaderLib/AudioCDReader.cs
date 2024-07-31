// Decompiled with JetBrains decompiler
// Type: AudioCDReaderLib.AudioCDReader
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using CSCore;
using CSCore.Codecs.RAW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AudioCDReaderLib
{
  internal class AudioCDReader : IWaveSource, IReadableAudioSource<byte>, IAudioSource, IDisposable
  {
    private readonly WaveFormat _waveFormat = new WaveFormat(44100, 16, 2);
    private static Msf msfPos = (Msf) 0;
    private Spti.ScsiStatus udeStatus;
    private Spti _m_clsDevice = new Spti();
    private List<Commands.ScsiTocTrack> _m_lstTOC = new List<Commands.ScsiTocTrack>();
    private List<AudioCDReader.TrackInfo> m_lstTags = new List<AudioCDReader.TrackInfo>();
    public List<AudioCDReader.TrackInfo> TracksInfo = new List<AudioCDReader.TrackInfo>();
    private Msf msfEnd;
    private Msf msfStart;
    private byte[] _buffers_storage = new byte[0];
    private bool _fill_more_buffers = true;

    public WaveFormat WaveFormat { get; private set; } = new WaveFormat(44100, 16, 2);

    private int _TrackIndex { get; set; } = -1;

    private int _DriveIndex { get; set; } = -1;

    public AudioCDReader(int DriveIndex, int TrackIndex)
    {
      try
      {
        this._TrackIndex = TrackIndex;
        this._DriveIndex = DriveIndex;
        this.LoadTracks(this._DriveIndex);
        this.msfStart = this._m_lstTOC[TrackIndex].Start;
        this.msfEnd = this._m_lstTOC[TrackIndex].End;
        AudioCDReader.msfPos = this.msfStart;
        this._fill_more_buffers = true;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public AudioCDReader(string FileName)
    {
      try
      {
        int DriveIndex = -1;
        int num = 0;
        foreach (DriveInfo driveInfo in ((IEnumerable<DriveInfo>) DriveInfo.GetDrives()).Where<DriveInfo>((Func<DriveInfo, bool>) (d => d.DriveType == DriveType.CDRom)))
        {
          Console.WriteLine("Path.GetPathRoot(file)");
          Console.WriteLine(Path.GetPathRoot(FileName));
          if (Path.GetPathRoot(driveInfo.Name) == Path.GetPathRoot(FileName))
            DriveIndex = num;
          ++num;
        }
        this._TrackIndex = int.Parse(Path.GetFileNameWithoutExtension(Regex.Match(FileName, "\\d+").Value)) - 1;
        this._DriveIndex = DriveIndex;
        this.LoadTracks(DriveIndex);
        this.msfStart = this._m_lstTOC[this._TrackIndex].Start;
        this.msfEnd = this._m_lstTOC[this._TrackIndex].End;
        AudioCDReader.msfPos = this.msfStart;
        this._fill_more_buffers = true;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public bool CDIsReady(int DriveIndex)
    {
      try
      {
        if (!this._m_clsDevice.SelectDevice(DriveIndex))
          throw new Exception("The CD drive is not ready.");
        if (Commands.TestUnitReady(this._m_clsDevice) == 0)
          return true;
        if (this._m_clsDevice.LastSenseKey != Spti.SenseKey.NotReady)
          throw new Exception("Drive is not ready.");
        if (this._m_clsDevice.LastAddSenseCode == (byte) 58)
          throw new Exception("No media in the drive.");
        throw new Exception("Drive is not ready.");
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public bool EjectCD(int DeviceIndex)
    {
      try
      {
        return this._m_clsDevice.EjectCD(DeviceIndex);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public bool CloseCD(int DeviceIndex)
    {
      try
      {
        return this._m_clsDevice.CloseCD(DeviceIndex);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public List<string> GetDevices()
    {
      try
      {
        List<string> devices = new List<string>();
        Commands.ScsiInquiryResult result = new Commands.ScsiInquiryResult();
        if (this._m_clsDevice.DeviceCount == 0)
          throw new Exception("No drives found.");
        for (int index = 0; index <= this._m_clsDevice.DeviceCount - 1; ++index)
        {
          this._m_clsDevice.SelectDevice(index);
          string str1;
          if (Commands.Inquiry(this._m_clsDevice, ref result) == Spti.ScsiStatus.Good)
          {
            Commands.ScsiInquiryResult scsiInquiryResult = result;
            str1 = scsiInquiryResult.Vendor + " " + scsiInquiryResult.Product + " " + scsiInquiryResult.Revision;
          }
          else
            str1 = this._m_clsDevice.DeviceName(index);
          Spti.BusPosition busPosition = this._m_clsDevice.DeviceAddress(index);
          string[] strArray = new string[10];
          strArray[0] = str1;
          strArray[1] = " [";
          byte num = busPosition.PortID;
          strArray[2] = num.ToString();
          strArray[3] = ":";
          num = busPosition.HostAdapter;
          strArray[4] = num.ToString();
          strArray[5] = ":";
          num = busPosition.Target;
          strArray[6] = num.ToString();
          strArray[7] = ":";
          num = busPosition.LogicalUnitNumber;
          strArray[8] = num.ToString();
          strArray[9] = "]";
          string str2 = string.Concat(strArray);
          devices.Add(str2);
        }
        return devices;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public bool LoadTracks(int DriveIndex)
    {
      try
      {
        byte[] Data = (byte[]) null;
        this.TracksInfo.Clear();
        AudioCDReader.TrackInfo trackInfo = new AudioCDReader.TrackInfo();
        this.m_lstTags.Clear();
        this._m_lstTOC.Clear();
        if (!this._m_clsDevice.SelectDevice(DriveIndex))
          throw new Exception("Could not select device.");
        if (Commands.TestUnitReady(this._m_clsDevice) != 0)
        {
          if (this._m_clsDevice.LastSenseKey != Spti.SenseKey.NotReady)
            throw new Exception("Drive is not ready.");
          if (this._m_clsDevice.LastAddSenseCode == (byte) 58)
            throw new Exception("No media in the drive.");
          throw new Exception("Drive is not ready.");
        }
        Commands.TocFormat TocFmt = Commands.TocFormat.FullToc;
        if (Commands.ReadTOC(this._m_clsDevice, Commands.TimeFormat.Msf, TocFmt, (byte) 0, ref Data) != 0)
        {
          TocFmt = Commands.TocFormat.FormatedToc;
          if (Commands.ReadTOC(this._m_clsDevice, Commands.TimeFormat.Msf, TocFmt, (byte) 0, ref Data) != 0)
            throw new Exception("Could not read table of contents.");
        }
        switch (TocFmt)
        {
          case Commands.TocFormat.FormatedToc:
            this._m_lstTOC = Commands.TocGetFormated(this._m_clsDevice, Data, Commands.TimeFormat.Msf);
            break;
          case Commands.TocFormat.FullToc:
            this._m_lstTOC = Commands.TocGetFull(this._m_clsDevice, Data);
            break;
        }
        return true;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public AudioCDReader.TrackInfo[] GetTracks(int DriveIndex)
    {
      try
      {
        this.TracksInfo.Clear();
        AudioCDReader.TrackInfo trackInfo = new AudioCDReader.TrackInfo();
        this.m_lstTags.Clear();
        this._m_lstTOC.Clear();
        for (int index = 0; index <= this._m_lstTOC.Count - 1; ++index)
        {
          trackInfo.TrackNumber = this._m_lstTOC[index].Track;
          int num1 = (int) this._m_lstTOC[index].Start.M * 60 * 1000 + (int) this._m_lstTOC[index].Start.S * 1000 + 10 * (int) this._m_lstTOC[index].Start.F;
          int num2 = (int) this._m_lstTOC[index].End.M * 60 * 1000 + (int) this._m_lstTOC[index].End.S * 1000 + 10 * (int) this._m_lstTOC[index].End.F;
          trackInfo.TrackLength = (long) (num2 - num1);
          trackInfo.TrackType = this._m_lstTOC[index].Type != Commands.TrackType.Data ? this._m_lstTOC[index].Type.ToString() : this._m_lstTOC[index].Type.ToString() + " [" + this._m_lstTOC[index].Mode.ToString() + "]";
          this.TracksInfo.Add(trackInfo);
        }
        return this.TracksInfo.ToArray();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    private int FillBuffers(int buffer_size)
    {
      try
      {
        GCHandle gcHandle1 = new GCHandle();
        if (AudioCDReader.msfPos.M == (byte) 0 && AudioCDReader.msfPos.S == (byte) 0 && AudioCDReader.msfPos.F == (byte) 0)
        {
          this.msfStart = this._m_lstTOC[this._TrackIndex].Start;
          AudioCDReader.msfPos = this.msfStart;
        }
        int num = this._m_clsDevice.DeviceMaximumTransferLength(this._m_clsDevice.SelectedDevice) / 2352;
        short[] src = new short[num * 2352 / 2];
        GCHandle gcHandle2 = GCHandle.Alloc((object) src, GCHandleType.Pinned);
        int Sectors = !(AudioCDReader.msfPos + num > this.msfEnd) ? num : (int) (this.msfEnd - AudioCDReader.msfPos);
        this.udeStatus = Commands.ReadCD(this._m_clsDevice, (int) AudioCDReader.msfPos, Sectors, Commands.ReadCDFlags.Raw, gcHandle2.AddrOfPinnedObject(), num * 2352);
        if (this.udeStatus != 0)
          return 0;
        byte[] numArray1 = new byte[src.Length * 2];
        Buffer.BlockCopy((Array) src, 0, (Array) numArray1, 0, numArray1.Length);
        IWaveSource waveSource = (IWaveSource) new RawDataReader((Stream) new MemoryStream(numArray1), new WaveFormat(44100, 16, 2, AudioEncoding.Pcm));
        byte[] numArray2 = new byte[num * 2352];
        int length;
        while ((length = waveSource.Read(numArray2, 0, numArray2.Length)) > 0)
        {
          byte[] numArray3 = new byte[length];
          Array.Copy((Array) numArray2, (Array) numArray3, length);
          this._buffers_storage = ((IEnumerable<byte>) this._buffers_storage).Concat<byte>((IEnumerable<byte>) numArray3).ToArray<byte>();
        }
        gcHandle2.Free();
        waveSource.Dispose();
        AudioCDReader.msfPos += Sectors;
        return AudioCDReader.msfPos >= this.msfEnd ? 0 : this._buffers_storage.Length;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      try
      {
        if (this._fill_more_buffers && this._buffers_storage.Length < count * 16)
        {
          while (this._buffers_storage.Length < 200000 && this._fill_more_buffers)
          {
            if (this.FillBuffers(10000) == 0)
              this._fill_more_buffers = false;
          }
        }
        Array.Copy((Array) this._buffers_storage, 0, (Array) buffer, 0, this._buffers_storage.Length >= count ? count : this._buffers_storage.Length);
        Array.Resize<byte>(ref buffer, count);
        this._buffers_storage = ((IEnumerable<byte>) this._buffers_storage).Skip<byte>(count).ToArray<byte>();
        if (this._buffers_storage.Length != 0)
          return buffer.Length;
        this._buffers_storage = (byte[]) null;
        return 0;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
      }
      finally
      {
      }
    }

    public long Position
    {
      get
      {
        try
        {
          int position = ((int) AudioCDReader.msfPos.M * 60 * 1000 + (int) AudioCDReader.msfPos.S * 1000 + 10 * (int) AudioCDReader.msfPos.F - ((int) this.msfStart.M * 60 * 1000 + (int) this.msfStart.S * 1000 + 10 * (int) this.msfStart.F)) / 1000 * this._waveFormat.BytesPerSecond;
          if (position < 0)
            position = 0;
          return (long) position;
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
        }
        finally
        {
        }
      }
      set
      {
        try
        {
          TimeSpan timeSpan = TimeSpan.FromSeconds((double) value / (double) this._waveFormat.BytesPerSecond);
          byte mins = byte.Parse(timeSpan.ToString("mm"));
          timeSpan = TimeSpan.FromSeconds((double) value / (double) this._waveFormat.BytesPerSecond);
          byte secs = byte.Parse(timeSpan.ToString("ss"));
          timeSpan = TimeSpan.FromSeconds((double) value / (double) this._waveFormat.BytesPerSecond);
          byte frms = byte.Parse(timeSpan.ToString("ms"));
          Array.Resize<byte>(ref this._buffers_storage, 0);
          this._fill_more_buffers = true;
          AudioCDReader.msfPos = this._m_lstTOC[this._TrackIndex].Start + Msf.FromMSF(mins, secs, frms);
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
        }
        finally
        {
        }
      }
    }

    public long Length
    {
      get
      {
        try
        {
          return (long) ((int) this._m_lstTOC[this._TrackIndex].End.M * 60 * 1000 + (int) this._m_lstTOC[this._TrackIndex].End.S * 1000 + 10 * (int) this._m_lstTOC[this._TrackIndex].End.F - ((int) this._m_lstTOC[this._TrackIndex].Start.M * 60 * 1000 + (int) this._m_lstTOC[this._TrackIndex].Start.S * 1000 + 10 * (int) this._m_lstTOC[this._TrackIndex].Start.F));
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message + "(" + ex.HResult.ToString() + ")");
        }
        finally
        {
        }
      }
    }

    public TimeSpan GetLength() => TimeSpan.FromSeconds((double) (this.Length / (long) this._waveFormat.BytesPerSecond));

    public bool CanSeek => true;

    public void Dispose()
    {
      Msf msf = (Msf) 0;
      this._m_lstTOC = (List<Commands.ScsiTocTrack>) null;
      this.m_lstTags = (List<AudioCDReader.TrackInfo>) null;
      this.TracksInfo = (List<AudioCDReader.TrackInfo>) null;
      this.msfEnd = (Msf) 0;
      this.msfStart = (Msf) 0;
      this._buffers_storage = (byte[]) null;
    }

    public struct TrackInfo
    {
      public int TrackNumber;
      public long TrackBytes;
      public long TrackLength;
      public string TrackType;
    }
  }
}
