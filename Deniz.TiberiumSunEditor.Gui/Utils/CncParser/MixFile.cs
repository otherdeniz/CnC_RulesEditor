using System.Text;

namespace Deniz.TiberiumSunEditor.Gui.Utils.CncParser
{
    /// <summary>
    /// A Tiberian Sun / Red Alert 2 type MIX file.
    /// </summary>
    public class MixFile
    {

        private const int INDEX_POSITION = 10;

        private readonly MixFile? _masterMix; // The MIX file that this MIX file resides in, if any.
        private readonly object _locker = new object();
        private List<MixFileContent>? _contentList;
        private int _bodyOffset;
        private Stream? _stream;

        /// <summary>
        /// The start offset for this MIX file when it's inside another MIX file.
        /// Should be zero if this MIX file is not inside another MIX file.
        /// </summary>
        private readonly int _mixStartOffset = 0;

        public MixFile(string name)
        {
            Name = name;
        }

        public MixFile(string name, MixFile masterMix, int startOffset)
        {
            Name = name;
            _masterMix = masterMix;
            _mixStartOffset = startOffset;

            if (masterMix != null)
            {
                _mixStartOffset += masterMix._bodyOffset;
            }
        }

        public string Name { get; }

        public string FilePath { get; private set; }

        public List<MixFileEntry> Entries { get; private set; }

        public MixFile? MasterMix => _masterMix;

        /// <summary>
        /// Reads MIX file information from a MIX file in the given file system path.
        /// </summary>
        /// <param name="path">The path to the MIX file.</param>
        public void Parse(string path)
        {
            if (_masterMix != null)
                throw new InvalidOperationException("Can't parse from a file when the MIX file is inside another MIX file.");

            FilePath = path;

            using (FileStream fileStream = File.OpenRead(path))
            {
                Parse(fileStream);
            }
        }

        /// <summary>
        /// Reads MIX file entry information from a stream.
        /// </summary>
        /// <param name="stream">The stream. Can be null for MIX files that
        /// reside inside another MIX file.</param>
        public void Parse(Stream? stream = null)
        {
            if (_masterMix != null)
            {
                _masterMix.OpenFile();
                stream = _stream = _masterMix._stream;
                if (stream != null)
                {
                    stream.Position = _mixStartOffset;
                }
            }

            if (stream == null || stream.Length < INDEX_POSITION)
                return;

            Entries = new List<MixFileEntry>();

            byte[] buffer = new byte[256];
            stream.Read(buffer, 0, 4);
            MixType mixType = (MixType)BitConverter.ToInt32(buffer, 0);
            
            bool isEncrypted = (mixType & MixType.ENCRYPTED) != 0;

            if (isEncrypted)
            {
                // Read and decrypt the Blowfish associated with this MIX.
                stream.Read(buffer, 0, KeyDecryptor.SIZE_OF_ENCRYPTED_KEY);
                stream = new BlowfishStream(stream, KeyDecryptor.DecryptBlowfishKey(buffer));
            }

            stream.Read(buffer, 0, MixFileHeader.SIZE_OF_HEADER);

            MixFileHeader header = new MixFileHeader(buffer);

            _bodyOffset = INDEX_POSITION + MixFileEntry.SIZE_OF_FILE_ENTRY * header.FileCount;

            if (isEncrypted)
            {
                // Account for Blowfish key and padding.
                _bodyOffset += KeyDecryptor.SIZE_OF_ENCRYPTED_KEY;
                _bodyOffset += (header.FileCount % 2) == 0 ? 2 : 6;
            }

            for (int i = 0; i < header.FileCount; i++)
            {
                if (stream.Position + MixFileEntry.SIZE_OF_FILE_ENTRY >= stream.Length)
                    throw new MixParseException("Invalid MIX file.");

                stream.Read(buffer, 0, MixFileEntry.SIZE_OF_FILE_ENTRY);
                Entries.Add(new MixFileEntry(buffer));
            }

            if (_masterMix != null)
                _masterMix.CloseFile();
        }

        /// <summary>
        /// Opens the MIX file for performing one or more read operations.
        /// </summary>
        public void OpenFile()
        {
            if (_masterMix != null)
            {
                _masterMix.OpenFile();
                _stream = _masterMix._stream;
                return;
            }

            if (FilePath == null)
                throw new MixParseException("No MIX file path defined!");

            if (_stream == null || !_stream.CanRead)
                _stream = File.OpenRead(FilePath);
        }

        public MixFileEntry? GetEntry(uint id)
        {
            return Entries.FirstOrDefault(e => e.Identifier == id);
        }

        public List<MixFileContent> GetFileContents()
        {
            if (_contentList != null)
            {
                return _contentList;
            }

            _contentList = new List<MixFileContent>();
            var localMixDbId = GetFileID("local mix database.dat");
            var localMixDbEntry = GetEntry(localMixDbId);
            if (localMixDbEntry != null)
            {
                using var fileStream = new MemoryStream(GetSingleFileData(localMixDbEntry.Offset, localMixDbEntry.Size));
                fileStream.Position = 52;
                var byteBuffer = new byte[1];
                var entryBuffer = new List<byte>();
                while (fileStream.Read(byteBuffer, 0, byteBuffer.Length) > 0)
                {
                    if (byteBuffer[0] == 0)
                    {
                        if (entryBuffer.Count > 0)
                        {
                            // read entry
                            var fileName = Encoding.UTF8.GetString(entryBuffer.ToArray());
                            var entry = Entries.FirstOrDefault(e => e.Identifier == GetFileID(fileName));
                            if (entry != null)
                            {
                                _contentList.Add(new MixFileContent(entry.Identifier, fileName, 
                                    new FileLocationInfo(this, entry.Offset, entry.Size)));
                            }
                        }
                        entryBuffer = new List<byte>();
                    }
                    else
                    {
                        entryBuffer.Add(byteBuffer[0]);
                    }
                }
            }
            else
            {
                _contentList = Entries
                    .Select(e => new MixFileContent(e.Identifier, MixEntryNames.Instance.GetName(e.Identifier), 
                        new FileLocationInfo(this, e.Offset, e.Size)))
                    .Where(n => !string.IsNullOrEmpty(n.FileName))
                    .ToList();
            }

            return _contentList;
        }

        /// <summary>
        /// Closes the MIX file.
        /// </summary>
        public void CloseFile()
        {
            _stream.Close();
        }

        /// <summary>
        /// Gets data for a single file from the MIX file.
        /// </summary>
        /// <param name="offset">The start offset from the MIX body.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A byte array.</returns>
        public byte[] GetSingleFileData(int offset, int count)
        {
            var lockObject = GetLockObject();

            lock (lockObject)
            {
                OpenFile();
                byte[] buffer = GetFileData(offset, count);
                CloseFile();
                return buffer;
            }
        }

        /// <summary>
        /// Gets file data from the MIX file.
        /// </summary>
        /// <param name="offset">The start offset from the MIX body.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A byte array.</returns>
        private byte[] GetFileData(int offset, int count)
        {
            byte[] buffer = new byte[count];

            _stream.Position = _mixStartOffset + _bodyOffset + offset;
            _stream.Read(buffer, 0, count);

            return buffer;
        }

        private object GetLockObject()
        {
            return _masterMix != null 
                ? _masterMix.GetLockObject() 
                : _locker;
        }

        /// <summary>
        /// Calculates and returns the internal ID of a file based on its name.
        /// The ID is needed when finding files from inside MIX files.
        /// </summary>
        /// <param name="fileName">The filename.</param>
        /// <returns></returns>
        public static uint GetFileID(string fileName)
        {
            fileName = fileName.ToUpperInvariant();
            int a = fileName.Length >> 2;
            if ((fileName.Length & 3) > 0)
            {
                fileName += (char)(fileName.Length - (a << 2));
                int i = 3 - ((fileName.Length - 1) & 3);
                while (i-- > 0)
                    fileName = fileName + fileName[a << 2];
            }

            return MixCRC.GetCRC(Encoding.ASCII.GetBytes(fileName));
        }
    }

    /// <summary>
    /// The type of a MIX file.
    /// </summary>
    [Flags]
    public enum MixType
    {
        DEFAULT = 0,
        CHECKSUMMED = 0x00010000,
        ENCRYPTED = 0x00020000
    }

    /// <summary>
    /// A MIX file header. Contains information on the number of files and the size
    /// of the body of the MIX file.
    /// </summary>
    public struct MixFileHeader
    {
        public const int SIZE_OF_HEADER = 6;

        public MixFileHeader(byte[] buffer)
        {
            if (buffer.Length < SIZE_OF_HEADER)
                throw new ArgumentException("buffer is not long enough");

            FileCount = BitConverter.ToInt16(buffer, 0);
            BodySize = BitConverter.ToInt32(buffer, 2);
        }

        /// <summary>
        /// The number of files in the MIX file.
        /// </summary>
        public short FileCount { get; private set; }

        /// <summary>
        /// The size of the MIX file, excluding the header and index.
        /// </summary>
        public int BodySize { get; private set; }
    }

    /// <summary>
    /// Contains information on a file stored inside a MIX file.
    /// </summary>
    public class MixFileEntry
    {
        public const int SIZE_OF_FILE_ENTRY = 12;

        public MixFileEntry(byte[] buffer)
        {
            if (buffer.Length < SIZE_OF_FILE_ENTRY)
                throw new ArgumentException("buffer is not long enough");

            Identifier = BitConverter.ToUInt32(buffer, 0);
            Offset = BitConverter.ToInt32(buffer, 4);
            Size = BitConverter.ToInt32(buffer, 8);
        }

        /// <summary>
        /// The identifier used to identify the file instead of a normal name.
        /// </summary>
        public uint Identifier { get; private set; }

        /// <summary>
        /// The offset of the file, from the start of the body.
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// The size of the file.
        /// </summary>
        public int Size { get; private set; }
    }

    public struct FileOffsetInfo
    {

    }

    public class MixParseException : Exception
    {
        public MixParseException(string message) : base(message)
        {
        }
    }
}
