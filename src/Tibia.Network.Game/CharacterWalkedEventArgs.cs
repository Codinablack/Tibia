using System;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class CharacterWalkedEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.CharacterWalkedEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="direction">The direction.</param>
        public CharacterWalkedEventArgs(CharacterSpawn characterSpawn, Direction direction)
        {
            CharacterSpawn = characterSpawn;
            Direction = direction;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        public Direction Direction { get; }
    }
}