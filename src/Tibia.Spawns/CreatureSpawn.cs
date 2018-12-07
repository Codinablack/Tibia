using System.Collections.Generic;
using Tibia.Battle;
using Tibia.Combat;
using Tibia.Data;
using Tibia.Mounts;
using Tibia.Outfits;

namespace Tibia.Spawns
{
    public abstract class CreatureSpawn : SpawnBase, ICreatureSpawn
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Spawns.CreatureSpawn" /> class.
        /// </summary>
        protected CreatureSpawn()
        {
            Health = new Health();
            Skull = new Skull();
            Level = new Level();
            Mount = new Mount();
            Outfit = new Outfit();
            RegenerationConditions = new HashSet<RegenerationCondition>();
            Speed = new SpeedInfo();
        }

        /// <summary>
        ///     Gets or sets the drunk condition.
        /// </summary>
        /// <value>
        ///     The drunk condition.
        /// </value>
        public DrunkCondition DrunkCondition { get; set; }

        /// <summary>
        ///     Gets or sets the regeneration conditions.
        /// </summary>
        /// <value>
        ///     The regeneration conditions.
        /// </value>
        public ICollection<RegenerationCondition> RegenerationConditions { get; set; }

        /// <summary>
        ///     Gets or sets the poison condition.
        /// </summary>
        /// <value>
        ///     The poison condition.
        /// </value>
        public PoisonCondition PoisonCondition { get; set; }

        /// <summary>
        ///     Gets or sets the exhaust condition.
        /// </summary>
        /// <value>
        ///     The exhaust condition.
        /// </value>
        public ExhaustCondition ExhaustCondition { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the outfit.
        /// </summary>
        /// <value>
        ///     The outfit.
        /// </value>
        public IOutfit Outfit { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the direction.
        /// </summary>
        /// <value>
        ///     The direction.
        /// </value>
        public Direction Direction { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the mount.
        /// </summary>
        /// <value>
        ///     The mount.
        /// </value>
        public IMount Mount { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the war icon.
        /// </summary>
        /// <value>
        ///     The war icon.
        /// </value>
        public WarIcon WarIcon { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the creature.
        /// </summary>
        /// <value>
        ///     The creature.
        /// </value>
        public ICreature Creature { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the skull.
        /// </summary>
        /// <value>
        ///     The skull.
        /// </value>
        public ISkull Skull { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        /// <value>
        ///     The speed.
        /// </value>
        public SpeedInfo Speed { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the health.
        /// </summary>
        /// <value>
        ///     The health.
        /// </value>
        public IHealth Health { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether this instance is dead.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is dead; otherwise, <c>false</c>.
        /// </value>
        public bool IsDead
        {
            get { return Health.Current <= 0; }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        /// <value>
        ///     The level.
        /// </value>
        public ILevel Level { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether this instance is drunk.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is drunk; otherwise, <c>false</c>.
        /// </value>
        public bool IsDrunk
        {
            get { return DrunkCondition != null; }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether this instance is exhausted.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is exhausted; otherwise, <c>false</c>.
        /// </value>
        public bool IsExhausted
        {
            get { return ExhaustCondition != null; }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether this instance is poisoned.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is poisoned; otherwise, <c>false</c>.
        /// </value>
        public bool IsPoisoned
        {
            get { return PoisonCondition != null; }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is riding.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is riding; otherwise, <c>false</c>.
        /// </value>
        public bool IsRiding { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is invisible.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is invisible; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvisible { get; set; }
    }
}