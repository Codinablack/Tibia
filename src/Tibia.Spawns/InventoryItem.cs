﻿using Tibia.Data;

namespace Tibia.Spawns
{
    public class InventoryItem : ItemSpawn, IInventoryItem
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the creature spawn.
        /// </summary>
        /// <value>
        ///     The creature spawn.
        /// </value>
        public ICreatureSpawn CreatureSpawn { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the creature spawn identifier.
        /// </summary>
        /// <value>
        ///     The creature spawn identifier.
        /// </value>
        public int CreatureSpawnId { get; set; }
    }
}