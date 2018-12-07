using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class ChannelOpenPacket : IServerPacket, IClientPacket
    {
        /// <summary>
        ///     Gets or sets the type of the channel.
        /// </summary>
        /// <value>
        ///     The type of the channel.
        /// </value>
        public ChannelType ChannelType { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static ChannelOpenPacket Parse(NetworkMessage message)
        {
            ChannelOpenPacket packet = new ChannelOpenPacket();
            packet.ChannelType = message.ReadChannelType();
            return packet;
        }

        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channel">The channel.</param>
        public static void Add(NetworkMessage message, IChannel channel)
        {
            message.AddPacketType(GamePacketType.ChannelOpen);

            switch (channel)
            {
                case IPrivateChannel privateChannel:
                    AddPrivateChannel(message, privateChannel);
                    break;
                case IGuildChannel guildChannel:
                    AddGuildChannel(message, guildChannel);
                    break;
                case IPartyChannel partyChannel:
                    AddPartyChannel(message, partyChannel);
                    break;
                case IPublicChannel publicChannel:
                    AddPublicChannel(message, publicChannel);
                    break;
                default:
                    AddChannel(message, channel);
                    break;
            }
        }

        /// <summary>
        ///     Adds the public channel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channel">The channel.</param>
        private static void AddPublicChannel(NetworkMessage message, IPublicChannel channel)
        {
            message.AddChannelType(channel.Type);
            AddUsers(message, channel);
            message.AddUInt16(0x00);
        }

        /// <summary>
        ///     Adds the channel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channel">The channel.</param>
        private static void AddChannel(NetworkMessage message, IChannel channel)
        {
            message.AddUInt16(channel.Id);
            AddUsers(message, channel);
            message.AddUInt16(0x00);
        }

        /// <summary>
        ///     Adds the party channel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="partyChannel">The party channel.</param>
        private static void AddPartyChannel(NetworkMessage message, IPartyChannel partyChannel)
        {
            message.AddUInt16(partyChannel.Party.Id);
            AddUsers(message, partyChannel);
            message.AddUInt16(0x00);
        }

        /// <summary>
        ///     Adds the guild channel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="guildChannel">The guild channel.</param>
        private static void AddGuildChannel(NetworkMessage message, IGuildChannel guildChannel)
        {
            message.AddUInt16(guildChannel.Guild.Id);
            AddUsers(message, guildChannel);
            message.AddUInt16(0x00);
        }

        /// <summary>
        ///     Adds the private channel.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="privateChannel">The private channel.</param>
        private static void AddPrivateChannel(NetworkMessage message, IPrivateChannel privateChannel)
        {
            message.AddUInt16((ushort) privateChannel.Owner.Id);
            AddUsers(message, privateChannel);
            message.AddUInt16((ushort) privateChannel.Invitations.Count);
            foreach (ICharacterSpawn invitation in privateChannel.Invitations)
                message.AddString(invitation.Character.Name);
            message.AddUInt16(0x00);
        }

        /// <summary>
        ///     Adds the users.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channel">The channel.</param>
        private static void AddUsers(NetworkMessage message, IChannel channel)
        {
            message.AddString(channel.Name);
            message.AddUInt16((ushort) channel.Members.Count);
            foreach (ICharacterSpawn member in channel.Members.Values)
                message.AddString(member.Character.Name);
        }
    }
}