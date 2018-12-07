using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class FriendsPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="friends">The friends.</param>
        public static void Add(NetworkMessage message, ICollection<IFriend> friends)
        {
            foreach (IFriend friend in friends)
                Add(message, friend);
        }

        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="friend">The friend.</param>
        public static void Add(NetworkMessage message, IFriend friend)
        {
            message.AddPacketType(GamePacketType.FriendEntry);
            message.AddUInt32(friend.Character.Id);
            message.AddString(friend.Character.Name);
            message.AddString(friend.Description);
            message.AddUInt32(friend.Icon);
            message.AddBoolean(friend.NotifyOnLogin);
            message.AddSessionStatus(friend.Character.Status);
        }
    }
}