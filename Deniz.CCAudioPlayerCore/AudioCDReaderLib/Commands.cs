// Decompiled with JetBrains decompiler
// Type: AudioCDReaderLib.Commands
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AudioCDReaderLib
{
  internal class Commands
  {
    public static Spti.ScsiStatus TestUnitReady(Spti device) => device.ExecuteCommand(new byte[6], (byte) 6, Spti.DataDirection.unspecified, IntPtr.Zero, 0, 60U);

    public static Spti.ScsiStatus Inquiry(Spti device, ref Commands.ScsiInquiryResult result)
    {
      Commands.ScsiInquiry structure = new Commands.ScsiInquiry();
      Commands.ScsiInquiryResult scsiInquiryResult = new Commands.ScsiInquiryResult();
      byte[] cdb = new byte[6];
      int num1 = Marshal.SizeOf<Commands.ScsiInquiry>(structure);
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.Inquiry));
      Commands.PutCDB8(ref cdb, 4, Convert.ToByte(num1));
      IntPtr num2 = Marshal.AllocHGlobal(num1);
      Spti.ScsiStatus scsiStatus = device.ExecuteCommand(cdb, (byte) 6, Spti.DataDirection.@in, num2, num1, 10U);
      Marshal.PtrToStructure<Commands.ScsiInquiry>(num2, structure);
      Marshal.FreeHGlobal(num2);
      scsiInquiryResult.Vendor = new string(structure.vendor);
      scsiInquiryResult.Product = new string(structure.product);
      scsiInquiryResult.Revision = new string(structure.revision);
      result = scsiInquiryResult;
      return scsiStatus;
    }

    public static Spti.ScsiStatus LoadUnload(
      Spti device,
      Commands.LoUnloadAction action,
      bool immed = false)
    {
      byte[] cdb = new byte[12];
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.LoadUnload));
      Commands.PutCDB8(ref cdb, 1, Convert.ToByte(-Convert.ToInt32(immed)));
      if (action == Commands.LoUnloadAction.Load)
        Commands.PutCDB8(ref cdb, 4, (byte) 2);
      else
        Commands.PutCDB8(ref cdb, 4, (byte) 3);
      return device.ExecuteCommand(cdb, (byte) 12, Spti.DataDirection.unspecified, IntPtr.Zero, 0, 20U);
    }

    public static Spti.ScsiStatus StartStopUnit(
      Spti device,
      Commands.PowerCondition condition,
      Commands.LoUnloadAction action,
      bool immed = false)
    {
      byte[] cdb = new byte[6];
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.StartStopUnit));
      Commands.PutCDB8(ref cdb, 1, Convert.ToByte(-Convert.ToInt32(immed)));
      if (action == Commands.LoUnloadAction.Unload)
        Commands.PutCDB8(ref cdb, 4, (byte) 2);
      else
        Commands.PutCDB8(ref cdb, 4, (byte) 3);
      Commands.AddCDB8(ref cdb, 4, Convert.ToByte((int) condition << 4));
      return device.ExecuteCommand(cdb, (byte) 6, Spti.DataDirection.unspecified, IntPtr.Zero, 0, 20U);
    }

    public static Spti.ScsiStatus PreventAllowMediumRemoval(
      Spti device,
      Commands.PreventRemovalAction @lock)
    {
      byte[] cdb = new byte[6];
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.PreventMediumRemoval));
      Commands.PutCDB8(ref cdb, 4, Convert.ToByte((object) @lock));
      return device.ExecuteCommand(cdb, (byte) 6, Spti.DataDirection.unspecified, IntPtr.Zero, 0);
    }

    public static Spti.ScsiStatus ReadTOC(
      Spti Device,
      Commands.TimeFormat Time,
      Commands.TocFormat TocFmt,
      byte TrackSessionNr,
      ref byte[] Data)
    {
      byte[] cdb = new byte[10];
      byte[] numArray = new byte[16];
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.ReadTOC));
      Commands.PutCDB8(ref cdb, 1, Convert.ToByte((object) Time));
      Commands.PutCDB8(ref cdb, 2, Convert.ToByte((object) TocFmt));
      Commands.PutCDB8(ref cdb, 6, TrackSessionNr);
      Commands.PutCDB16(ref cdb, 7, (short) 16);
      IntPtr num = Marshal.AllocHGlobal(16);
      Spti.ScsiStatus scsiStatus1 = Device.ExecuteCommand(cdb, (byte) 10, Spti.DataDirection.@in, num, 16, 10U);
      if (scsiStatus1 != 0)
      {
        Marshal.FreeHGlobal(num);
        return scsiStatus1;
      }
      short int16 = Convert.ToInt16((int) Convert.ToInt16(Marshal.ReadByte(num, 0)) << 8 | (int) Convert.ToInt16(Marshal.ReadByte(num, 1)) + 2);
      Marshal.FreeHGlobal(num);
      if (int16 <= (short) 0)
        return Spti.ScsiStatus.CheckCondition;
      Data = new byte[(int) int16 - 1 + 1];
      Commands.PutCDB16(ref cdb, 7, int16);
      GCHandle gcHandle = GCHandle.Alloc((object) Data, GCHandleType.Pinned);
      Spti.ScsiStatus scsiStatus2 = Device.ExecuteCommand(cdb, (byte) 10, Spti.DataDirection.@in, gcHandle.AddrOfPinnedObject(), (int) int16, 30U);
      if (scsiStatus2 != 0)
        return scsiStatus2;
      gcHandle.Free();
      return scsiStatus2;
    }

    public static Spti.ScsiStatus ReadTrackInformation(
      Spti Device,
      byte track,
      ref Commands.ScsiTrackInformation info)
    {
      byte[] cdb = new byte[10];
      short int16 = Convert.ToInt16(Marshal.SizeOf(typeof (Commands.ScsiTrackInformation)));
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.ReadTrackInformation));
      Commands.PutCDB8(ref cdb, 1, (byte) 1);
      Commands.PutCDB32(ref cdb, 2, (int) track);
      Commands.PutCDB16(ref cdb, 7, int16);
      IntPtr num = Marshal.AllocHGlobal((int) int16);
      Spti.ScsiStatus scsiStatus = Device.ExecuteCommand(cdb, (byte) 10, Spti.DataDirection.@in, num, (int) int16);
      Marshal.PtrToStructure<Commands.ScsiTrackInformation>(num, info);
      Marshal.FreeHGlobal(num);
      info.TrackStartAddress = Commands.ReverseI32(info.TrackStartAddress);
      info.NextWritableAddress = Commands.ReverseI32(info.NextWritableAddress);
      info.FreeBlocks = Commands.ReverseI32(info.FreeBlocks);
      info.BlockingFactor = Commands.ReverseI32(info.BlockingFactor);
      info.TrackSize = Commands.ReverseI32(info.TrackSize);
      info.LastRecordedAddress = Commands.ReverseI32(info.LastRecordedAddress);
      return scsiStatus;
    }

    public static Spti.ScsiStatus SetCDSpeed(
      Spti Device,
      Commands.RotationControl rotation,
      short ReadSpeed,
      short WriteSpeed)
    {
      byte[] cdb = new byte[12];
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.SetSpeed));
      Commands.PutCDB8(ref cdb, 1, Convert.ToByte((object) rotation));
      Commands.PutCDB16(ref cdb, 2, ReadSpeed);
      Commands.PutCDB16(ref cdb, 4, WriteSpeed);
      return Device.ExecuteCommand(cdb, (byte) 12, Spti.DataDirection.unspecified, IntPtr.Zero, 0);
    }

    public static Spti.ScsiStatus ReadCD(
      Spti Device,
      int StartLBA,
      int Sectors,
      Commands.ReadCDFlags Flags,
      IntPtr pBuffer,
      int BufferLen)
    {
      byte[] cdb = new byte[12];
      Commands.PutCDB8(ref cdb, 0, Convert.ToByte((object) Spti.ScsiCommand.ReadCD));
      Commands.PutCDB32(ref cdb, 2, StartLBA);
      Commands.PutCDB24(ref cdb, 6, Sectors);
      Commands.PutCDB8(ref cdb, 9, Convert.ToByte((object) Flags));
      return Device.ExecuteCommand(cdb, (byte) 12, Spti.DataDirection.@in, pBuffer, BufferLen, 30U);
    }

    public static List<Commands.ScsiTocTrack> TocGetFull(Spti Device, byte[] data)
    {
      List<Commands.ScsiTocTrack> full = new List<Commands.ScsiTocTrack>();
      Commands.ScsiFullToc structure1 = new Commands.ScsiFullToc();
      Commands.ScsiFullTocPacket structure2 = new Commands.ScsiFullTocPacket();
      int num = 0;
      Commands.ScsiTocTrack[] scsiTocTrackArray = new Commands.ScsiTocTrack[99];
      GCHandle gcHandle = GCHandle.Alloc((object) data, GCHandleType.Pinned);
      IntPtr ptr = gcHandle.AddrOfPinnedObject();
      Marshal.PtrToStructure<Commands.ScsiFullToc>(ptr, structure1);
      ptr = (IntPtr) ptr.ToInt32() + Marshal.SizeOf<Commands.ScsiFullToc>(structure1);
      short int16 = Convert.ToInt16((int) (short) ((int) structure1.TocLenLo << 8 | (int) structure1.TocLenHi) / Marshal.SizeOf<Commands.ScsiFullTocPacket>(structure2));
      for (int index1 = 0; index1 <= (int) int16 - 1; ++index1)
      {
        Marshal.PtrToStructure<Commands.ScsiFullTocPacket>(ptr, structure2);
        ptr = (IntPtr) ptr.ToInt32() + Marshal.SizeOf<Commands.ScsiFullTocPacket>(structure2);
        byte point = structure2.Point;
        if ((byte) 1 > structure2.Point || structure2.Point > (byte) 99)
        {
          switch (point)
          {
            case 160:
            case 176:
            case 177:
              continue;
            case 161:
              if (num < (int) structure2.PMin)
              {
                for (int index2 = num; index2 <= (int) structure2.PMin; ++index2)
                  scsiTocTrackArray[index2] = new Commands.ScsiTocTrack();
                num = (int) structure2.PMin;
                continue;
              }
              continue;
            case 162:
              scsiTocTrackArray[num - 1].End = Msf.FromMSF(structure2.PMin, structure2.PSec, structure2.PFrm);
              continue;
            default:
              if ((byte) 178 <= structure2.Point && structure2.Point <= (byte) 180 || point == (byte) 192 || point == (byte) 193)
                continue;
              continue;
          }
        }
        else
        {
          if (scsiTocTrackArray[(int) structure2.Point - 1] == null)
            scsiTocTrackArray[(int) structure2.Point - 1] = new Commands.ScsiTocTrack();
          if (num < (int) structure2.Point)
            num = (int) structure2.Point;
          Commands.ScsiFullTocPacket scsiFullTocPacket = structure2;
          scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Start = Msf.FromMSF(structure2.PMin, structure2.PSec, structure2.PFrm);
          scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Session = (int) scsiFullTocPacket.Session;
          scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Track = (int) scsiFullTocPacket.Point;
          scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].ADR = (Commands.QSubInfo) ((int) structure2.ADR >> 4 & 15);
          scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].CTL = (Commands.QSubControlFlags) ((int) structure2.ADR & 15);
          switch (Convert.ToInt32((object) (scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].CTL & Commands.QSubControlFlags.DigitalCopyPermitted)))
          {
            case 0:
            case 1:
            case 8:
            case 9:
            case 12:
            case 13:
            case 15:
              scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Type = Commands.TrackType.Audio;
              scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Mode = Commands.TrackMode.Audio;
              continue;
            default:
              scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Type = Commands.TrackType.Data;
              Commands.ScsiTrackInformation info = new Commands.ScsiTrackInformation();
              if (Commands.ReadTrackInformation(Device, scsiFullTocPacket.Point, ref info) == Spti.ScsiStatus.Good)
              {
                switch ((int) info.DataMode & 15)
                {
                  case 1:
                    scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Mode = Commands.TrackMode.Mode1;
                    continue;
                  case 2:
                    scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Mode = Commands.TrackMode.Mode2;
                    continue;
                  case 3:
                    scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Mode = Commands.TrackMode.Audio;
                    continue;
                  default:
                    scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Mode = Commands.TrackMode.Unknown;
                    continue;
                }
              }
              else
              {
                scsiTocTrackArray[(int) scsiFullTocPacket.Point - 1].Mode = Commands.TrackMode.Unknown;
                continue;
              }
          }
        }
      }
      for (int index = 1; index <= num - 1; ++index)
      {
        scsiTocTrackArray[index - 1].End = scsiTocTrackArray[index].Start - 1;
        full.Add(scsiTocTrackArray[index - 1]);
      }
      full.Add(scsiTocTrackArray[num - 1]);
      gcHandle.Free();
      return full;
    }

    public static List<Commands.ScsiTocTrack> TocGetFullOLD(Spti Device, byte[] data)
    {
      List<Commands.ScsiTocTrack> fullOld = new List<Commands.ScsiTocTrack>();
      Commands.ScsiFullToc structure1 = new Commands.ScsiFullToc();
      Commands.ScsiFullTocPacket structure2 = new Commands.ScsiFullTocPacket();
      int num = 0;
      Commands.ScsiTocTrack[] scsiTocTrackArray = new Commands.ScsiTocTrack[99];
      GCHandle gcHandle = GCHandle.Alloc((object) data, GCHandleType.Pinned);
      IntPtr ptr = gcHandle.AddrOfPinnedObject();
      Marshal.PtrToStructure<Commands.ScsiFullToc>(ptr, structure1);
      ptr = (IntPtr) (ptr.ToInt32() + Marshal.SizeOf<Commands.ScsiFullToc>(structure1));
      short int16 = Convert.ToInt16((int) (short) ((int) structure1.TocLenLo << 8 | (int) structure1.TocLenHi) / Marshal.SizeOf<Commands.ScsiFullTocPacket>(structure2));
      for (int index1 = 0; index1 <= (int) int16 - 1; ++index1)
      {
        Marshal.PtrToStructure<Commands.ScsiFullTocPacket>(ptr, structure2);
        ptr = (IntPtr) (ptr.ToInt32() + Marshal.SizeOf<Commands.ScsiFullTocPacket>(structure2));
        byte point = structure2.Point;
        if ((byte) 1 > structure2.Point || structure2.Point > (byte) 99)
        {
          switch (point)
          {
            case 160:
            case 176:
            case 177:
              continue;
            case 161:
              if (num < (int) structure2.PMin)
              {
                for (int index2 = num; index2 <= (int) structure2.PMin; ++index2)
                  scsiTocTrackArray[index2] = new Commands.ScsiTocTrack();
                num = (int) structure2.PMin;
                continue;
              }
              continue;
            case 162:
              Commands.ScsiFullTocPacket scsiFullTocPacket1 = structure2;
              scsiTocTrackArray[num - 1].End = Msf.FromMSF(scsiFullTocPacket1.PMin, scsiFullTocPacket1.PSec, scsiFullTocPacket1.PFrm);
              continue;
            default:
              if ((byte) 178 <= structure2.Point && structure2.Point <= (byte) 180 || point == (byte) 192 || point == (byte) 193)
                continue;
              continue;
          }
        }
        else
        {
          if (scsiTocTrackArray[(int) structure2.Point - 1] == null)
            scsiTocTrackArray[(int) structure2.Point - 1] = new Commands.ScsiTocTrack();
          if (num < (int) structure2.Point)
            num = (int) structure2.Point;
          Commands.ScsiFullTocPacket scsiFullTocPacket2 = structure2;
          scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Start = Msf.FromMSF(scsiFullTocPacket2.PMin, scsiFullTocPacket2.PSec, scsiFullTocPacket2.PFrm);
          scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Session = (int) scsiFullTocPacket2.Session;
          scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Track = (int) scsiFullTocPacket2.Point;
          scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].ADR = (Commands.QSubInfo) ((int) scsiFullTocPacket2.ADR >> 4 & 15);
          scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].CTL = (Commands.QSubControlFlags) ((int) scsiFullTocPacket2.ADR & 15);
          switch (Convert.ToInt32((object) (scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].CTL & Commands.QSubControlFlags.DigitalCopyPermitted)))
          {
            case 0:
            case 1:
            case 8:
            case 9:
            case 13:
            case 15:
            case 18:
              scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Type = Commands.TrackType.Audio;
              scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Mode = Commands.TrackMode.Audio;
              continue;
            default:
              scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Type = Commands.TrackType.Data;
              Commands.ScsiTrackInformation info = new Commands.ScsiTrackInformation();
              if (Commands.ReadTrackInformation(Device, scsiFullTocPacket2.Point, ref info) == Spti.ScsiStatus.Good)
              {
                switch ((int) info.DataMode & 15)
                {
                  case 1:
                    scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Mode = Commands.TrackMode.Mode1;
                    continue;
                  case 2:
                    scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Mode = Commands.TrackMode.Mode2;
                    continue;
                  case 3:
                    scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Mode = Commands.TrackMode.Audio;
                    continue;
                  default:
                    scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Mode = Commands.TrackMode.Unknown;
                    continue;
                }
              }
              else
              {
                scsiTocTrackArray[(int) scsiFullTocPacket2.Point - 1].Mode = Commands.TrackMode.Unknown;
                continue;
              }
          }
        }
      }
      for (int index = 1; index <= num - 1; ++index)
      {
        scsiTocTrackArray[index - 1].End = (Msf) ((int) scsiTocTrackArray[index].Start - 1);
        fullOld.Add(scsiTocTrackArray[index - 1]);
      }
      fullOld.Add(scsiTocTrackArray[num - 1]);
      gcHandle.Free();
      return fullOld;
    }

    public static List<Commands.ScsiTocTrack> TocGetFormated(
      Spti Device,
      byte[] data,
      Commands.TimeFormat time)
    {
      List<Commands.ScsiTocTrack> formated = new List<Commands.ScsiTocTrack>();
      Commands.ScsiFormatedToc structure1 = new Commands.ScsiFormatedToc();
      Commands.ScsiFormatedTocTrack structure2 = new Commands.ScsiFormatedTocTrack();
      GCHandle gcHandle = GCHandle.Alloc((object) data, GCHandleType.Pinned);
      IntPtr ptr = gcHandle.AddrOfPinnedObject();
      Marshal.PtrToStructure<Commands.ScsiFormatedToc>(ptr, structure1);
      ptr = (IntPtr) (ptr.ToInt32() + Marshal.SizeOf<Commands.ScsiFormatedToc>(structure1));
      for (int index = 0; index <= (int) structure1.LastTrack; ++index)
      {
        Marshal.PtrToStructure<Commands.ScsiFormatedTocTrack>(ptr, structure2);
        Commands.ScsiTocTrack scsiTocTrack = new Commands.ScsiTocTrack();
        Commands.ScsiFormatedTocTrack formatedTocTrack = structure2;
        switch (time)
        {
          case Commands.TimeFormat.lba:
            scsiTocTrack.Start = Msf.FromLBA((int) formatedTocTrack.Addr[0] << 24 | (int) formatedTocTrack.Addr[1] << 16 | (int) formatedTocTrack.Addr[2] << 8 | (int) formatedTocTrack.Addr[3]);
            break;
          case Commands.TimeFormat.Msf:
            scsiTocTrack.Start = Msf.FromMSF(formatedTocTrack.Addr[1], formatedTocTrack.Addr[2], formatedTocTrack.Addr[3]);
            break;
        }
        if (index > 0)
          formated[index - 1].End = (Msf) ((int) scsiTocTrack.Start - 1);
        scsiTocTrack.Track = (int) structure2.track;
        scsiTocTrack.ADR = (Commands.QSubInfo) ((int) structure2.ADR >> 4 & 15);
        scsiTocTrack.CTL = (Commands.QSubControlFlags) ((int) structure2.ADR & 15);
        switch (scsiTocTrack.CTL & Commands.QSubControlFlags.DigitalCopyPermitted)
        {
          case Commands.QSubControlFlags.Audio2ChannelsWOEmphasis:
          case Commands.QSubControlFlags.Audio2ChannelsWEmphasis:
          case Commands.QSubControlFlags.AudioChannelsWOEmphasis:
          case Commands.QSubControlFlags.AudioChannelWEmphasis:
          case Commands.QSubControlFlags.AudioChannelWEmphasis | Commands.QSubControlFlags.DataTrackUninterrupted:
          case Commands.QSubControlFlags.AudioChannelWEmphasis | Commands.QSubControlFlags.DataTrackUninterrupted | Commands.QSubControlFlags.DigitalCopyPermitted:
          case Commands.QSubControlFlags.Reserved:
            scsiTocTrack.Type = Commands.TrackType.Audio;
            scsiTocTrack.Mode = Commands.TrackMode.Audio;
            break;
          default:
            scsiTocTrack.Type = Commands.TrackType.Data;
            break;
        }
        Commands.ScsiTrackInformation info = new Commands.ScsiTrackInformation();
        if (Commands.ReadTrackInformation(Device, structure2.track, ref info) == Spti.ScsiStatus.Good)
        {
          scsiTocTrack.Session = (int) info.SessionNumberLSB;
          if (scsiTocTrack.Mode == Commands.TrackMode.Unknown)
          {
            switch ((int) info.DataMode & 15)
            {
              case 1:
                scsiTocTrack.Mode = Commands.TrackMode.Mode1;
                break;
              case 2:
                scsiTocTrack.Mode = Commands.TrackMode.Mode2;
                break;
              case 3:
                scsiTocTrack.Mode = Commands.TrackMode.Audio;
                break;
              default:
                scsiTocTrack.Mode = Commands.TrackMode.Unknown;
                break;
            }
          }
        }
        if (index < (int) structure1.LastTrack)
          formated.Add(scsiTocTrack);
        ptr = (IntPtr) (ptr.ToInt32() + Marshal.SizeOf<Commands.ScsiFormatedTocTrack>(structure2));
      }
      gcHandle.Free();
      return formated;
    }

    private static void AddCDB8(ref byte[] cdb, int index, byte value) => cdb[index] = (byte) ((uint) cdb[index] | (uint) value);

    private static void PutCDB8(ref byte[] cdb, int index, byte value) => cdb[index] = value;

    private static void PutCDB16(ref byte[] cdb, int index, short value)
    {
      cdb[index] = Convert.ToByte((int) value >> 8 & (int) byte.MaxValue);
      cdb[index + 1] = Convert.ToByte((int) value & (int) byte.MaxValue);
    }

    private static void PutCDB24(ref byte[] cdb, int index, int value)
    {
      cdb[index] = Convert.ToByte(value >> 16 & (int) byte.MaxValue);
      cdb[index + 1] = Convert.ToByte(value >> 8 & (int) byte.MaxValue);
      cdb[index + 2] = Convert.ToByte(value & (int) byte.MaxValue);
    }

    private static void PutCDB32(ref byte[] cdb, int index, int value)
    {
      cdb[index] = Convert.ToByte(value >> 24 & (int) byte.MaxValue);
      cdb[index + 1] = Convert.ToByte(value >> 16 & (int) byte.MaxValue);
      cdb[index + 2] = Convert.ToByte(value >> 8 & (int) byte.MaxValue);
      cdb[index + 3] = Convert.ToByte(value & (int) byte.MaxValue);
    }

    private static short ReverseI16(short short_in)
    {
      byte[] numArray = new byte[2]
      {
        Convert.ToByte((int) short_in >> 8 & (int) byte.MaxValue),
        Convert.ToByte((int) short_in & (int) byte.MaxValue)
      };
      return (short) ((int) Convert.ToInt16(numArray[1]) << 8 | (int) numArray[0]);
    }

    private static int ReverseI32(int int_in)
    {
      byte[] numArray = new byte[4]
      {
        Convert.ToByte(int_in >> 24 & (int) byte.MaxValue),
        Convert.ToByte(int_in >> 16 & (int) byte.MaxValue),
        Convert.ToByte(int_in >> 8 & (int) byte.MaxValue),
        Convert.ToByte(int_in & (int) byte.MaxValue)
      };
      return Convert.ToInt32(numArray[3]) << 24 | Convert.ToInt32(numArray[2]) << 16 | Convert.ToInt32(numArray[1]) << 8 | Convert.ToInt32(numArray[0]);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ScsiTocTrack
    {
      public int Track = -1;
      public int Session = -1;
      public Msf Start = (Msf) 0;
      public Msf End = (Msf) 0;
      public Commands.QSubInfo ADR = Commands.QSubInfo.NotSupplied;
      public Commands.QSubControlFlags CTL = Commands.QSubControlFlags.Reserved;
      public Commands.TrackMode Mode = Commands.TrackMode.Unknown;
      public Commands.TrackType Type = Commands.TrackType.Unknown;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ScsiInquiryResult
    {
      public string Vendor;
      public string Product;
      public string Revision;
    }

    public enum TocFormat
    {
      FormatedToc,
      SessionInfo,
      FullToc,
      PMA,
      Atip,
      CDText,
    }

    public enum TimeFormat
    {
      lba = 0,
      Msf = 2,
    }

    public enum TrackType
    {
      Audio,
      Data,
      Unknown,
    }

    public enum TrackMode
    {
      Mode1,
      Mode2,
      Audio,
      Unknown,
    }

    public enum ReadCDFlags
    {
      ErrorField = 3,
      EDC_ECC = 6,
      HeaderCodes = 6,
      SYNC = 7,
      UserData = 7,
      Raw = 248, // 0x000000F8
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ScsiInquiry
    {
      public byte qualifier;
      public byte rmb;
      public byte version;
      public byte respfmt;
      public byte addlen;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
      public byte[] various1;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
      public char[] vendor;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
      public char[] product;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
      public char[] revision;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 219)]
      public byte[] various2;
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ScsiFormatedTocTrack
    {
      public byte Reserved;
      public byte ADR;
      public byte track;
      public byte Reserved2;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
      public byte[] Addr;

      public ScsiFormatedTocTrack() => this.Addr = new byte[4];
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ScsiFullTocPacket
    {
      public byte Session;
      public byte ADR;
      public byte TNO;
      public byte Point;
      public byte Min;
      public byte Sec;
      public byte Frm;
      public byte Zero;
      public byte PMin;
      public byte PSec;
      public byte PFrm;
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ScsiFormatedToc
    {
      public byte TocLenLo;
      public byte TocLenHi;
      public byte FirstTrack;
      public byte LastTrack;
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ScsiFullToc
    {
      public byte TocLenLo;
      public byte TocLenHi;
      public byte FirstTrack;
      public byte LastTrack;
    }

    public enum QSubInfo
    {
      NotSupplied,
      CurrentPosition,
      MediaCatalogNumber,
      ISRC,
    }

    public enum QSubControlFlags
    {
      Audio2ChannelsWOEmphasis = 0,
      Audio2ChannelsWEmphasis = 1,
      DigitalCopyPermitted = 2,
      DataTrackUninterrupted = 4,
      DataTrackInterrupted = 5,
      AudioChannelsWOEmphasis = 8,
      AudioChannelWEmphasis = 9,
      Reserved = 18, // 0x00000012
    }

    [StructLayout(LayoutKind.Sequential)]
    public class ScsiTrackInformation
    {
      public byte DataLenHi;
      public byte DataLenLo;
      public byte TrackNumberLSB;
      public byte SessionNumberLSB;
      public byte rsvd1;
      public byte TrackMode;
      public byte DataMode;
      public byte NWA_LRA;
      public int TrackStartAddress;
      public int NextWritableAddress;
      public int FreeBlocks;
      public int BlockingFactor;
      public int TrackSize;
      public int LastRecordedAddress;
      public byte TrackNumberMSB;
      public byte SessionNumberMSB;
      public byte rsvd2;
      public byte rsvd3;
    }

    public enum RotationControl
    {
      CLV,
      CAV,
    }

    public enum LoUnloadAction
    {
      Load,
      Unload,
    }

    public enum PreventRemovalAction
    {
      Allow,
      Prevent,
    }

    public enum PowerCondition
    {
      StartValid = 0,
      Active = 1,
      Idle = 2,
      Standby = 3,
      LuControl = 7,
      ForceIdle0 = 10, // 0x0000000A
      ForceStandby0 = 11, // 0x0000000B
    }
  }
}
