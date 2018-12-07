using System;
using System.Linq;
using Tibia.Data;
using Tibia.InteropServices;
using Tibia.Items.Features;

namespace Tibia.Network
{
    public static class NetworkMessageExtensions
    {
        /// <summary>
        ///     Adds the type of the channel event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channelEventType">Type of the channel event.</param>
        public static void AddChannelEventType(this NetworkMessage message, ChannelEventType channelEventType)
        {
            message.AddByte((byte) channelEventType);
        }

        /// <summary>
        ///     Adds the time span.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="timeSpan">The time span.</param>
        public static void AddTimeSpan(this NetworkMessage message, TimeSpan timeSpan)
        {
            message.AddUInt32((uint) timeSpan.Seconds);
        }

        /// <summary>
        ///     Adds the type of the text message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="textMessageType">Type of the text message.</param>
        public static void AddTextMessageType(this NetworkMessage message, TextMessageType textMessageType)
        {
            message.AddByte((byte) textMessageType);
        }

        /// <summary>
        ///     Adds the type of the channel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channelType">Type of the channel.</param>
        public static void AddChannelType(this NetworkMessage message, ChannelType channelType)
        {
            message.AddUInt16((ushort) channelType);
        }

        /// <summary>
        ///     Adds the type of the slot.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="slotType">Type of the slot.</param>
        public static void AddSlotType(this NetworkMessage message, SlotType slotType)
        {
            message.AddByte((byte) slotType);
        }

        /// <summary>
        ///     Adds the percent.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="percent">The percent.</param>
        public static void AddPercent(this NetworkMessage message, Percent percent)
        {
            message.AddByte(percent.Value);
        }

        /// <summary>
        ///     Adds the light information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="lightInfo">The light information.</param>
        public static void AddLightInfo(this NetworkMessage message, ILightInfo lightInfo)
        {
            message.AddByte(lightInfo.Level);
            message.AddByte(lightInfo.Color);
        }

        /// <summary>
        ///     Adds the vector3.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="value">The value.</param>
        public static void AddVector3(this NetworkMessage message, IVector3 value)
        {
            // TODO: Consider changing Vector3 implementation since casting to ushort is not safe
            // TODO: but using int is default in C# for arithmetic operations
            message.AddUInt16((ushort) value.X);
            message.AddUInt16((ushort) value.Y);
            message.AddByte((byte) value.Z);
        }

        /// <summary>
        ///     Adds the item spawn.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="itemSpawn">The item spawn.</param>
        public static void AddItemSpawn(this NetworkMessage message, IItemSpawn itemSpawn)
        {
            message.AddUInt16(itemSpawn.Item.SpriteId);

            // TODO: MARK_UNMARKED
            message.AddByte(0xFF);

            IStackableItemSpawn stackableItemSpawn = itemSpawn.GetFeature<IStackableItemSpawn>();
            if (stackableItemSpawn != null)
                message.AddByte((byte) stackableItemSpawn.Count);
            else
            {
                // TODO: Replace feature with properties
                PoolFeature poolFeature = itemSpawn.GetFeature<PoolFeature>();
                if (poolFeature != null)
                    message.AddByte((byte) poolFeature.Fluid.Color);
            }

            // TODO: Replace feature with properties
            // TODO: Random phase (0xFF for async)
            if (itemSpawn.Item.Features.Any(s => s is AnimationFeature))
                message.AddByte(0xFE);
        }

        /// <summary>
        ///     Adds the effect.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="effect">The effect.</param>
        public static void AddEffect(this NetworkMessage message, Effect effect)
        {
            message.AddByte((byte) effect);
        }

        /// <summary>
        ///     Adds the direction.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="direction">The direction.</param>
        public static void AddDirection(this NetworkMessage message, Direction direction)
        {
            message.AddByte((byte) direction);
        }

        /// <summary>
        ///     Adds the type of the creature.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureType">Type of the creature.</param>
        public static void AddCreatureType(this NetworkMessage message, CreatureType creatureType)
        {
            message.AddByte((byte) creatureType);
        }

        /// <summary>
        ///     Adds the war icon.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="warIcon">The war icon.</param>
        public static void AddWarIcon(this NetworkMessage message, WarIcon warIcon)
        {
            message.AddByte((byte) warIcon);
        }

        /// <summary>
        ///     Adds the type of the skull.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="skullType">Type of the skull.</param>
        public static void AddSkullType(this NetworkMessage message, SkullType skullType)
        {
            message.AddByte((byte) skullType);
        }

        /// <summary>
        ///     Adds the session status.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        public static void AddSessionStatus(this NetworkMessage message, SessionStatus status)
        {
            message.AddByte((byte) status);
        }

        /// <summary>
        ///     Adds the type of the speech.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="speechType">Type of the speech.</param>
        public static void AddSpeechType(this NetworkMessage message, SpeechType speechType)
        {
            message.AddByte((byte) speechType);
        }

        /// <summary>
        ///     Adds the speech bubble.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="speechBubble">The speech bubble.</param>
        public static void AddSpeechBubble(this NetworkMessage message, SpeechBubble speechBubble)
        {
            message.AddByte((byte) speechBubble);
        }

        /// <summary>
        ///     Adds the conditions.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="conditions">The conditions.</param>
        public static void AddConditions(this NetworkMessage message, Conditions conditions)
        {
            message.AddUInt16((ushort) conditions);
        }

        /// <summary>
        ///     Adds the party shield.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="partyShield">The party shield.</param>
        public static void AddPartyShield(this NetworkMessage message, PartyShield partyShield)
        {
            message.AddByte((byte) partyShield);
        }

        /// <summary>
        ///     Reads the battle stance.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The battle stance.</returns>
        public static BattleStance ReadBattleStance(this NetworkMessage message)
        {
            return (BattleStance) message.ReadByte();
        }

        /// <summary>
        ///     Reads the type of the channel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The channel.</returns>
        public static ChannelType ReadChannelType(this NetworkMessage message)
        {
            return (ChannelType) message.ReadUInt16();
        }

        /// <summary>
        ///     Reads the operating system platform.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The operating system platform.</returns>
        public static OSPlatform ReadOSPlatform(this NetworkMessage message)
        {
            return OSPlatform.Parse(message.ReadUInt16());
        }
    }
}