namespace Tibia.Data
{
    public interface IPartyChannel : IChannel
    {
        /// <summary>
        ///     Gets or sets the party.
        /// </summary>
        /// <value>
        ///     The party.
        /// </value>
        IParty Party { get; set; }
    }
}