using System;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class CreatureMovedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.CreatureMovedEventArgs" /> class.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="targetTile">The target tile.</param>
        public CreatureMovedEventArgs(ICreatureSpawn creatureSpawn, ITile targetTile)
        {
            CreatureSpawn = creatureSpawn;
            TargetTile = targetTile;
        }

        /// <summary>
        ///     Gets the creature spawn.
        /// </summary>
        /// <value>
        ///     The creature spawn.
        /// </value>
        public ICreatureSpawn CreatureSpawn { get; }

        /// <summary>
        ///     Gets the target tile.
        /// </summary>
        /// <value>
        ///     The target tile.
        /// </value>
        public ITile TargetTile { get; }
    }
}