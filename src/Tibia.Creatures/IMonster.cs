using System.Collections.Generic;
using Tibia.Data;
using Tibia.Items;

namespace Tibia.Creatures
{
    public interface IMonster : ICreature
    {
        /// <summary>
        ///     Gets or sets the attacks.
        /// </summary>
        /// <value>
        ///     The attacks.
        /// </value>
        ICollection<ICombat> Attacks { get; set; }

        /// <summary>
        ///     Gets or sets the corpse.
        /// </summary>
        /// <value>
        ///     The corpse.
        /// </value>
        ICorpse Corpse { get; set; }

        /// <summary>
        ///     Gets or sets the defenses.
        /// </summary>
        /// <value>
        ///     The defenses.
        /// </value>
        ICollection<ICombat> Defenses { get; set; }

        /// <summary>
        ///     Gets or sets the convince cost.
        /// </summary>
        /// <value>
        ///     The convince cost.
        /// </value>
        int ConvinceCost { get; set; }

        /// <summary>
        ///     Gets or sets the type of the race.
        /// </summary>
        /// <value>
        ///     The type of the race.
        /// </value>
        RaceType RaceType { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the experience.
        /// </summary>
        /// <value>
        ///     The experience.
        /// </value>
        uint Experience { get; set; }

        /// <summary>
        ///     Gets or sets the loot.
        /// </summary>
        /// <value>
        ///     The loot.
        /// </value>
        ICollection<LootItem> Loot { get; set; }

        /// <summary>
        ///     Gets or sets the monster group identifier.
        /// </summary>
        /// <value>
        ///     The monster group identifier.
        /// </value>
        uint MonsterGroupId { get; set; }

        /// <summary>
        ///     Gets or sets the protection information.
        /// </summary>
        /// <value>
        ///     The protection information.
        /// </value>
        ProtectionInfo ProtectionInfo { get; set; }

        /// <summary>
        ///     Gets or sets the quotes.
        /// </summary>
        /// <value>
        ///     The quotes.
        /// </value>
        ICollection<Quote> Quotes { get; set; }

        /// <summary>
        ///     Gets or sets the summon cost.
        /// </summary>
        /// <value>
        ///     The summon cost.
        /// </value>
        uint SummonCost { get; set; }

        /// <summary>
        ///     Gets or sets the target change settings.
        /// </summary>
        /// <value>
        ///     The target change settings.
        /// </value>
        TargetChangeSettings TargetChangeSettings { get; set; }

        /// <summary>
        ///     Gets or sets the position change settings.
        /// </summary>
        /// <value>
        ///     The position change settings.
        /// </value>
        PositionChangeSettings PositionChangeSettings { get; set; }

        /// <summary>
        ///     Gets or sets the range.
        /// </summary>
        /// <value>
        ///     The range.
        /// </value>
        uint Range { get; set; }

        /// <summary>
        ///     Gets or sets the flee health.
        /// </summary>
        /// <value>
        ///     The flee health.
        /// </value>
        uint? FleeHealth { get; set; }

        /// <summary>
        ///     Gets or sets the armor.
        /// </summary>
        /// <value>
        ///     The armor.
        /// </value>
        short Armor { get; set; }

        /// <summary>
        ///     Gets or sets the defense.
        /// </summary>
        /// <value>
        ///     The defense.
        /// </value>
        short Defense { get; set; }

        /// <summary>
        ///     Gets or sets the unique name.
        /// </summary>
        /// <value>
        ///     The unique name.
        /// </value>
        string UniqueName { get; set; }
    }
}