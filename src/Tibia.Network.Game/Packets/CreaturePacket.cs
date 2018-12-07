using Tibia.Data;
using Tibia.Parties;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class CreaturePacket : IServerPacket
    {
        /// <summary>
        ///     Adds the creature.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="known">if set to <c>true</c> [known].</param>
        /// <param name="removeKnown">The remove known.</param>
        public static void AddCreature(NetworkMessage message, ICharacterSpawn characterSpawn, ICreatureSpawn creatureSpawn, bool known, uint removeKnown)
        {
            // TODO: This method MUST be called "Add" for consistency
            if (known)
            {
                // TODO: MAGIC NUMBER DETECTED!
                message.AddUInt16(0x62); // known
                message.AddUInt32(creatureSpawn.Id);
            }
            else
            {
                // TODO: MAGIC NUMBER DETECTED!
                message.AddUInt16(0x61); // unknown
                message.AddUInt32(removeKnown);
                message.AddUInt32(creatureSpawn.Id);
                message.AddCreatureType(creatureSpawn.Creature.CreatureType);
                message.AddString(creatureSpawn.Creature.Name);
            }

            message.AddPercent(creatureSpawn.Health.ToPercent());
            message.AddDirection(creatureSpawn.Direction);
            message.AddAppearance(creatureSpawn.Outfit, creatureSpawn.Mount);
            message.AddByte(creatureSpawn.LightInfo.Level);
            message.AddByte(creatureSpawn.LightInfo.Color);
            message.AddUInt16(creatureSpawn.Speed.WalkSpeed);
            message.AddSkullType(creatureSpawn.Skull.Type);
            message.AddPartyShield(characterSpawn.GetPartyShield(creatureSpawn as CharacterSpawn));

            if (!known)
                message.AddWarIcon(creatureSpawn.WarIcon);

            CreatureType creatureType = creatureSpawn.Creature.CreatureType;
            if (creatureType == CreatureType.Monster && creatureSpawn is ISummon summon)
                creatureType = summon.Master == characterSpawn ? CreatureType.SummonOwn : CreatureType.SummonOthers;

            message.AddCreatureType(creatureType);
            message.AddSpeechBubble(creatureSpawn.Creature.SpeechBubble);

            // TODO: Implement marked/unmarked??
            message.AddByte(0xFF);

            // TODO: Implement helpers
            //if (otherPlayer)
            //{
            //    msg.add<uint16_t>(otherPlayer->getHelpers());
            //}
            //else
            //{
            // TODO: MAGIC NUMBER DETECTED!
            message.AddUInt16(0x00);
            //}
            message.AddBoolean(creatureSpawn is ISolidBlock);
        }
    }
}