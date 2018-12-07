using Tibia.Core;

namespace Tibia.Data
{
    public interface ICreatureSpawn : ISpawn, IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is riding.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is riding; otherwise, <c>false</c>.
        /// </value>
        bool IsRiding { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is invisible.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is invisible; otherwise, <c>false</c>.
        /// </value>
        bool IsInvisible { get; set; }

        /// <summary>
        ///     Gets or sets the creature.
        /// </summary>
        /// <value>
        ///     The creature.
        /// </value>
        ICreature Creature { get; set; }

        /// <summary>
        ///     Gets or sets the skull.
        /// </summary>
        /// <value>
        ///     The skull.
        /// </value>
        ISkull Skull { get; set; }

        /// <summary>
        ///     Gets or sets the war icon.
        /// </summary>
        /// <value>
        ///     The war icon.
        /// </value>
        WarIcon WarIcon { get; set; }

        /// <summary>
        ///     Gets or sets the mount.
        /// </summary>
        /// <value>
        ///     The mount.
        /// </value>
        IMount Mount { get; set; }

        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        Direction Direction { get; set; }

        /// <summary>
        ///     Gets or sets the outfit.
        /// </summary>
        /// <value>
        ///     The outfit.
        /// </value>
        IOutfit Outfit { get; set; }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        /// <value>
        ///     The speed.
        /// </value>
        SpeedInfo Speed { get; set; }

        /// <summary>
        ///     Gets or sets the health.
        /// </summary>
        /// <value>
        ///     The health.
        /// </value>
        IHealth Health { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is dead.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is dead; otherwise, <c>false</c>.
        /// </value>
        bool IsDead { get; }

        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        /// <value>
        ///     The level.
        /// </value>
        ILevel Level { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is drunk.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is drunk; otherwise, <c>false</c>.
        /// </value>
        bool IsDrunk { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is exhausted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is exhausted; otherwise, <c>false</c>.
        /// </value>
        bool IsExhausted { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is poisoned.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is poisoned; otherwise, <c>false</c>.
        /// </value>
        bool IsPoisoned { get; }
    }
}