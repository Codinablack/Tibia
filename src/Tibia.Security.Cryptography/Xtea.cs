using System;

namespace Tibia.Security.Cryptography
{
    public class Xtea
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Xtea" /> class.
        /// </summary>
        /// <param name="key">The buffer.</param>
        public Xtea(uint[] key)
        {
            Key = key;
        }

        /// <summary>
        ///     Gets the key.
        /// </summary>
        /// <value>
        ///     The key.
        /// </value>
        public uint[] Key { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="Xtea" /> class.
        /// </summary>
        /// <returns>The new instance.</returns>
        public static Xtea Create()
        {
            return new Xtea(new uint[4]);
        }

        /// <summary>
        ///     Encrypts the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="length">The length.</param>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <returns>Whether the buffer is encrypted successfully.</returns>
        public static unsafe bool Encrypt(ref byte[] buffer, ref int length, int index, uint[] key)
        {
            if (key == null)
                return false;

            int msgSize = length - index;

            int pad = msgSize % 8;
            if (pad > 0)
            {
                msgSize += 8 - pad;
                length = index + msgSize;
            }

            fixed (byte* bufferPtr = buffer)
            {
                uint* words = (uint*) (bufferPtr + index);

                for (int pos = 0; pos < msgSize / 4; pos += 2)
                {
                    uint xSum = 0;
                    const uint xDelta = 0x9E3779B9;
                    uint xCount = 32;

                    while (xCount-- > 0)
                    {
                        words[pos] += (((words[pos + 1] << 4) ^ (words[pos + 1] >> 5)) + words[pos + 1]) ^ (xSum + key[xSum & 3]);
                        xSum += xDelta;
                        words[pos + 1] += (((words[pos] << 4) ^ (words[pos] >> 5)) + words[pos]) ^ (xSum + key[(xSum >> 11) & 3]);
                    }
                }
            }

            return true;
        }

        /// <summary>
        ///     Decrypts the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="length">The length.</param>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <returns>Whether the buffer is decrypted successfully.</returns>
        public static unsafe bool Decrypt(ref byte[] buffer, ref int length, int index, uint[] key)
        {
            if (length <= index || (length - index) % 8 > 0 || key == null)
                return false;

            fixed (byte* bufferPtr = buffer)
            {
                uint* words = (uint*) (bufferPtr + index);
                int msgSize = length - index;

                for (int pos = 0; pos < msgSize / 4; pos += 2)
                {
                    uint xCount = 32;
                    uint xSum = 0xC6EF3720;
                    const uint xDelta = 0x9E3779B9;

                    while (xCount-- > 0)
                    {
                        words[pos + 1] -= (((words[pos] << 4) ^ (words[pos] >> 5)) + words[pos]) ^ (xSum + key[(xSum >> 11) & 3]);
                        xSum -= xDelta;
                        words[pos] -= (((words[pos + 1] << 4) ^ (words[pos + 1] >> 5)) + words[pos + 1]) ^ (xSum + key[xSum & 3]);
                    }
                }
            }

            length = BitConverter.ToUInt16(buffer, index) + 2 + index;
            return true;
        }
    }
}