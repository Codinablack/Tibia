using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class ChannelsListPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channels">The channels.</param>
        public static void Add(NetworkMessage message, ICollection<IChannel> channels)
        {
            message.AddPacketType(GamePacketType.ChannelsList);
            message.AddByte((byte) channels.Count);

            foreach (IChannel channel in channels)
            {
                switch (channel)
                {
                    case IPrivateChannel privateChannel:
                        // TODO: WARNING! Casting CharacterSpawn.Id to UInt16 is BAD design. FIX ASAP!
                        message.AddUInt16((ushort) privateChannel.Owner.Id);
                        break;
                    case IGuildChannel guildChannel:
                        message.AddUInt16(guildChannel.Guild.Id);
                        break;
                    case IPartyChannel partyChannel:
                        message.AddUInt16(partyChannel.Party.Id);
                        break;
                    case IPublicChannel publicChannel:
                        message.AddChannelType(publicChannel.Type);
                        break;
                    default:
                        message.AddUInt16(channel.Id);
                        break;
                }
                message.AddString(channel.Name);
            }
        }
    }
}