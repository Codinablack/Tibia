namespace Tibia.Data
{
    public interface IStackable
    {
        /// <summary>
        ///     Gets or sets the maximum stackable count.
        /// </summary>
        /// <value>
        ///     The maximum stackable count.
        /// </value>
        uint MaxStackableCount { get; set; }
    }
}