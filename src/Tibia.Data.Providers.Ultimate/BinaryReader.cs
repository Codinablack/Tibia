using System;
using System.IO;
using System.Text;
using Tibia.Map;

namespace Tibia.Data.Providers.Ultimate
{
    public class BinaryReader : IDisposable
    {
        private readonly System.IO.BinaryReader _binaryReader;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BinaryReader" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public BinaryReader(Stream stream)
        {
            _binaryReader = new System.IO.BinaryReader(stream);
        }
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _binaryReader?.Dispose();
        }

        /// <summary>
        ///     Reads the vector2.
        /// </summary>
        /// <returns>The vector2.</returns>
        public IVector2 ReadVector2()
        {
            return new Vector2(ReadUInt16(), ReadUInt16());
        }

        /// <summary>
        ///     Reads the vector3.
        /// </summary>
        /// <returns>The vector3.</returns>
        public IVector3 ReadVector3()
        {
            return new Vector3(ReadUInt16(), ReadUInt16(), ReadByte());
        }

        /// <summary>
        ///     Reads the string.
        /// </summary>
        /// <returns>The string.</returns>
        public string ReadString()
        {
            ushort length = _binaryReader.ReadUInt16();
            return Encoding.ASCII.GetString(_binaryReader.ReadBytes(length));
        }

        /// <summary>
        ///     Reads the unsigned short integer.
        /// </summary>
        /// <returns>The unsigned short integer.</returns>
        public ushort ReadUInt16()
        {
            return _binaryReader.ReadUInt16();
        }

        /// <summary>
        ///     Reads the unsigned integer.
        /// </summary>
        /// <returns>The unsigned integer.</returns>
        public uint ReadUInt32()
        {
            return _binaryReader.ReadUInt32();
        }

        /// <summary>
        ///     Reads the integer.
        /// </summary>
        /// <returns>The integer.</returns>
        public int ReadInt32()
        {
            return _binaryReader.ReadInt32();
        }

        /// <summary>
        ///     Reads the byte.
        /// </summary>
        /// <returns>The byte.</returns>
        public byte ReadByte()
        {
            return _binaryReader.ReadByte();
        }
    }
}