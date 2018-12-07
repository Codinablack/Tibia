using System;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class AddedFriendEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.AddedFriendEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="friend">The friend.</param>
        public AddedFriendEventArgs(CharacterSpawn characterSpawn, IFriend friend)
        {
            CharacterSpawn = characterSpawn;
            Friend = friend;
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
    }
}