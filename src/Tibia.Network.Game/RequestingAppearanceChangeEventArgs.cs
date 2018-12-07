using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class RequestingAppearanceChangeEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.RequestingAppearanceChangeEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="outfit">The outfit.</param>
        /// <param name="mount">The mount.</param>
        public RequestingAppearanceChangeEventArgs(CharacterSpawn characterSpawn, IOutfit outfit, IMount mount)
        {
            CharacterSpawn = characterSpawn;
            Outfit = outfit;
            Mount = mount;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the outfit.
        /// </summary>
        /// <value>
        ///     The outfit.
        /// </value>
        public IOutfit Outfit { get; }

        /// <summary>
        ///     Gets the mount.
        /// </summary>
        /// <value>
        ///     The mount.
        /// </value>
        public IMount Mount { get; }
    }
}