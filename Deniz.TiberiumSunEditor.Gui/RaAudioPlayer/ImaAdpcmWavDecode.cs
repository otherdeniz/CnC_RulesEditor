namespace Deniz.TiberiumSunEditor.Gui.RaAudioPlayer;

public class ImaAdpcmWavDecode
{
    private const int NumSteps = 89;
    private static readonly int[] StepTable = new int[]
    {
        7, 8, 9, 10, 11, 12, 13, 14, 16,
        17, 19, 21, 23, 25, 28, 31, 34, 37,
        41, 45, 50, 55, 60, 66, 73, 80, 88,
        97, 107, 118, 130, 143, 157, 173, 190, 209,
        230, 253, 279, 307, 337, 371, 408, 449, 494,
        544, 598, 658, 724, 796, 876, 963, 1060, 1166,
        1282, 1411, 1552, 1707, 1878, 2066, 2272, 2499, 2749,
        3024, 3327, 3660, 4026, 4428, 4871, 5358, 5894, 6484,
        7132, 7845, 8630, 9493, 10442, 11487, 12635, 13899, 15289,
        16818, 18500, 20350, 22385, 24623, 27086, 29794, 32767
    };
    private static readonly int[] IndexAdjustTable = new int[]
    {
        -1, -1, -1, -1, 2, 4, 6, 8
    };

    private int _index;
    private int _sample;
    private short[] _decodedData;
    private int _samplesCount;

    public ImaAdpcmWavDecode()
    {
        _index = 0;
        _sample = 0;
    }

    public void Load(byte[] data, int size, int channels, int chunkSize)
    {
        _index = 0;
        _sample = 0;
        // chat-gpt version
        //Decode(data, size, channels);
        // XCC converted manually version
        _decodedData = new short[size * channels];
        _samplesCount = size;
        AudioDecodeIMAChunk(data, _decodedData, 0, ref _index, ref _sample, size);
    }

    public static void AudioDecodeIMAChunk(byte[] audioIn, short[] decodedData, int writeOffset, ref int index, ref int sample, int csChunk)
    {
        short[] audioOutArray = new short[csChunk];

        for (int sampleIndex = 0; sampleIndex < csChunk; sampleIndex++)
        {
            int code = audioIn[sampleIndex >> 1];
            code = (sampleIndex & 1) != 0 ? code >> 4 : code & 0xf;
            var step = StepTable[index];
            var delta = step >> 3;
            if ((code & 1) != 0)
                delta += step >> 2;
            if ((code & 2) != 0)
                delta += step >> 1;
            if ((code & 4) != 0)
                delta += step;
            if ((code & 8) != 0)
            {
                sample -= delta;
                if (sample < -32768)
                    sample = -32768;
            }
            else
            {
                sample += delta;
                if (sample > 32767)
                    sample = 32767;
            }
            audioOutArray[sampleIndex] = (short)sample;
            index += IndexAdjustTable[code & 7];
            if (index < 0)
                index = 0;
            else if (index > 88)
                index = 88;
        }
        Array.Copy(audioOutArray, 0, decodedData, writeOffset, csChunk);
        //Marshal.Copy(audioOutArray, 0, audioOut, csChunk);
    }

    // chat-gpt version
    private void Decode(byte[] data, int size, int channels)
    {
        int samplesPerChannel = size / (channels * 4); // Each sample is 4 bytes in IMA ADPCM
        _decodedData = new short[samplesPerChannel * channels]; //new short[size]; // was: new short[samplesPerChannel * channels];

        int outputIndex = 0;

        using (var ms = new MemoryStream(data))
        using (var reader = new BinaryReader(ms))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte code = reader.ReadByte();
                DecodeSample(code, _decodedData, outputIndex++);
            }
        }

        _samplesCount = outputIndex;
    }

    // chat-gpt version
    private void DecodeSample(byte code, short[] output, int outputIndex)
    {
        int step = StepTable[this._index];
        int delta = step >> 3;
        if ((code & 1) != 0) delta += step >> 2;
        if ((code & 2) != 0) delta += step >> 1;
        if ((code & 4) != 0) delta += step;
        if ((code & 8) != 0)
        {
            _sample -= delta;
            if (_sample < short.MinValue) _sample = short.MinValue;
        }
        else
        {
            _sample += delta;
            if (_sample > short.MaxValue) _sample = short.MaxValue;
        }

        output[outputIndex] = (short)_sample;

        this._index += IndexAdjustTable[code & 7];
        if (this._index < 0) this._index = 0;
        if (this._index >= NumSteps) this._index = NumSteps - 1;
    }

    public byte[] GetDecodedData()
    {
        // Konvertiere die dekodierten Daten in ein Byte-Array
        byte[] byteData = new byte[_decodedData.Length * sizeof(short)];
        Buffer.BlockCopy(_decodedData, 0, byteData, 0, byteData.Length);
        return byteData;
    }

    public int CSamples()
    {
        return _samplesCount;
    }
}