using Tibia.Data;

namespace Tibia.Items
{
    public class FluidInfo : IFluid
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the color.
        /// </summary>
        /// <value>
        ///     The color.
        /// </value>
        public FluidColor Color { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public FluidType Type { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public string Message { get; set; }
    }
}