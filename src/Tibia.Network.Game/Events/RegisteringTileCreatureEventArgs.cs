using System.ComponentModel;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class RegisteringTileCreatureEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.RegisteringTileCreatureEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public RegisteringTileCreatureEventArgs(ICreatureSpawn creatureSpawn)
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