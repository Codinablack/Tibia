namespace Tibia.Network.Login.Packets
{
    internal class PremiumDaysPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="premiumDaysLeft">The premium days left.</param>
        public static void Add(NetworkMessage message, int premiumDaysLeft)
        {
            message.AddUInt16((ushort) premiumDaysLeft);
        }
    }
}