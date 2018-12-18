using System;

namespace Tibia.Security.Cryptography
{
    public class AdlerChecksum : IEquatable<AdlerChecksum>
    {
        /// <summary>
        ///     Represents the Adler-32 checksum algorithm parameter for the base. This field is constant.
        /// </summary>
        public const uint AdlerBase = 0xFFF1;

        /// <summary>
        ///     Represents the Adler-32 checksum algorithm parameter for the start. This field is constant.
        /// </summary>
        public const uint AdlerStart = 0x0001;

        /// <summary>
        ///     Represents the Adler-32 checksum algorithm parameter for the buffer. This field is constant.
        /// </summary>
        public const uint AdlerBuffer = 0x0400;

        /// <summary>
        ///     Get Adler-32 checksum value for the last calculation.
        /// </summary>
        public uint ChecksumValue { get; private set; }
        /// <summary>
        ///     Determines whether this instance and another specified
        ///     <see cref="T:Tibia.Security.Cryptography.AdlerChecksum" /> object have the same value.
        /// </summary>
        /// <param name="value">The checksum to compare to this instance.</param>
        /// <returns>
        ///     <see langword="true" /> if the value of the <paramref name="value" /> parameter is the same as the value of
        ///     this instance; otherwise, <see langword="false" />. If <paramref name="value" /> is <see langword="null" />, the
        ///     method returns <see langword="false" />.
        /// </returns>
        public bool Equals(AdlerChecksum value)
        {
            if (value == null || GetType() != value.GetType())
                return false;

            return ChecksumValue == value.ChecksumValue;
        }

        /// <summary>
        ///     Calculate Adler-32 checksum for buffer.
        /// </summary>
        /// <param name="bytesBuff">Byte array for checksum calculation.</param>
        /// <returns>Returns true if the checksum values is successflly calculated</returns>
        public bool Calculate(byte[] bytesBuff)
        {
            ChecksumValue = Calculate(ref bytesBuff, 0, bytesBuff.Length);
            return true;
        }

        /// <summary>
        ///     Determines whether this instance and another specified object, which also must be a <see cref="AdlerChecksum" />
        ///     object, have the same value.
        /// </summary>
        /// <param name="obj">The checksum to compare to this instance.</param>
        /// <returns>
        ///     <see langword="true" /> is a <see cref="AdlerChecksum" /> and its value is the same as this instance;
        ///     otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns
        ///     <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            AdlerChecksum other = (AdlerChecksum) obj;
            return ChecksumValue == other.ChecksumValue;
        }

        /// <summary>
        ///     Determines whether two specified <see cref="AdlerChecksum" /> objects have the same value.
        /// </summary>
        /// <param name="a">The first <see cref="AdlerChecksum" /> to compare, or <see langword="null" />.</param>
        /// <param name="b">The second <see cref="AdlerChecksum" /> to compare, or <see langword="null" />.</param>
        /// <returns>
        ///     <see langword="true" /> if the value of <paramref name="a" /> is the same as the value of
        ///     <paramref name="b" />; otherwise, <see langword="false" />. If both <paramref name="a" /> and <paramref name="b" />
        ///     are null, the method returns <see langword="true" />.
        /// </returns>
        public static bool operator ==(AdlerChecksum a, AdlerChecksum b)
        {
            return Equals(a, null) || Equals(b, null) || a.Equals(b);
        }

        /// <summary>
        ///     Determines whether two specified <see cref="AdlerChecksum" /> objects have different values.
        /// </summary>
        /// <param name="a">The first <see cref="AdlerChecksum" /> to compare, or <see langword="null" />.</param>
        /// <param name="b">The second <see cref="AdlerChecksum" /> to compare, or <see langword="null" />.</param>
        /// <returns>
        ///     <see langword="true" /> if the value of <paramref name="a" /> is different from the value of
        ///     <paramref name="b" />; otherwise, <see langword="false" />.
        /// </returns>
        public static bool operator !=(AdlerChecksum a, AdlerChecksum b)
        {
            return !(a == b);
        }

        /// <summary>
        ///     Returns the hash code for this <see cref="AdlerChecksum" />.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return ChecksumValue.GetHashCode();
        }

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ChecksumValue.ToString();
        }

        /// <summary>
        ///     Adds the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <returns>The modified packet.</returns>
        public static byte[] Add(byte[] packet)
        {
            byte[] newPacket = new byte[packet.Length + 4];
            Array.Copy(packet, 2, newPacket, 6, packet.Length - 2);
            Array.Copy(BitConverter.GetBytes((ushort) newPacket.Length - 2), newPacket, 2);
            Array.Copy(BitConverter.GetBytes(Calculate(ref newPacket, 6, newPacket.Length)), 0, newPacket, 2, 4);

            return newPacket;
        }

        /// <summary>
        ///     Calculates a checksum for the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="index">The index.</param>
        /// <param name="length">The length.</param>
        /// <returns>The checksum.</returns>
        public static uint Calculate(ref byte[] buffer, int index, int length)
        {
            if (buffer == null || length - index <= 0)
                return 0;

            uint unSum1 = AdlerStart & 0xFFFF;
            uint unSum2 = (AdlerStart >> 16) & 0xFFFF;

            for (int i = index; i < length; i++)
            {
                unSum1 = (unSum1 + buffer[i]) % AdlerBase;
                unSum2 = (unSum1 + unSum2) % AdlerBase;
            }

            return (unSum2 << 16) + unSum1;
        }
    }
}