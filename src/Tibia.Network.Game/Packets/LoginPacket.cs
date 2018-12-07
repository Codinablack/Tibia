using System;
using Tibia.InteropServices;
using Tibia.Security.Cryptography;

namespace Tibia.Network.Game.Packets
{
    public sealed class LoginPacket : IClientPacket
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
        ///     Gets the client version.
        /// </summary>
        /// <value>
        ///     The client version.
        /// </value>
        public uint ClientVersion { get; private set; }

        /// <summary>
        ///     Gets the type of the client.
        /// </summary>
        /// <value>
        ///     The type of the client.
        /// </value>
        public byte ClientType { get; private set; }

        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public ushort Version { get; private set; }

        /// <summary>
        ///     Gets the dat revision.
        /// </summary>
        /// <value>
        ///     The dat revision.
        /// </value>
        public ushort DatRevision { get; private set; }

        /// <summary>
        ///     Gets the username.
        /// </summary>
        /// <value>
        ///     The username.
        /// </value>
        public string Username { get; private set; }

        /// <summary>
        ///     Gets the name of the character.
        /// </summary>
        /// <value>
        ///     The name of the character.
        /// </value>
        public string CharacterName { get; private set; }

        /// <summary>
        ///     Gets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is RSA valid.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is RSA valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsRsaValid { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether [gamemaster mode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [gamemaster mode]; otherwise, <c>false</c>.
        /// </value>
        public bool GamemasterMode { get; private set; }

        /// <summary>
        ///     Gets the elapsed.
        /// </summary>
        /// <value>
        ///     The elapsed.
        /// </value>
        public uint Elapsed { get; private set; }

        /// <summary>
        ///     Gets the fractional time.
        /// </summary>
        /// <value>
        ///     The fractional time.
        /// </value>
        public byte FractionalTime { get; private set; }

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
        /// <returns></returns>
        public static LoginPacket Parse(NetworkMessage message)
        {
            LoginPacket packet = new LoginPacket();
            packet.OSPlatform = message.ReadOSPlatform();
            packet.Version = message.ReadUInt16();
            packet.ClientVersion = message.ReadUInt32();
            packet.ClientType = message.ReadByte();
            packet.DatRevision = message.ReadUInt16();
            packet.IsRsaValid = message.DecryptRsa();

            // TODO: Should be zero
            packet.UnknownValue1 = message.ReadByte();

            // XTEA Key
            packet.Xtea = message.ReadXteaKey();

            // TODO: Investigate whether this can be replaced with NetworkMessage.ReadBoolean
            packet.GamemasterMode = Convert.ToBoolean(message.ReadByte());

            // Session Key
            // TODO: This must throw an exception
            string sessionKey = message.ReadString();
            if (!sessionKey.Contains("\n"))
                return packet;

            // TODO: This must throw an exception
            string[] sessionKeyParts = sessionKey.Split('\n');
            if (sessionKeyParts.Length != 2)
                return packet;

            packet.Username = sessionKeyParts[0];
            packet.Password = sessionKeyParts[1];
            packet.CharacterName = message.ReadString();

            // Connect
            packet.Elapsed = message.ReadUInt32();
            packet.FractionalTime = message.ReadByte();

            return packet;
        }
    }
}