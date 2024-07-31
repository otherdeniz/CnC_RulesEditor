using System;
using System.IO;
using TagLib.Riff;

namespace Deniz.CCAudioPlayerCore
{
    public class StupidStream : Stream
    {
        private readonly byte[] _buffer;
        private int _position;

        public static StupidStream FromFileStream(FileStream fileStream)
        {
            using (var memStr = new MemoryStream())
            {
                fileStream.CopyTo(memStr);
                memStr.Position = 0;
                return new StupidStream(memStr.ToArray());
            }
        }

        public StupidStream(byte[] buffer)
        {
            _buffer = buffer;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int n = _buffer.Length - _position;
            if (n > count)
                n = count;
            if (n <= 0)
                return 0;

            if (n <= 8)
            {
                int byteCount = _buffer.Length - _position;
                while (--byteCount >= 0)
                    buffer[offset + byteCount] = _buffer[_position + byteCount];
            }
            else
                Buffer.BlockCopy(_buffer, _position, buffer, offset, n);
            _position += n;

            return n;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead => true;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _buffer.Length;

        public override long Position
        {
            get => _position;
            set => _position = Convert.ToInt32(value);
        }
    }
}
