using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class QuestLogWindowPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="quests">The quests.</param>
        internal static void Add(NetworkMessage message, ICollection<IQuestInfo> quests)
        {
            message.AddPacketType(GamePacketType.QuestLogWindow);
            AddQuests(message, quests);
        }

        /// <summary>
        ///     Adds the quests.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="quests">The quests.</param>
        private static void AddQuests(NetworkMessage message, ICollection<IQuestInfo> quests)
        {
            message.AddUInt16((ushort) quests.Count);
            foreach (IQuestInfo quest in quests)
                message.AddQuest(quest);
        }
    }
}