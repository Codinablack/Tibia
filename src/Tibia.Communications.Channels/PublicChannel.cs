using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public abstract class PublicChannel : ChannelBase, IPublicChannel
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public abstract ChannelType Type { get; }
    }
}