using System.Collections.Generic;
using Tibia.Quests;

namespace Tibia.Network.Game.Packets
{
    public class QuestLogQuestLinePacket : IServerPacket, IClientPacket
    {
        /// <summary>
        ///     Gets or sets the quest identifier.
        /// </summary>
        /// <value>
        ///     The quest identifier.
        /// </value>
        public ushort QuestId { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static QuestLogQuestLinePacket Parse(NetworkMessage message)
        {
            return new QuestLogQuestLinePacket
            {
                QuestId = message.ReadUInt16()
            };
        }

        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="questId">The quest identifier.</param>
        /// <param name="missions">The missions.</param>
        public static void Add(NetworkMessage message, ushort questId, ICollection<MissionInfo> missions)
        {
            message.AddPacketType(GamePacketType.QuestLogQuestLine);
            message.AddUInt16(questId);
            AddMissions(message, missions);
        }

        /// <summary>
        ///     Adds the missions.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="missions">The missions.</param>
        private static void AddMissions(NetworkMessage message, ICollection<MissionInfo> missions)
        {
            message.AddByte((byte) missions.Count);
            foreach (MissionInfo mission in missions)
            {
                message.AddString(mission.Mission.Name);
                message.AddString(!string.IsNullOrWhiteSpace(mission.MissionState?.Description) ? mission.MissionState.Description : string.Empty);
            }
        }
    }
}