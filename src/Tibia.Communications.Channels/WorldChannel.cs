using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public class WorldChannel : PublicQueryableChannel
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public override string Name => "World Chat";

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public override ChannelType Type => ChannelType.World;
        /// <summary>
        ///     Gets the minimum level.
        /// </summary>
        /// <value>
        ///     The minimum level.
        /// </value>
        public override int MinLevel => 10;
    }
}