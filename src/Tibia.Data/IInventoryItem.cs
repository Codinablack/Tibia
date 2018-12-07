namespace Tibia.Data
{
    public interface IInventoryItem
    {
        /// <summary>
        ///     Gets or sets the creature spawn.
        /// </summary>
        /// <value>
        ///     The creature spawn.
        /// </value>
        ICreatureSpawn CreatureSpawn { get; set; }

        /// <summary>
        ///     Gets or sets the creature spawn identifier.
        /// </summary>
        /// <value>
        ///     The creature spawn identifier.
        /// </value>
        int CreatureSpawnId { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        int Id { get; set; }
    }
}