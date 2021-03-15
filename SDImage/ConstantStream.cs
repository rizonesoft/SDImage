using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDImager
{
    public class ConstantStream : Stream
    {
        private byte m_byte;

        public ConstantStream(byte Byte)
        {
            m_byte = Byte;
        }

        public ConstantStream() : this(0) { }

        public override void Write(byte[] buffer, int offset, int count) { }

        public override int Read(byte[] buffer, int offset, int count)
        {
            for (int i = offset; i < offset + count; i++)
                buffer[i] = m_byte;
            return count;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override int ReadByte()
        {
            return m_byte;
        }

        public override void WriteByte(byte value) { }

        public override long Length
        {
            get { return long.MaxValue; }
        }

        public override void Flush()
        {

        }

        public override long Position
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {

        }
    }
}
