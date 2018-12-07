using Tibia.Data;

namespace Tibia.Map
{
    public class LightInfo : ILightInfo
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Map.LightInfo" /> class.
        /// </summary>
        public LightInfo()
            : this(LightLevel.None, LightColor.None)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LightInfo" /> class.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="color">The color.</param>
        public LightInfo(byte level, byte color)
        {
            Level = level;
            Color = color;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public byte Color { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the level.
        /// </summary>
        /// <value>
        ///     The level.
        /// </value>
        public byte Level { get; set; }
    }
}