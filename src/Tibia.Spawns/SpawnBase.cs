using Tibia.Data;
using Tibia.Map;

namespace Tibia.Spawns
{
    public abstract class SpawnBase : ISpawn
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SpawnBase" /> class.
        /// </summary>
        protected SpawnBase()
        {
            LightInfo = new LightInfo();
        }
        /// <summary>
        ///     Gets or sets the light information.
        /// </summary>
        /// <value>
        ///     The light information.
        /// </value>
        public ILightInfo LightInfo { get; set; }
        /// <summary>
        ///     Gets or sets the tile.
        /// </summary>
        /// <value>
        ///     The tile.
        /// </value>
        public ITile Tile { get; set; }
        /// <summary>
        ///     Gets or sets the stack position.
        /// </summary>
        /// <value>
        ///     The stack position.
        /// </value>
        public byte StackPosition { get; set; }
    }
}