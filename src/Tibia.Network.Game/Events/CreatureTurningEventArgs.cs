using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game.Events
{
    public class CreatureTurningEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.CreatureTurningEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The creature spawn.</param>
        /// <param name="direction">The direction.</param>
        public CreatureTurningEventArgs(CreatureSpawn characterSpawn, Direction direction)
        {
            CharacterSpawn = characterSpawn;
            Direction = direction;
        }

        /// <summary>
        ///     Gets the creature spawn.
        /// </summary>
        /// <value>
        ///     The creature spawn.
        /// </value>
        public CreatureSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        public Direction Direction { get; }
    }
}