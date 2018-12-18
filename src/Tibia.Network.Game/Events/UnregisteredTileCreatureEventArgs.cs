using System;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class UnregisteredTileCreatureEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.UnregisteredTileCreatureEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public UnregisteredTileCreatureEventArgs(ICreatureSpawn creatureSpawn)
        {
            CreatureSpawn = creatureSpawn;
        }

        /// <summary>
        ///     Gets the creature spawn.
        /// </summary>
        /// <value>
        ///     The creature spawn.
        /// </value>
        public ICreatureSpawn CreatureSpawn { get; }
    }
}