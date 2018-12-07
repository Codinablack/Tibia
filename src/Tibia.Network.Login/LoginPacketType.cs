namespace Tibia.Network.Login
{
    public enum LoginPacketType : byte
    {
        /// <summary>
        ///     The disconnect packet.
        /// </summary>
        Disconnect = 0x0B,

        /// <summary>
        ///     The message of the day packet.
        /// </summary>
        MessageOfTheDay = 0x14,

        /// <summary>
        ///     The session key packet.
        /// </summary>
        SessionKey = 0x28,

        /// <summary>
        ///     The character list packet.
        /// </summary>
        CharacterList = 0x64
    }
}