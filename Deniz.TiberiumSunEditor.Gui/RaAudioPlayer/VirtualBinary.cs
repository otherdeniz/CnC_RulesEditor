namespace Deniz.TiberiumSunEditor.Gui.RaAudioPlayer;

public class VirtualBinary
{
    private byte[] _data;
    private int _writePosition;

    public VirtualBinary()
    {
        _data = new byte[0];
        _writePosition = 0;
    }

    public byte[] WriteStart(int size)
    {
        _data = new byte[size];
        _writePosition = 0;
        return _data;
    }

    public byte[] Data
    {
        get { return _data; }
    }

    public int Size
    {
        get { return _data.Length; }
    }

    public bool Read(byte[] buffer, int size)
    {
        if (_writePosition + size > _data.Length)
            return false;

        Array.Copy(_data, _writePosition, buffer, 0, size);
        _writePosition += size;
        return true;
    }

    public bool Write(byte[] buffer, int size)
    {
        if (_writePosition + size > _data.Length)
            return false;

        Array.Copy(buffer, 0, _data, _writePosition, size);
        _writePosition += size;
        return true;
    }
}