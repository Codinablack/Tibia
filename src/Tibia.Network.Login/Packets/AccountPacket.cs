using Tibia.InteropServices;
using Tibia.Security.Cryptography;

namespace Tibia.Network.Login.Packets
{
    public sealed class AccountPacket : IClientPacket
    {
        /// <summary>
        ///     Gets the XTEA key.
        /// </summary>
        /// <value>
        ///     The XTEA key.
        /// </value>
        public Xtea Xtea { get; private set; }

        /// <summary>
        ///     Gets the operating system platform.
        /// </summary>
        /// <value>
        ///     The operating system platform.
        /// </value>
        public OSPlatform OSPlatform { get; private set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public ushort Version { get; private set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Username { get; private set; }

        /// <summary>
        ///     Gets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has a valid RSA.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has a valid RSA; otherwise, <c>false</c>.
        /// </value>
        public bool IsRsaValid { get; private set; }

        /// <summary>
        ///     Gets the protocol version.
        /// </summary>
        /// <value>
        ///     The protocol version.
        /// </value>
        public uint ProtocolVersion { get; private set; }

        /// <summary>
        ///     Gets the dat signature.
        /// </summary>
        /// <value>
        ///     The dat signature.
        /// </value>
        public uint DatSignature { get; private set; }

        /// <summary>
        ///     Gets the SPR signature.
        /// </summary>
        /// <value>
        ///     The SPR signature.
        /// </value>
        public uint SprSignature { get; private set; }

        /// <summary>
        ///     Gets the pic signature.
        /// </summary>
        /// <value>
        ///     The pic signature.
        /// </value>
        public uint PicSignature { get; private set; }

        /// <summary>
        ///     Gets or sets the unknown value.
        /// </summary>
        /// <value>
        ///     The unknown value.
        /// </value>
        public byte UnknownValue2 { get; set; }

        /// <summary>
        ///     Gets or sets the unknown value.
        /// </summary>
        /// <value>
        ///     The unknown value.
        /// </value>
        public byte UnknownValue1 { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static AccountPacket Parse(NetworkMessage message)
        {
            return new AccountPacket
            {
                OSPlatform = message.ReadOSPlatform(),
                Version = message.ReadUInt16(),
                ProtocolVersion = message.ReadUInt32(),
                DatSignature = message.ReadUInt32(),
                SprSignature = message.ReadUInt32(),
                PicSignature = message.ReadUInt32(),
                UnknownValue1 = message.ReadByte(), // TODO: Value should be always zero?
                IsRsaValid = message.DecryptRsa(),
                UnknownValue2 = message.ReadByte(), // TODO: Value should be always zero?
                Xtea = message.ReadXteaKey(),
                Username = message.ReadString(),
                Password = message.ReadString()
            };
        }
    }
}