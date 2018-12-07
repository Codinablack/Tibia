using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class AddingFriendEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.AddingFriendEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="friendName">Name of the friend.</param>
        public AddingFriendEventArgs(CharacterSpawn characterSpawn, string friendName)
        {
            CharacterSpawn = characterSpawn;
            FriendName = friendName;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the name of the friend.
        /// </summary>
        /// <value>
        ///     The name of the friend.
        /// </value>
        public string FriendName { get; }

        /// <summary>
        ///     Gets or sets the character.
        /// </summary>
        /// <value>
        ///     The character.
        /// </value>
        public ICharacter Character { get; set; }
    }
}