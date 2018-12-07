namespace Tibia.Data
{
    public interface ILightInfo
    {
        /// <summary>
        ///     Gets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        byte Color { get; set; }

        /// <summary>
        ///     Gets the level.
        /// </summary>
        /// <value>
        ///     The level.
        /// </value>
        byte Level { get; set; }
    }
}