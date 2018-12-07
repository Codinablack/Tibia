namespace Tibia.Data
{
    public interface IGuildChannel : IChannel
    {
        /// <summary>
        ///     Gets or sets the guild.
        /// </summary>
        /// <value>
        ///     The guild.
        /// </value>
        IGuild Guild { get; set; }
    }
}