using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class SelfStatsPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        public static void Add(NetworkMessage message, ICharacterSpawn characterSpawn)
        {
            message.AddPacketType(GamePacketType.SelfStats);
            message.AddUInt16((ushort) characterSpawn.Health.Current);
            message.AddUInt16((ushort) characterSpawn.Health.Maximum);
            message.AddUInt32(characterSpawn.FreeCapacity);
            message.AddUInt32(characterSpawn.Capacity);
            message.AddUInt64(characterSpawn.Level.Experience);
            message.AddUInt16((ushort) characterSpawn.Level.Current);
            message.AddPercent(characterSpawn.Level.ToPercent());

            // TODO: Experience bonus
            message.AddDouble(0, 3);

            message.AddUInt16((ushort) characterSpawn.Mana.Current);
            message.AddUInt16((ushort) characterSpawn.Mana.Maximum);

            message.AddByte(characterSpawn.MagicLevel.Base);
            message.AddByte(characterSpawn.MagicLevel.Current);
            message.AddPercent(characterSpawn.MagicLevel.ToPercent());

            message.AddByte(characterSpawn.Soul);
            message.AddUInt16(characterSpawn.Stamina);

            // TODO: Improve protocol to provide BonusSpeed in this packet
            message.AddUInt16(characterSpawn.Speed.WalkSpeed);

            // TODO: var condition = characterSpawn.getCondition(ConditionType.CONDITION_REGENERATION);
            message.AddUInt16( /*(ushort)(condition != null ? condition.getTicks() / 1000 : */ 0x00 /*)*/);

            message.AddUInt16((ushort) (characterSpawn.OfflineTraining.Elapsed.Ticks / 60 / 1000));
        }
    }
}