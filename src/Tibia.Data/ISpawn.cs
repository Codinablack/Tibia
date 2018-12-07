namespace Tibia.Data
{
    public interface ISpawn
    {
        /// <summary>
        ///     Gets or sets the light information.
        /// </summary>
        /// <value>
        ///     The light information.
        /// </value>
        ILightInfo LightInfo { get; set; }

        /// <summary>
        ///     Gets or sets the tile.
        /// </summary>
        /// <value>
        ///     The tile.
        /// </value>
        ITile Tile { get; set; }

        /// <summary>
        ///     Gets or sets the stack position.
        /// </summary>
        /// <value>
        ///     The stack position.
        /// </value>
        byte StackPosition { get; set; }
    }
}