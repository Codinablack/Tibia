using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class SelfSkillsPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        public static void Add(NetworkMessage message, ICharacterSpawn characterSpawn)
        {
            message.AddPacketType(GamePacketType.SelfSkills);

            // Fist
            // TODO: message.AddUInt16(characterSpawn.FistSkill.Current);
            message.AddUInt16(characterSpawn.FistSkill.Base);
            message.AddUInt16(characterSpawn.FistSkill.Base);
            message.AddPercent(characterSpawn.FistSkill.ToPercent());

            // Club
            // TODO: message.AddUInt16(characterSpawn.ClubSkill.Current);
            message.AddUInt16(characterSpawn.ClubSkill.Base);
            message.AddUInt16(characterSpawn.ClubSkill.Base);
            message.AddPercent(characterSpawn.ClubSkill.ToPercent());

            // Sword
            // TODO: message.AddUInt16(characterSpawn.SwordSkill.Current);
            message.AddUInt16(characterSpawn.SwordSkill.Base);
            message.AddUInt16(characterSpawn.SwordSkill.Base);
            message.AddPercent(characterSpawn.SwordSkill.ToPercent());

            // Axe
            // TODO: message.AddUInt16(characterSpawn.AxeSkill.Current);
            message.AddUInt16(characterSpawn.AxeSkill.Base);
            message.AddUInt16(characterSpawn.AxeSkill.Base);
            message.AddPercent(characterSpawn.AxeSkill.ToPercent());

            // Distance
            // TODO: message.AddUInt16(characterSpawn.DistanceSkill.Current);
            message.AddUInt16(characterSpawn.DistanceSkill.Base);
            message.AddUInt16(characterSpawn.DistanceSkill.Base);
            message.AddPercent(characterSpawn.DistanceSkill.ToPercent());

            // Shield
            // TODO: message.AddUInt16(characterSpawn.ShieldSkill.Current);
            message.AddUInt16(characterSpawn.ShieldSkill.Base);
            message.AddUInt16(characterSpawn.ShieldSkill.Base);
            message.AddPercent(characterSpawn.ShieldSkill.ToPercent());

            // Fishing
            // TODO: message.AddUInt16(characterSpawn.FishingSkill.Current);
            message.AddUInt16(characterSpawn.FishingSkill.Base);
            message.AddUInt16(characterSpawn.FishingSkill.Base);
            message.AddPercent(characterSpawn.FishingSkill.ToPercent());
        }
    }
}