using System;
using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class TextMessagePacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="primaryValue">The primary value.</param>
        /// <param name="primaryColor">Color of the primary.</param>
        /// <param name="secondaryValue">The secondary value.</param>
        /// <param name="secondaryColor">Color of the secondary.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Add(NetworkMessage message, TextMessageType type, string text, IVector3 position, uint? primaryValue, byte? primaryColor, uint? secondaryValue, byte? secondaryColor)
        {
            message.AddPacketType(GamePacketType.TextMessage);
            message.AddTextMessageType(type);

            switch (type)
            {
                case TextMessageType.DamageDealt:
                case TextMessageType.DamageReceived:
                case TextMessageType.DamageOthers:
                {
                    if (position == null)
                        throw new ArgumentNullException(nameof(position));

                    if (primaryValue == null)
                        throw new ArgumentNullException(nameof(primaryValue));

                    if (primaryColor == null)
                        throw new ArgumentNullException(nameof(primaryColor));

                    if (secondaryValue == null)
                        throw new ArgumentNullException(nameof(secondaryValue));

                    if (secondaryColor == null)
                        throw new ArgumentNullException(nameof(secondaryColor));

                    message.AddVector3(position);
                    message.AddUInt32(primaryValue.Value);
                    message.AddByte(primaryColor.Value);
                    message.AddUInt32(secondaryValue.Value);
                    message.AddByte(secondaryColor.Value);
                    break;
                }
                case TextMessageType.Heal:
                case TextMessageType.HealOthers:
                case TextMessageType.Experience:
                case TextMessageType.ExperienceOthers:
                {
                    if (position == null)
                        throw new ArgumentNullException(nameof(position));

                    if (primaryValue == null)
                        throw new ArgumentNullException(nameof(primaryValue));

                    if (primaryColor == null)
                        throw new ArgumentNullException(nameof(primaryColor));

                    message.AddVector3(position);
                    message.AddUInt32(primaryValue.Value);
                    message.AddByte(primaryColor.Value);
                    break;
                }
            }

            message.AddString(text);
        }
    }
}