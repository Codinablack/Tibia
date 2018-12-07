using System;
using System.Text;
using Tibia.Data;
using Tibia.Map;
using Tibia.Security.Cryptography;

namespace Tibia.Network
{
    public class NetworkMessage
    {
        public const int BufferSize = 16394;
        private byte[] _buffer;
        private int _length;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NetworkMessage" /> class.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        public NetworkMessage(Xtea xtea)
        {
            Xtea = xtea;
            Reset();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NetworkMessage" /> class.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        /// <param name="startIndex">The start index.</param>
        public NetworkMessage(Xtea xtea, int startIndex)
        {
            Xtea = xtea;
            Reset(startIndex);
        }

        /// <summary>
        ///     Gets the XTEA key.
        /// </summary>
        /// <value>
        ///     The XTEA key.
        /// </value>
        public Xtea Xtea { get; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public int Position { get; set; }

        /// <summary>
        ///     Gets or sets the length.
        /// </summary>
        /// <value>
        ///     The length.
        /// </value>
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        /// <summary>
        ///     Gets the buffer.
        /// </summary>
        /// <value>
        ///     The buffer.
        /// </value>
        public byte[] Buffer
        {
            get { return _buffer; }
        }

        /// <summary>
        ///     Replaces the bytes.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        public void ReplaceBytes(int index, byte[] value)
        {
            if (Length - index >= value.Length)
                Array.Copy(value, 0, Buffer, index, value.Length);
        }

        /// <summary>
        ///     Skips the bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <exception cref="IndexOutOfRangeException">Index is out of range.</exception>
        public void SkipBytes(int count)
        {
            if (Position + count > Length)
                throw new IndexOutOfRangeException("Index is out of range.");
            Position += count;
        }

        /// <summary>
        ///     Reads the packet.
        /// </summary>
        /// <returns>The buffer.</returns>
        public byte[] ReadPacket()
        {
            byte[] buffer = new byte[Length - 8];
            Array.Copy(Buffer, 8, buffer, 0, Length - 8);
            return buffer;
        }

        /// <summary>
        ///     Resets the specified start index.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        public void Reset(int startIndex)
        {
            _buffer = new byte[BufferSize];
            _length = startIndex;
            Position = startIndex;
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        public void Reset()
        {
            Reset(8);
        }

        /// <summary>
        ///     Reads the network protocol.
        /// </summary>
        /// <returns>The network protocol.</returns>
        public NetworkProtocol ReadNetworkProtocol()
        {
            return (NetworkProtocol) ReadByte();
        }

        /// <summary>
        ///     Reads the byte.
        /// </summary>
        /// <returns>The byte.</returns>
        /// <exception cref="IndexOutOfRangeException">Index is out of range.</exception>
        public byte ReadByte()
        {
            if (Position + 1 > Length)
                throw new IndexOutOfRangeException("Index is out of range.");

            return Buffer[Position++];
        }

        /// <summary>
        ///     Reads the boolean.
        /// </summary>
        /// <returns>The boolean.</returns>
        public bool ReadBoolean()
        {
            return BitConverter.ToBoolean(ReadBytes(1), 0);
        }

        /// <summary>
        ///     Reads the bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The bytes.</returns>
        /// <exception cref="IndexOutOfRangeException">Index is out of range.</exception>
        public byte[] ReadBytes(int count)
        {
            if (Position + count > Length)
                throw new IndexOutOfRangeException("Index is out of range.");

            byte[] buffer = new byte[count];
            Array.Copy(Buffer, Position, buffer, 0, count);
            Position += count;
            return buffer;
        }

        /// <summary>
        ///     Reads the string.
        /// </summary>
        /// <returns>The string.</returns>
        public string ReadString()
        {
            ushort length = ReadUInt16();
            string value = Encoding.Default.GetString(Buffer, Position, length);
            Position += length;
            return value;
        }

        /// <summary>
        ///     Reads the unsigned short integer.
        /// </summary>
        /// <returns>The unsigned short integer.</returns>
        public ushort ReadUInt16()
        {
            return BitConverter.ToUInt16(ReadBytes(2), 0);
        }

        /// <summary>
        ///     Reads the unsigned integer.
        /// </summary>
        /// <returns>The unsigned integer.</returns>
        public uint ReadUInt32()
        {
            return BitConverter.ToUInt32(ReadBytes(4), 0);
        }

        /// <summary>
        ///     Adds the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="Exception">NetworkMessage Buffer is full.</exception>
        public void AddByte(byte value)
        {
            if (Length + 1 > BufferSize)
                throw new Exception("NetworkMessage Buffer is full.");

            AddBytes(new[] { value });
        }

        /// <summary>
        ///     Adds the bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="Exception">NetworkMessage Buffer is full.</exception>
        public void AddBytes(byte[] value)
        {
            if (value.Length + Length > BufferSize)
                throw new Exception("NetworkMessage Buffer is full.");

            Array.Copy(value, 0, Buffer, Position, value.Length);
            Position += value.Length;

            if (Position > Length)
                _length = Position;
        }

        /// <summary>
        ///     Adds the string.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddString(string value)
        {
            AddUInt16((ushort) value.Length);
            AddBytes(Encoding.Default.GetBytes(value));
        }

        /// <summary>
        ///     Adds the unsigned short integer.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddUInt16(ushort value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        /// <summary>
        ///     Adds the boolean.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void AddBoolean(bool value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        /// <summary>
        ///     Adds the double.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="precision">The precision.</param>
        public void AddDouble(double value, byte precision = 2)
        {
            AddByte(precision);
            AddUInt32((uint) (value * Math.Pow(10, precision)) + int.MaxValue);
        }

        /// <summary>
        ///     Adds the unsigned integer.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddUInt32(uint value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        /// <summary>
        ///     Adds the unsigned long integer.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddUInt64(ulong value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        /// <summary>
        ///     Adds the padding bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        public void AddPaddingBytes(int count)
        {
            Position += count;

            if (Position > Length)
                _length = Position;
        }

        /// <summary>
        ///     Peeks the byte.
        /// </summary>
        /// <returns>The byte.</returns>
        public byte PeekByte()
        {
            return Buffer[Position];
        }

        /// <summary>
        ///     Peeks the bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The bytes.</returns>
        public byte[] PeekBytes(int count)
        {
            byte[] buffer = new byte[count];
            Array.Copy(Buffer, Position, buffer, 0, count);
            return buffer;
        }

        /// <summary>
        ///     Peeks the unsigned short integer.
        /// </summary>
        /// <returns>The unsigned short integer.</returns>
        public ushort PeekUInt16()
        {
            return BitConverter.ToUInt16(PeekBytes(2), 0);
        }

        /// <summary>
        ///     Peeks the unsigned integer.
        /// </summary>
        /// <returns>The unsigned integer.</returns>
        public uint PeekUInt32()
        {
            return BitConverter.ToUInt32(PeekBytes(4), 0);
        }

        /// <summary>
        ///     Peeks the string.
        /// </summary>
        /// <returns>The string.</returns>
        public string PeekString()
        {
            ushort length = PeekUInt16();
            return Encoding.ASCII.GetString(PeekBytes(length + 2), 2, length);
        }

        /// <summary>
        ///     Decrypts the RSA.
        /// </summary>
        /// <returns>Whether the decryption is completed successfully.</returns>
        public bool DecryptRsa()
        {
            return Rsa.Decrypt(ref _buffer, Position, Length);
        }

        /// <summary>
        ///     Reads the XTEA key.
        /// </summary>
        /// <returns>The XTEA key.</returns>
        public Xtea ReadXteaKey()
        {
            uint[] key = new uint[4];
            key[0] = ReadUInt32();
            key[1] = ReadUInt32();
            key[2] = ReadUInt32();
            key[3] = ReadUInt32();

            return new Xtea(key);
        }

        /// <summary>
        ///     Decrypts the buffer with the specified XTEA key.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        /// <returns>Whether the decryption is completed successfully.</returns>
        public bool Decrypt(Xtea xtea)
        {
            return Xtea.Decrypt(ref _buffer, ref _length, 6, xtea.Key);
        }

        /// <summary>
        ///     Encrypts the buffer with the specified XTEA key.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        /// <returns>Whether the encryption is completed successfully.</returns>
        public bool Encrypt(Xtea xtea)
        {
            return Xtea.Encrypt(ref _buffer, ref _length, 6, xtea.Key);
        }

        /// <summary>
        ///     Determines whether [has valid Adler-32 checksum].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [has valid Adler-32 checksum]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasValidAdler32Checksum()
        {
            return AdlerChecksum.Calculate(ref _buffer, 6, Length) == ReadAdler32Checksum();
        }

        /// <summary>
        ///     Inserts the Adler-32 checksum.
        /// </summary>
        public void InsertAdler32Checksum()
        {
            Array.Copy(BitConverter.GetBytes(AdlerChecksum.Calculate(ref _buffer, 6, _length)), 0, _buffer, 2, 4);
        }

        /// <summary>
        ///     Reads the Adler-32 checksum.
        /// </summary>
        /// <returns>The Adler-32 checksum.</returns>
        private uint ReadAdler32Checksum()
        {
            return BitConverter.ToUInt32(Buffer, 2);
        }

        /// <summary>
        ///     Inserts the length of the packet.
        /// </summary>
        private void InsertPacketLength()
        {
            Array.Copy(BitConverter.GetBytes((ushort) (Length - 8)), 0, Buffer, 6, 2);
        }

        /// <summary>
        ///     Inserts the total length.
        /// </summary>
        private void InsertTotalLength()
        {
            Array.Copy(BitConverter.GetBytes((ushort) (Length - 2)), 0, Buffer, 0, 2);
        }

        /// <summary>
        ///     Prepares to send.
        /// </summary>
        /// <returns>Whether the message is prepared to send.</returns>
        public bool PrepareToSend()
        {
            InsertPacketLength();
            InsertAdler32Checksum();
            InsertTotalLength();
            return true;
        }

        /// <summary>
        ///     Prepares to send.
        /// </summary>
        /// <param name="xtea">The xtea.</param>
        /// <returns>Whether the message is prepared to send.</returns>
        public bool PrepareToSend(Xtea xtea)
        {
            // Must be before Xtea, because the packet Length is encrypted as well
            InsertPacketLength();

            if (!Encrypt(xtea))
                return false;

            // Must be after Xtea, because takes the checksum of the encrypted packet
            InsertAdler32Checksum();
            InsertTotalLength();
            return true;
        }

        /// <summary>
        ///     Prepares to read.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        /// <returns>Whether the stream is prepared to read.</returns>
        public bool PrepareToRead(Xtea xtea)
        {
            if (!Decrypt(xtea))
                return false;

            Position = 8;
            return true;
        }

        /// <summary>
        ///     Adds the appearance.
        /// </summary>
        /// <param name="outfit">The outfit.</param>
        /// <param name="mount">The mount.</param>
        public void AddAppearance(IOutfit outfit, IMount mount)
        {
            AddUInt16(outfit.SpriteId);
            if (outfit.SpriteId != 0)
            {
                AddByte(outfit.Head);
                AddByte(outfit.Body);
                AddByte(outfit.Legs);
                AddByte(outfit.Feet);
                AddByte(outfit.Addons);
            }
            else
            {
                AddUInt16(outfit.Item);
            }
            AddUInt16(mount?.SpriteId ?? 0);
        }

        /// <summary>
        ///     Adds the outfit.
        /// </summary>
        /// <param name="outfit">The outfit.</param>
        public void AddOutfit(IOutfit outfit)
        {
            // TODO: Change Type to ushort
            AddUInt16(outfit.SpriteId);
            AddString(outfit.Name);
            AddByte(outfit.Addons);
        }

        /// <summary>
        ///     Adds the mount.
        /// </summary>
        /// <param name="mount">The mount.</param>
        public void AddMount(IMount mount)
        {
            // TODO: Change Type to ushort
            AddUInt16(mount.SpriteId);
            AddString(mount.Name);
        }

        /// <summary>
        ///     Adds the quest.
        /// </summary>
        /// <param name="questInfo">The quest information.</param>
        public void AddQuest(IQuestInfo questInfo)
        {
            // TODO: Change QuestId to ushort
            AddUInt16((ushort) questInfo.QuestId);
            AddString(questInfo.Quest.Name);
            AddBoolean(questInfo.IsComplete);
        }

        /// <summary>
        ///     Reads the vector3.
        /// </summary>
        /// <returns>The vector3.</returns>
        public Vector3 ReadVector3()
        {
            return new Vector3(ReadUInt16(), ReadUInt16(), ReadByte());
        }

        /// <summary>
        ///     Reads the vector2.
        /// </summary>
        /// <returns>The vector2.</returns>
        public Vector2 ReadVector2()
        {
            return new Vector2(ReadUInt16(), ReadUInt16());
        }
    }
}