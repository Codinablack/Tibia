using System.ComponentModel;
using Tibia.Spawns;

namespace Tibia.Network.Game.Events
{
    public class RequestingAppearanceWindowEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.RequestingAppearanceWindowEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public RequestingAppearanceWindowEventArgs(CharacterSpawn characterSpawn)
        {
            CharacterSpawn = characterSpawn;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }
    }
}