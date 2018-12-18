using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public abstract class PublicChannel : ChannelBase, IPublicChannel
    {
        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public abstract ChannelType Type { get; }
    }
}