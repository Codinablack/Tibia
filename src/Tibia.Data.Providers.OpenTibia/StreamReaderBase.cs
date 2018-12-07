using System;
using System.IO;
using System.Text;

namespace Tibia.Data.Providers.OpenTibia
{
    public abstract class StreamReaderBase : IStreamReader
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamReaderBase" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        protected StreamReaderBase(Stream stream)
        {
            Reader = new BinaryReader(stream);
        }

        /// <summary>
        ///     Gets the reader.
        /// </summary>
        /// <value>
        ///     The reader.
        /// </value>
        protected virtual BinaryReader Reader { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Peeks the character.
        /// </summary>
        /// <returns>The peaked character.</returns>
        public virtual int PeekChar()
        {
            return Reader.PeekChar();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Reads the byte.
        /// </summary>
        /// <returns>The byte.</returns>
        public virtual byte ReadByte()
        {
            return Reader.ReadByte();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Reads the unsigned short integer.
        /// </summary>
        /// <returns>The unsigned short integer.</returns>
        public virtual ushort ReadUInt16()
        {
            return Reader.ReadUInt16();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Reads the single.
        /// </summary>
        /// <returns>The single.</returns>
        public virtual float ReadSingle()
        {
            return Reader.ReadSingle();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Reads the unsigned integer.
        /// </summary>
        /// <returns>The unsigned integer.</returns>
        public virtual uint ReadUInt32()
        {
            return Reader.ReadUInt32();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Reads the integer.
        /// </summary>
        /// <returns>The integer.</returns>
        public virtual int ReadInt32()
        {
            return Reader.ReadInt32();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Reads the string.
        /// </summary>
        /// <returns>The string.</returns>
        public virtual string ReadString()
        {
            ushort length = ReadUInt16();
            return CanSkip(length) ? Encoding.ASCII.GetString(Reader.ReadBytes(length)) : null;
        }

        /// <summary>
        ///     Reads the bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The bytes.</returns>
        public virtual byte[] ReadBytes(int count)
        {
            return Reader.ReadBytes(count);
        }

        /// <summary>
        ///     Gets the size of the stream.
        /// </summary>
        /// <returns>The size of the stream.</returns>
        protected virtual long GetStreamSize()
        {
            return Reader.BaseStream.Length - Reader.BaseStream.Position;
        }

        /// <summary>
        ///     Skips the specified byte count.
        /// </summary>
        /// <param name="byteCount">The byte count.</param>
        public virtual void Skip(long byteCount)
        {
            if (!CanSkip(byteCount))
                throw new ArgumentOutOfRangeException(nameof(byteCount), byteCount, "Cannot read byte count from stream");

            Reader.BaseStream.Seek(byteCount, SeekOrigin.Current);
        }

        /// <summary>
        ///     Determines whether this instance can skip the specified byte count.
        /// </summary>
        /// <param name="byteCount">The byte count.</param>
        /// <returns>
        ///     <c>true</c> if this instance can skip the specified byte count; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanSkip(long byteCount)
        {
            return GetStreamSize() >= byteCount;
        }
    }
}