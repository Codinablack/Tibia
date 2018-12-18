using System;
using System.IO;
using System.Text;

namespace Tibia.Data.Providers.Ultimate
{
    public class BinaryWriter : IDisposable
    {
        private readonly System.IO.BinaryWriter _binaryWriter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BinaryWriter" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public BinaryWriter(Stream stream)
        {
            _binaryWriter = new System.IO.BinaryWriter(stream);
        }
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _binaryWriter?.Dispose();
        }

        /// <summary>
        ///     Writes the u int32.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteUInt32(uint value)
        {
            _binaryWriter.Write(value);
        }

        /// <summary>
        ///     Writes the int32.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteInt32(int value)
        {
            _binaryWriter.Write(value);
        }

        /// <summary>
        ///     Writes the u int16.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteUInt16(ushort value)
        {
            _binaryWriter.Write(value);
        }

        /// <summary>
        ///     Writes the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteByte(byte value)
        {
            _binaryWriter.Write(value);
        }

        /// <summary>
        ///     Writes the string.
        /// </summary>
        /// <param name="value">The value.</param>
        public void WriteString(string value)
        {
            WriteUInt16((ushort) value.Length);
            _binaryWriter.Write(Encoding.ASCII.GetBytes(value));
        }

        /// <summary>
        //     Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.
        /// </summary>
        public void Flush()
        {
            _binaryWriter.Flush();
        }
    }
}