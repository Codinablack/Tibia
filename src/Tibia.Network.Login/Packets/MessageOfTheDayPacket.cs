using Tibia.Data;

namespace Tibia.Network.Login.Packets
{
    internal class MessageOfTheDayPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageOfTheDay">The message of the day.</param>
        public static void Add(NetworkMessage message, INotification messageOfTheDay)
        {
            if (messageOfTheDay == null)
                return;

            if (string.IsNullOrWhiteSpace(messageOfTheDay.Value))
                return;

            // TODO: MotD should have an ID that allows to flag it as 'read' for each player individually
            string motd = $"0\n{messageOfTheDay.Value}";

            message.AddPacketType(LoginPacketType.MessageOfTheDay);
            message.AddString(motd);
        }
    }
}