using System.Collections.Generic;
using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class AutoWalkingEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.AutoWalkingEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="directions">The directions.</param>
        public AutoWalkingEventArgs(CharacterSpawn characterSpawn, Queue<Direction> directions)
        {
            CharacterSpawn = characterSpawn;
            Directions = directions;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the directions.
        /// </summary>
        /// <value>
        ///     The directions.
        /// </value>
        public Queue<Direction> Directions { get; }
    }
}