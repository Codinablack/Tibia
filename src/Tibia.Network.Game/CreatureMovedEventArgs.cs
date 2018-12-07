using System;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class CreatureMovedEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.CreatureMovedEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="targetTile">The target tile.</param>
        public CreatureMovedEventArgs(CharacterSpawn characterSpawn, ITile targetTile)
        {
            CharacterSpawn = characterSpawn;
            TargetTile = targetTile;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the target tile.
        /// </summary>
        /// <value>
        ///     The target tile.
        /// </value>
        public ITile TargetTile { get; }
    }
}