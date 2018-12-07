using System.Collections.Generic;
using Tibia.Data;
using Tibia.Items;
using Tibia.Outfits;

namespace Tibia.Creatures
{
    public class Monster : Creature, IMonster
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Creatures.Monster" /> class.
        /// </summary>
        public Monster()
            : base(CreatureType.Monster, SpeechBubble.None)
        {
            TargetChangeSettings = new TargetChangeSettings();
            PositionChangeSettings = new PositionChangeSettings();
            Attacks = new HashSet<ICombat>();
            Defenses = new HashSet<ICombat>();
            ProtectionInfo = new ProtectionInfo();
            Quotes = new HashSet<Quote>();
            Loot = new HashSet<LootItem>();
            Outfit = new Outfit();
            Corpse = new Corpse();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the corpse.
        /// </summary>
        /// <value>
        ///     The corpse.
        /// </value>
        public ICorpse Corpse { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the monster group identifier.
        /// </summary>
        /// <value>
        ///     The monster group identifier.
        /// </value>
        public uint MonsterGroupId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the defenses.
        /// </summary>
        /// <value>
        ///     The defenses.
        /// </value>
        public ICollection<ICombat> Defenses { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the target change settings.
        /// </summary>
        /// <value>
        ///     The target change settings.
        /// </value>
        public TargetChangeSettings TargetChangeSettings { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the position change settings.
        /// </summary>
        /// <value>
        ///     The position change settings.
        /// </value>
        public PositionChangeSettings PositionChangeSettings { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the range.
        /// </summary>
        /// <value>
        ///     The range.
        /// </value>
        public uint Range { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the flee health.
        /// </summary>
        /// <value>
        ///     The flee health.
        /// </value>
        public uint? FleeHealth { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the armor.
        /// </summary>
        /// <value>
        ///     The armor.
        /// </value>
        public short Armor { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the defense.
        /// </summary>
        /// <value>
        ///     The defense.
        /// </value>
        public short Defense { get; set; }

        /// <summary>
        ///     Gets or sets the unique name.
        /// </summary>
        /// <value>
        ///     The unique name.
        /// </value>
        /// <inheritdoc />
        public string UniqueName { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the attacks.
        /// </summary>
        /// <value>
        ///     The attacks.
        /// </value>
        public ICollection<ICombat> Attacks { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the quotes.
        /// </summary>
        /// <value>
        ///     The quotes.
        /// </value>
        public ICollection<Quote> Quotes { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the loot.
        /// </summary>
        /// <value>
        ///     The loot.
        /// </value>
        public ICollection<LootItem> Loot { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the protection information.
        /// </summary>
        /// <value>
        ///     The protection information.
        /// </value>
        public ProtectionInfo ProtectionInfo { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the type of the race.
        /// </summary>
        /// <value>
        ///     The type of the race.
        /// </value>
        public RaceType RaceType { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the experience.
        /// </summary>
        /// <value>
        ///     The experience.
        /// </value>
        public uint Experience { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the summon cost.
        /// </summary>
        /// <value>
        ///     The summon cost.
        /// </value>
        public uint SummonCost { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the convince cost.
        /// </summary>
        /// <value>
        ///     The convince cost.
        /// </value>
        public int ConvinceCost { get; set; }
    }
}