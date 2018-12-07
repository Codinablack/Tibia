using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public class AdvertisingRookgaardChannel : AdvertisingChannel
    {
        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        /// <inheritdoc />
        public override ChannelType Type => ChannelType.AdvertisingRookgaard;
    }
}