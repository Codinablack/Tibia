namespace Tibia.Data
{
    public interface ICharacter : ICreature
    {
        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        SessionStatus Status { get; set; }
    }
}