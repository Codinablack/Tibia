using System.ComponentModel;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class TogglingMountEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.TogglingMountEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="isRiding">if set to <c>true</c> [is riding].</param>
        public TogglingMountEventArgs(CharacterSpawn characterSpawn, bool isRiding)
        {
            CharacterSpawn = characterSpawn;
            IsRiding = isRiding;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is riding.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is riding; otherwise, <c>false</c>.
        /// </value>
        public bool IsRiding { get; }
    }
}