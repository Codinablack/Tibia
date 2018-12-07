namespace Tibia.Data
{
    public interface IPublicChannel : IChannel
    {
        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        ChannelType Type { get; }
    }
}