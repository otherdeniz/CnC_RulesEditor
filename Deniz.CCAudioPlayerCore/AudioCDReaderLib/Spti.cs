// Decompiled with JetBrains decompiler
// Type: AudioCDReaderLib.Spti
// Assembly: CSAudioPlayer, Version=1.1.2.0, Culture=neutral, PublicKeyToken=null
// MVID: A8E584D5-B795-4193-87B8-CE822D716F58
// Assembly location: C:\Code\Deniz.TiberiumSunEditor\Deniz.TiberiumSunEditor.Gui\bin\Debug\net6.0-windows\CSAudioPlayer.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AudioCDReaderLib
{
  internal class Spti : IDisposable
  {
    public const uint IOCTL_CDROM_READ_TOC = 147456;
    public const uint IOCTL_STORAGE_CHECK_VERIFY = 2967552;
    public const uint IOCTL_CDROM_RAW_READ = 147518;
    public const uint IOCTL_STORAGE_MEDIA_REMOVAL = 2967556;
    public const int IOCTL_STORAGE_EJECT_MEDIA = 2967560;
    public const int IOCTL_STORAGE_LOAD_MEDIA = 2967564;
    private const int ERROR_NO_MORE_ITEMS = 259;
    private const int ERROR_INSUFFICIENT_BUFFER = 122;
    private const int ERROR_INVALID_DATA = 13;
    private const int IOCTL_SCSI_BASE = 4;
    private int IOCTL_SCSI_PASS_THROUGH_DIRECT;
    private int IOCTL_SCSI_GET_ADDRESS;
    private int IOCTL_SCSI_GET_CAPABILITIES;
    private List<Spti.DriveHandle> m_lstHandles = new List<Spti.DriveHandle>();
    private byte m_btLastAsc;
    private byte m_btLastAscq;
    private byte m_btLastSK;
    private int m_intDevIndex = -1;
    private bool m_disposedValue = false;

    [DllImport("setupapi.dll")]
    private static extern int SetupDiDestroyDeviceInfoList(int DeviceInfoSet);

    [DllImport("setupapi.dll")]
    private static extern int SetupDiEnumDeviceInfo(
      int DeviceInfoSet,
      int MemberIndex,
      Spti.SP_DEVINFO_DATA DeviceInfoData);

    [DllImport("setupapi.dll")]
    private static extern int SetupDiEnumDeviceInterfaces(
      int DeviceInfoSet,
      IntPtr DeviceInfoData,
      [MarshalAs(UnmanagedType.LPStruct)] Guid InterfaceClassGuid,
      int MemberIndex,
      [MarshalAs(UnmanagedType.LPStruct)] Spti.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData);

    [DllImport("setupapi.dll")]
    private static extern int SetupDiGetClassDevs(
      [MarshalAs(UnmanagedType.LPStruct)] Guid classguid,
      [MarshalAs(UnmanagedType.LPStr)] string enumerator,
      int hwndparent,
      Spti.SetupDiGetClassDevsFlags flags);

    [DllImport("setupapi.dll")]
    private static extern int SetupDiGetDeviceInterfaceDetail(
      int DeviceInfoSet,
      Spti.SP_DEVICE_INTERFACE_DATA DeviceInterfaceData,
      IntPtr DeviceInterfaceDetailData,
      int DeviceInterfaceDetailDataSize,
      ref int RequiredSize,
      IntPtr DeviceInfoData);

    [DllImport("setupapi.dll")]
    private static extern int SetupDiGetDeviceRegistryProperty(
      int DeviceInfoSet,
      Spti.SP_DEVINFO_DATA DeviceInfoData,
      int devproperty,
      int PropertyRegDataType,
      IntPtr PropertyBuffer,
      int PropertyBufferSize,
      ref int RequiredSize);

    [DllImport("kernel32")]
    private static extern int DeviceIoControl(
      IntPtr hDevice,
      int dwIoControlCode,
      Spti.ScsiPassThroughDirectWSI lpInBuffer,
      int nInBufferSize,
      IntPtr lpOutBuffer,
      int nOutBufferSize,
      ref int lpBytesReturned,
      IntPtr lpOverlapped);

    [DllImport("kernel32")]
    private static extern int DeviceIoControl(
      IntPtr hDevice,
      int dwIoControlCode,
      IntPtr lpInBuffer,
      int nInBufferSize,
      IntPtr lpOutBuffer,
      int nOutBufferSize,
      ref int lpBytesReturned,
      IntPtr lpOverlapped);

    [DllImport("kernel32")]
    private static extern int DeviceIoControl(
      IntPtr hDevice,
      int dwIoControlCode,
      [MarshalAs(UnmanagedType.LPStruct)] Spti.ScsiAddress lpInBuffer,
      int nInBufferSize,
      [MarshalAs(UnmanagedType.LPStruct)] Spti.ScsiAddress lpOutBuffer,
      int nOutBufferSize,
      ref int lpBytesReturned,
      IntPtr lpOverlapped);

    [DllImport("kernel32")]
    private static extern int DeviceIoControl(
      IntPtr hDevice,
      int dwIoControlCode,
      [MarshalAs(UnmanagedType.LPStruct)] Spti.IoScsiCapabilities lpInBuffer,
      int nInBufferSize,
      [MarshalAs(UnmanagedType.LPStruct)] Spti.IoScsiCapabilities lpOutBuffer,
      int nOutBufferSize,
      ref int lpBytesReturned,
      IntPtr lpOverlapped);

    [DllImport("kernel32")]
    private static extern IntPtr CreateFile(
      string lpFileName,
      int dwDesiredAccess,
      int dwShareMode,
      IntPtr lpSecurityAttributes,
      int dwCreationDisposition,
      int dwFlagsAndAttributes,
      int hTemplateFile);

    [DllImport("kernel32")]
    private static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32")]
    private static extern int GetLastError();

    public Spti()
    {
      this.IOCTL_SCSI_PASS_THROUGH_DIRECT = this.CtlCode(4, 1029, Spti.Method.Buffered, Spti.FileAccess.ReadAccess | Spti.FileAccess.WriteAccess);
      this.IOCTL_SCSI_GET_ADDRESS = this.CtlCode(4, 1030, Spti.Method.Buffered, Spti.FileAccess.AnyAccess);
      this.IOCTL_SCSI_GET_CAPABILITIES = this.CtlCode(4, 1028, Spti.Method.Buffered, Spti.FileAccess.AnyAccess);
      if (Environment.OSVersion.Platform != PlatformID.Win32NT)
        return;
      this.FindDrives();
    }

    ~Spti() => this.CloseDrives();

    public bool EjectCD(int index)
    {
      int lpBytesReturned = 0;
      return Spti.DeviceIoControl(this.m_lstHandles[index].handle, 2967560, IntPtr.Zero, 0, IntPtr.Zero, 0, ref lpBytesReturned, IntPtr.Zero) != 0;
    }

    public bool CloseCD(int index)
    {
      int lpBytesReturned = 0;
      return Spti.DeviceIoControl(this.m_lstHandles[index].handle, 2967564, IntPtr.Zero, 0, IntPtr.Zero, 0, ref lpBytesReturned, IntPtr.Zero) != 0;
    }

    public int DeviceCount => this.m_lstHandles.Count;

    public string DeviceName(int index) => this.m_lstHandles[index].name;

    public Spti.BusPosition DeviceAddress(int index) => this.GetDevBusPosition(index);

    public int DeviceMaximumTransferLength(int index)
    {
      Spti.IoScsiCapabilities devCapabilities = this.GetDevCapabilities(index);
      return devCapabilities == null || devCapabilities.MaximumTransferLength == 0 || devCapabilities.MaximumTransferLength > 65536 ? 65536 : devCapabilities.MaximumTransferLength;
    }

    public Spti.SenseKey LastSenseKey => (Spti.SenseKey) this.m_btLastSK;

    public byte LastAddSenseCode => this.m_btLastAsc;

    public byte LastAdSenseQualifier => this.m_btLastAscq;

    public int SelectedDevice => this.m_intDevIndex;

    public bool SelectDevice(int index)
    {
      if (index > this.m_lstHandles.Count - 1 || index < 0)
        return false;
      this.m_intDevIndex = index;
      return true;
    }

    public Spti.ScsiStatus ExecuteCommand(
      byte[] cdb,
      byte cdblen,
      Spti.DataDirection direction,
      IntPtr pBuffer,
      int bufferlen,
      uint timeout = 5)
    {
      IntPtr handle = this.m_lstHandles[this.m_intDevIndex].handle;
      int lpBytesReturned = 0;
      Spti.ScsiPassThroughDirectWSI throughDirectWsi1 = new Spti.ScsiPassThroughDirectWSI();
      Spti.ScsiPassThroughDirectWSI throughDirectWsi2 = throughDirectWsi1;
      throughDirectWsi2.length = (ushort) 44;
      Array.Copy((Array) cdb, (Array) throughDirectWsi2.cdb, (int) cdblen);
      throughDirectWsi2.CdbLength = cdblen;
      throughDirectWsi2.TimeOutValue = timeout != 0U ? timeout : 108000U;
      throughDirectWsi2.SenseInfoLength = Convert.ToByte(throughDirectWsi1.senseinfo.GetUpperBound(0) + 1);
      throughDirectWsi2.SenseInfoOffset = 48U;
      throughDirectWsi2.DataIn = Convert.ToByte((object) direction);
      throughDirectWsi2.DataBuffer = pBuffer;
      throughDirectWsi2.DataTransferLength = bufferlen;
      IntPtr num1 = Marshal.AllocHGlobal(80);
      int num2 = Spti.DeviceIoControl(handle, this.IOCTL_SCSI_PASS_THROUGH_DIRECT, throughDirectWsi1, Marshal.SizeOf<Spti.ScsiPassThroughDirectWSI>(throughDirectWsi1), num1, Marshal.SizeOf<Spti.ScsiPassThroughDirectWSI>(throughDirectWsi1), ref lpBytesReturned, new IntPtr(0));
      Marshal.PtrToStructure<Spti.ScsiPassThroughDirectWSI>(num1, throughDirectWsi1);
      Marshal.FreeHGlobal(num1);
      this.m_btLastSK = Convert.ToByte((int) throughDirectWsi1.senseinfo[2] & 15);
      this.m_btLastAsc = throughDirectWsi1.senseinfo[12];
      this.m_btLastAscq = throughDirectWsi1.senseinfo[13];
      return num2 != 1 ? Spti.ScsiStatus.Timeout : (Spti.ScsiStatus) throughDirectWsi1.ScsiStatus;
    }

    private void CloseDrives()
    {
      if (this.m_lstHandles == null)
        return;
      for (int index = 0; index <= this.m_lstHandles.Count - 1; ++index)
        Spti.CloseHandle(this.m_lstHandles[index].handle);
      this.m_lstHandles.Clear();
    }

    private int FindDrives()
    {
      int index = 0;
      Guid classguid = new Guid(1295444325U, (ushort) 58149, (ushort) 4558, (byte) 191, (byte) 193, (byte) 8, (byte) 0, (byte) 43, (byte) 225, (byte) 3, (byte) 24);
      Guid guid = new Guid(1408590600U, (ushort) 46783, (ushort) 4560, (byte) 148, (byte) 242, (byte) 0, (byte) 160, (byte) 201, (byte) 30, (byte) 251, (byte) 139);
      int classDevs1 = Spti.SetupDiGetClassDevs(classguid, (string) null, 0, Spti.SetupDiGetClassDevsFlags.Present);
      if (classDevs1 == 0)
        return 0;
      int classDevs2 = Spti.SetupDiGetClassDevs(guid, (string) null, 0, Spti.SetupDiGetClassDevsFlags.DeviceInterface | Spti.SetupDiGetClassDevsFlags.Present);
      if (classDevs2 == 0)
      {
        Spti.SetupDiDestroyDeviceInfoList(classDevs1);
        return 0;
      }
      this.CloseDrives();
      int minValue = int.MinValue;
      if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 5)
        minValue |= 1073741824;
      while (true)
      {
        string devName = this.GetDevName(classDevs1, index);
        string devPath = this.GetDevPath(classDevs2, index, guid);
        if (!(devName == null | devPath == null))
        {
          IntPtr file = Spti.CreateFile(devPath, minValue, 3, new IntPtr(0), 3, 0, 0);
          if (file.ToInt32() != -1)
            this.m_lstHandles.Add(new Spti.DriveHandle(devName, devPath, file));
          ++index;
        }
        else
          break;
      }
      Spti.SetupDiDestroyDeviceInfoList(classDevs1);
      Spti.SetupDiDestroyDeviceInfoList(classDevs2);
      return this.DeviceCount;
    }

    private string GetDevName(int DevInfo, int index)
    {
      Spti.SP_DEVINFO_DATA spDevinfoData = new Spti.SP_DEVINFO_DATA();
      int RequiredSize = 0;
      int PropertyRegDataType = 0;
      spDevinfoData.cbSize = Marshal.SizeOf<Spti.SP_DEVINFO_DATA>(spDevinfoData);
      if (Spti.SetupDiEnumDeviceInfo(DevInfo, index, spDevinfoData) == 0)
      {
        if (Spti.GetLastError() != 259)
          ;
        return (string) null;
      }
      GCHandle gcHandle1 = GCHandle.Alloc((object) new byte[1], GCHandleType.Pinned);
      int registryProperty1 = Spti.SetupDiGetDeviceRegistryProperty(DevInfo, spDevinfoData, 12, PropertyRegDataType, gcHandle1.AddrOfPinnedObject(), RequiredSize, ref RequiredSize);
      gcHandle1.Free();
      if (registryProperty1 == 0)
      {
        switch (Spti.GetLastError())
        {
          case 13:
            return (string) null;
          case 122:
            break;
          default:
            return (string) null;
        }
      }
      byte[] numArray = new byte[RequiredSize - 1 + 1];
      GCHandle gcHandle2 = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
      int registryProperty2 = Spti.SetupDiGetDeviceRegistryProperty(DevInfo, spDevinfoData, 12, PropertyRegDataType, gcHandle2.AddrOfPinnedObject(), RequiredSize, ref RequiredSize);
      gcHandle2.Free();
      if (registryProperty2 == 0)
        return Spti.GetLastError() == 13 ? "" : (string) null;
      string devName = "";
      for (int index1 = 0; index1 <= numArray.GetUpperBound(0) && numArray[index1] != (byte) 0; ++index1)
        devName += ((char) numArray[index1]).ToString();
      return devName;
    }

    private string GetDevPath(int IntDevInfo, int index, Guid guidICD)
    {
      Spti.SP_DEVICE_INTERFACE_DATA deviceInterfaceData = new Spti.SP_DEVICE_INTERFACE_DATA();
      Spti.SP_DEVICE_INTERFACE_DETAIL_DATA interfaceDetailData = new Spti.SP_DEVICE_INTERFACE_DETAIL_DATA();
      int RequiredSize = 0;
      deviceInterfaceData.cbSize = Marshal.SizeOf<Spti.SP_DEVICE_INTERFACE_DATA>(deviceInterfaceData);
      if (Spti.SetupDiEnumDeviceInterfaces(IntDevInfo, new IntPtr(0), guidICD, index, deviceInterfaceData) == 0)
        return Spti.GetLastError() == 259 ? (string) null : (string) null;
      if (Spti.SetupDiGetDeviceInterfaceDetail(IntDevInfo, deviceInterfaceData, IntPtr.Zero, 0, ref RequiredSize, new IntPtr(0)) == 0 && Spti.GetLastError() != 122)
        return (string) null;
      int DeviceInterfaceDetailDataSize = RequiredSize;
      byte[] numArray = new byte[DeviceInterfaceDetailDataSize - 1 + 1];
      numArray[0] = (byte) 5;
      GCHandle gcHandle1 = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
      int deviceInterfaceDetail = Spti.SetupDiGetDeviceInterfaceDetail(IntDevInfo, deviceInterfaceData, gcHandle1.AddrOfPinnedObject(), DeviceInterfaceDetailDataSize, ref RequiredSize, new IntPtr(0));
      gcHandle1.Free();
      if (deviceInterfaceDetail == 0)
        return (string) null;
      GCHandle gcHandle2 = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
      string stringAnsi = Marshal.PtrToStringAnsi(gcHandle2.AddrOfPinnedObject() + 4);
      gcHandle2 = GCHandle.Alloc((object) numArray, GCHandleType.Pinned);
      gcHandle2.Free();
      return stringAnsi;
    }

    private Spti.BusPosition GetDevBusPosition(int index)
    {
      Spti.ScsiAddress scsiAddress1 = new Spti.ScsiAddress();
      int lpBytesReturned = 0;
      scsiAddress1.length = Marshal.SizeOf<Spti.ScsiAddress>(scsiAddress1);
      if (Spti.DeviceIoControl(this.m_lstHandles[index].handle, this.IOCTL_SCSI_GET_ADDRESS, scsiAddress1, scsiAddress1.length, scsiAddress1, scsiAddress1.length, ref lpBytesReturned, new IntPtr(0)) != 1)
        return (Spti.BusPosition) null;
      Spti.ScsiAddress scsiAddress2 = scsiAddress1;
      return new Spti.BusPosition(scsiAddress2.PathId, scsiAddress2.PortNumber, scsiAddress2.TargetId, scsiAddress2.Lun);
    }

    private Spti.IoScsiCapabilities GetDevCapabilities(int index)
    {
      Spti.IoScsiCapabilities scsiCapabilities = new Spti.IoScsiCapabilities();
      int lpBytesReturned = 0;
      scsiCapabilities.length = Marshal.SizeOf<Spti.IoScsiCapabilities>(scsiCapabilities);
      return Spti.DeviceIoControl(this.m_lstHandles[index].handle, this.IOCTL_SCSI_GET_CAPABILITIES, scsiCapabilities, scsiCapabilities.length, scsiCapabilities, scsiCapabilities.length, ref lpBytesReturned, new IntPtr(0)) == 1 ? scsiCapabilities : (Spti.IoScsiCapabilities) null;
    }

    private int CtlCode(
      int intDevType,
      int intFunction,
      Spti.Method intMethod,
      Spti.FileAccess intAccess)
    {
      return (int) ((Spti.Method) (intDevType << 16 | (int) intAccess << 14 | intFunction << 2) | intMethod);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.m_disposedValue)
        this.CloseDrives();
      this.m_disposedValue = true;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public enum ScsiStatus
    {
      Good = 0,
      CheckCondition = 2,
      ConditionMet = 4,
      Busy = 8,
      Intermediate = 16, // 0x00000010
      IntermediateConditionMet = 20, // 0x00000014
      ReservationConflict = 24, // 0x00000018
      CommandTerminated = 34, // 0x00000022
      QueueFull = 40, // 0x00000028
      Timeout = 255, // 0x000000FF
    }

    public enum SenseKey
    {
      NoSense = 0,
      RecoveredError = 1,
      NotReady = 2,
      MediumError = 3,
      HardError = 4,
      IllegalRequest = 5,
      UnitAttention = 6,
      DataProtect = 7,
      BlankCheck = 8,
      VendorSpecific = 9,
      CopyAbort = 10, // 0x0000000A
      VolumeOverflow = 13, // 0x0000000D
      Miscompare = 14, // 0x0000000E
      Reserved = 15, // 0x0000000F
      Equal = 18, // 0x00000012
    }

    public class BusPosition
    {
      private byte m_btHA;
      private byte m_btTgt;
      private byte m_btLun;
      private byte m_btPort;

      public BusPosition(byte portid, byte ha, byte tgt, byte lun)
      {
        this.m_btPort = portid;
        this.m_btHA = ha;
        this.m_btTgt = tgt;
        this.m_btLun = lun;
      }

      public byte PortID => this.m_btPort;

      public byte HostAdapter => this.m_btHA;

      public byte Target => this.m_btTgt;

      public byte LogicalUnitNumber => this.m_btLun;
    }

    public enum DataDirection
    {
      @out,
      @in,
      unspecified,
    }

    public enum ScsiCommand
    {
      TestUnitReady = 0,
      RecieveDiagnosticResults = 1,
      RezeroUnit = 1,
      Erase10 = 2,
      ReadBuffer = 3,
      RequestSense = 3,
      FormatUnit = 4,
      LogSelect = 4,
      ReadBufferCapacity = 5,
      ReassingBlocks = 7,
      Read6 = 8,
      GetPerformance = 10, // 0x0000000A
      Write6 = 10, // 0x0000000A
      PlayCD = 11, // 0x0000000B
      Seek6 = 11, // 0x0000000B
      Inquiry = 18, // 0x00000012
      ModeSelect6 = 21, // 0x00000015
      ReleaseUnit = 23, // 0x00000017
      Copy = 24, // 0x00000018
      ModeSense6 = 26, // 0x0000001A
      StartStopUnit = 27, // 0x0000001B
      SendDiagnostic = 29, // 0x0000001D
      PreventMediumRemoval = 30, // 0x0000001E
      ReadCapacity = 37, // 0x00000025
      Read10 = 40, // 0x00000028
      Write10 = 42, // 0x0000002A
      Seek10 = 43, // 0x0000002B
      WriteVerify = 46, // 0x0000002E
      Verify = 47, // 0x0000002F
      SearchDataHigh = 48, // 0x00000030
      SearchDataEqual = 49, // 0x00000031
      SearchDataLow = 50, // 0x00000032
      SetLimits = 51, // 0x00000033
      Prefetch = 52, // 0x00000034
      SynchronizeCache = 53, // 0x00000035
      LockUnlockCache = 54, // 0x00000036
      ReadDefectedData = 55, // 0x00000037
      Compare = 57, // 0x00000039
      CopyVerify = 58, // 0x0000003A
      WriteBuffer = 59, // 0x0000003B
      ReadLong = 62, // 0x0000003E
      WriteLong = 63, // 0x0000003F
      ChangeDefinition = 64, // 0x00000040
      WriteSame = 65, // 0x00000041
      ReadSubChannel = 66, // 0x00000042
      ReadTOC = 67, // 0x00000043
      ReadHeader = 68, // 0x00000044
      PlayAudio10 = 69, // 0x00000045
      GetConfiguration = 70, // 0x00000046
      PlayAudioMSF = 71, // 0x00000047
      PlayAudioIndex = 72, // 0x00000048
      PlayTrackRelative10 = 73, // 0x00000049
      GetEventStatusNotification = 74, // 0x0000004A
      PauseResume = 75, // 0x0000004B
      LogSense = 77, // 0x0000004D
      ReadTrackInformation = 82, // 0x00000052
      ModeSelect10 = 85, // 0x00000055
      ModeSense10 = 90, // 0x0000005A
      CloseTrackSession = 91, // 0x0000005B
      Blank = 161, // 0x000000A1
      PlayAudio12 = 165, // 0x000000A5
      LoadUnload = 166, // 0x000000A6
      PlyTrackRelative12 = 169, // 0x000000A9
      ReadCDMSF = 185, // 0x000000B9
      SetSpeed = 187, // 0x000000BB
      MechanismStatus = 189, // 0x000000BD
      ReadCD = 190, // 0x000000BE
    }

    [StructLayout(LayoutKind.Sequential)]
    private class IoScsiCapabilities
    {
      public int length;
      public int MaximumTransferLength;
      public int MaximumPhysicalPages;
      public int SupportedAsynchronousEvents;
      public int AlignmentMask;
      public byte TaggedQueuing;
      public byte AdapterScansDown;
      public byte AdapterUsesPio;
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ScsiAddress
    {
      public int length;
      public byte PortNumber;
      public byte PathId;
      public byte TargetId;
      public byte Lun;
    }

    private struct ScsiPassThroughDirect
    {
      public ushort length;
      public byte ScsiStatus;
      public byte PathId;
      public byte TargetId;
      public byte Lun;
      public byte CdbLength;
      public byte SenseInfoLength;
      public byte DataIn;
      public uint DataTransferLength;
      public uint TimeOutValue;
      public int DataBuffer;
      public uint SenseInfoOffset;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
      public byte[] cdb;
    }

    private struct SenseInfoData
    {
      public byte ErrorCode;
      public byte SegmentNum;
      public byte SenseKey;
      public byte InfoByte0;
      public byte InfoByte1;
      public byte InfoByte2;
      public byte InfoByte3;
      public byte AddSenLen;
      public byte ComSpecInfo0;
      public byte ComSpecInfo1;
      public byte ComSpecInfo2;
      public byte ComSpecInfo3;
      public byte AddSenseCode;
      public byte AddSenQual;
      public byte FieldRepUCode;
      public byte SenKeySpec15;
      public byte SenKeySpec16;
      public byte SenKeySpec17;
      public byte AddSenseBytes;
    }

    [StructLayout(LayoutKind.Sequential)]
    private class ScsiPassThroughDirectWSI
    {
      public ushort length;
      public byte ScsiStatus;
      public byte PathId;
      public byte TargetId;
      public byte Lun;
      public byte CdbLength;
      public byte SenseInfoLength;
      public byte DataIn;
      public int DataTransferLength;
      public uint TimeOutValue;
      public IntPtr DataBuffer;
      public uint SenseInfoOffset;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
      public byte[] cdb;
      public int intFill;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
      public byte[] senseinfo;

      public ScsiPassThroughDirectWSI()
      {
        this.cdb = new byte[16];
        this.senseinfo = new byte[31];
      }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SP_DEVINFO_DATA
    {
      public int cbSize;
      public Guid classguid;
      public int DevInst;
      public int rsvd;
    }

    [StructLayout(LayoutKind.Sequential)]
    private class SP_DEVICE_INTERFACE_DATA
    {
      public int cbSize;
      public Guid InterfaceClassGuid;
      public Spti.DeviceInterfaceDataFlags flags;
      public int rsvd;
    }

    private enum DeviceInterfaceDataFlags
    {
      Active = 1,
      Default = 2,
      Removed = 4,
    }

    [StructLayout(LayoutKind.Sequential)]
    private class SP_DEVICE_INTERFACE_DETAIL_DATA
    {
      public int cbSize;
      public int DevicePathStrPtrA;
    }

    private struct DriveHandle
    {
      public IntPtr handle;
      public string name;
      public string path;

      public DriveHandle(string DevName, string DevPath, IntPtr DevHandle)
      {
        this.name = DevName;
        this.path = DevPath;
        this.handle = DevHandle;
      }
    }

    private enum CreateDevInfoCreationFlags
    {
      GenerateId = 1,
      InheritClassDrivers = 2,
    }

    private enum OpenClassRegKeyExFlags
    {
      Installer = 1,
      Interface = 2,
    }

    private enum OpenDeviceInfoFlags
    {
      InheritClassDrivers = 2,
      CancelRemove = 4,
    }

    private enum SetupDiGetClassDevsFlags
    {
      Present = 2,
      AllClasses = 4,
      Profile = 8,
      DeviceInterface = 16, // 0x00000010
    }

    private enum SetupDeviceProperties
    {
      DeviceDescription = 0,
      Address = 1,
      HardwareId = 1,
      CompatibleIds = 2,
      Service = 4,
      Class = 7,
      ClassGuid = 8,
      Driver = 9,
      ConfigFlags = 10, // 0x0000000A
      Mfg = 11, // 0x0000000B
      FriendlyName = 12, // 0x0000000C
      LocationInformation = 13, // 0x0000000D
      PhysicalDeviceObjectName = 14, // 0x0000000E
      Capabilities = 15, // 0x0000000F
      UINumber = 16, // 0x00000010
      UpperFilters = 17, // 0x00000011
      LowerFilters = 18, // 0x00000012
      BusTypeGuid = 19, // 0x00000013
      EnumeratorName = 22, // 0x00000016
      Security = 23, // 0x00000017
      SecuritySds = 24, // 0x00000018
      DeviceType = 25, // 0x00000019
      Characteristics = 27, // 0x0000001B
      UINumberDescriptionFormat = 29, // 0x0000001D
      DevicePowerData = 30, // 0x0000001E
      RemovalPolicy = 31, // 0x0000001F
      RemovalPolicyHwDefault = 32, // 0x00000020
      RemovalPolicyOverride = 33, // 0x00000021
    }

    private enum FileOpenMethod
    {
      CreateNew = 1,
      CreateAlways = 2,
      OpenExisting = 3,
      OpenAlways = 4,
    }

    private enum FileShareRights
    {
      Read = 1,
      Write = 2,
    }

    private enum FileAccessRights
    {
      Read = -2147483648, // 0x80000000
      Write = 1073741824, // 0x40000000
    }

    private enum FileAccess
    {
      AnyAccess,
      ReadAccess,
      WriteAccess,
    }

    private enum Method
    {
      Buffered,
      InDirect,
      OutDirect,
      Neither,
    }
  }
}
