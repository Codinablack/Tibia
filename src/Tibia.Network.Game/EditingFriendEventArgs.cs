using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class EditingFriendEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.EditingFriendEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="friend">The friend.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="notifyOnLogin">if set to <c>true</c> [notify on login].</param>
        public EditingFriendEventArgs(CharacterSpawn characterSpawn, IFriend friend, uint friendId, string description, uint icon, bool notifyOnLogin)
        {
            CharacterSpawn = characterSpawn;
            Friend = friend;
            FriendId = friendId;
            Description = description;
            Icon = icon;
            NotifyOnLogin = notifyOnLogin;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        public IFriend Friend { get; }

        /// <summary>
        ///     Gets the friend identifier.
        /// </summary>
        /// <value>
        ///     The friend identifier.
        /// </value>
        public uint FriendId { get; }

        /// <summary>
        ///     Gets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; }

        /// <summary>
        ///     Gets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        public uint Icon { get; }

        /// <summary>
        ///     Gets a value indicating whether [notify on login].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [notify on login]; otherwise, <c>false</c>.
        /// </value>
        public bool NotifyOnLogin { get; }
    }
}