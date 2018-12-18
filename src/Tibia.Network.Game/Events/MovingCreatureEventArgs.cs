using System.ComponentModel;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class MovingCreatureEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.MovingCreatureEventArgs" /> class.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="targetTile">The target tile.</param>
        public MovingCreatureEventArgs(ICreatureSpawn creatureSpawn, ITile targetTile)
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