using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public class HelpChannel : PublicQueryableChannel
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        public override string Name => "Help";

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        /// <inheritdoc />
        public override ChannelType Type => ChannelType.Help;
    }
}