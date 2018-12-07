using System;
using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class SelfBasicStatsPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="premiumTimeLeft">The premium time left.</param>
        /// <param name="vocation">The vocation.</param>
        /// <param name="spells">The spells.</param>
        public static void Add(NetworkMessage message, TimeSpan premiumTimeLeft, IVocation vocation, ICollection<ISpell> spells)
        {
            message.AddPacketType(GamePacketType.SelfBasicData);
            message.AddBoolean(premiumTimeLeft > TimeSpan.Zero);
            message.AddTimeSpan(premiumTimeLeft);
            message.AddByte(vocation.Id);
            AddSpells(message, spells);
        }

        /// <summary>
        ///     Adds the spells.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="spells">The spells.</param>
        private static void AddSpells(NetworkMessage message, ICollection<ISpell> spells)
        {
            message.AddUInt16((ushort) spells.Count);
            foreach (ISpell spell in spells)
                message.AddByte(spell.Id);
        }
    }
}