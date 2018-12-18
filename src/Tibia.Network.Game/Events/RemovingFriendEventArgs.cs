using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game.Events
{
    public class RemovingFriendEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.RemovingFriendEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="friend">The friend.</param>
        /// <param name="friendId">The friend identifier.</param>
        public RemovingFriendEventArgs(CharacterSpawn characterSpawn, IFriend friend, uint friendId)
        {
            CharacterSpawn = characterSpawn;
            Friend = friend;
            FriendId = friendId;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the friend.
        /// </summary>
        /// <value>
        ///     The friend.
        /// </value>
        public IFriend Friend { get; }

        /// <summary>
        ///     Gets the friend identifier.
        /// </summary>
        /// <value>
        ///     The friend identifier.
        /// </value>
        public uint FriendId { get; }
    }
}