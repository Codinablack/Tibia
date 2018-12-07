using Tibia.Data;

namespace Tibia.Vocations
{
    public class Vocation : IVocation
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

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
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public VocationType Type { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the capacity base.
        /// </summary>
        /// <value>
        ///     The capacity base.
        /// </value>
        public int CapacityBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the capacity maximum.
        /// </summary>
        /// <value>
        ///     The capacity maximum.
        /// </value>
        public int CapacityMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the capacity per level.
        /// </summary>
        /// <value>
        ///     The capacity per level.
        /// </value>
        public int CapacityPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the health base.
        /// </summary>
        /// <value>
        ///     The health base.
        /// </value>
        public int HealthBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the health maximum.
        /// </summary>
        /// <value>
        ///     The health maximum.
        /// </value>
        public int HealthMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the health per level.
        /// </summary>
        /// <value>
        ///     The health per level.
        /// </value>
        public int HealthPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the health regeneration maximum.
        /// </summary>
        /// <value>
        ///     The health regeneration maximum.
        /// </value>
        public int HealthRegenerationMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the health base regeneration.
        /// </summary>
        /// <value>
        ///     The health base regeneration.
        /// </value>
        public int HealthBaseRegeneration { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the health regeneration per level.
        /// </summary>
        /// <value>
        ///     The health regeneration per level.
        /// </value>
        public int HealthRegenerationPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the resource base.
        /// </summary>
        /// <value>
        ///     The resource base.
        /// </value>
        public int ResourceBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the resource maximum.
        /// </summary>
        /// <value>
        ///     The resource maximum.
        /// </value>
        public int ResourceMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the resource per level.
        /// </summary>
        /// <value>
        ///     The resource per level.
        /// </value>
        public int ResourcePerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the resource regeneration maximum.
        /// </summary>
        /// <value>
        ///     The resource regeneration maximum.
        /// </value>
        public int ResourceRegenerationMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the resource base regeneration.
        /// </summary>
        /// <value>
        ///     The resource base regeneration.
        /// </value>
        public int ResourceBaseRegeneration { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the resource regeneration.
        /// </summary>
        /// <value>
        ///     The resource regeneration.
        /// </value>
        public int ResourceRegeneration { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the armor penetration base.
        /// </summary>
        /// <value>
        ///     The armor penetration base.
        /// </value>
        public int ArmorPenetrationBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the armor penetration maximum.
        /// </summary>
        /// <value>
        ///     The armor penetration maximum.
        /// </value>
        public int ArmorPenetrationMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the armor penetration per level.
        /// </summary>
        /// <value>
        ///     The armor penetration per level.
        /// </value>
        public int ArmorPenetrationPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the magic penetration base.
        /// </summary>
        /// <value>
        ///     The magic penetration base.
        /// </value>
        public int MagicPenetrationBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the magic penetration maximum.
        /// </summary>
        /// <value>
        ///     The magic penetration maximum.
        /// </value>
        public int MagicPenetrationMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the magic penetration per level.
        /// </summary>
        /// <value>
        ///     The magic penetration per level.
        /// </value>
        public int MagicPenetrationPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the life steal base.
        /// </summary>
        /// <value>
        ///     The life steal base.
        /// </value>
        public int LifeStealBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the life steal maximum.
        /// </summary>
        /// <value>
        ///     The life steal maximum.
        /// </value>
        public int LifeStealMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the life steal per level.
        /// </summary>
        /// <value>
        ///     The life steal per level.
        /// </value>
        public int LifeStealPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the spell vamp base.
        /// </summary>
        /// <value>
        ///     The spell vamp base.
        /// </value>
        public int SpellVampBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the spell vamp maximum.
        /// </summary>
        /// <value>
        ///     The spell vamp maximum.
        /// </value>
        public int SpellVampMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the spell vamp per level.
        /// </summary>
        /// <value>
        ///     The spell vamp per level.
        /// </value>
        public int SpellVampPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the attack range base.
        /// </summary>
        /// <value>
        ///     The attack range base.
        /// </value>
        public int AttackRangeBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the attack range maximum.
        /// </summary>
        /// <value>
        ///     The attack range maximum.
        /// </value>
        public int AttackRangeMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the attack range per level.
        /// </summary>
        /// <value>
        ///     The attack range per level.
        /// </value>
        public int AttackRangePerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the tenacity base.
        /// </summary>
        /// <value>
        ///     The tenacity base.
        /// </value>
        public int TenacityBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the tenacity maximum.
        /// </summary>
        /// <value>
        ///     The tenacity maximum.
        /// </value>
        public int TenacityMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the tenacity per level.
        /// </summary>
        /// <value>
        ///     The tenacity per level.
        /// </value>
        public int TenacityPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the movement speed base.
        /// </summary>
        /// <value>
        ///     The movement speed base.
        /// </value>
        public int MovementSpeedBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the movement speed maximum.
        /// </summary>
        /// <value>
        ///     The movement speed maximum.
        /// </value>
        public int MovementSpeedMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the movement speed per level.
        /// </summary>
        /// <value>
        ///     The movement speed per level.
        /// </value>
        public int MovementSpeedPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the soul base.
        /// </summary>
        /// <value>
        ///     The soul base.
        /// </value>
        public byte SoulBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the soul maximum.
        /// </summary>
        /// <value>
        ///     The soul maximum.
        /// </value>
        public byte SoulMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the soul per level.
        /// </summary>
        /// <value>
        ///     The soul per level.
        /// </value>
        public byte SoulPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the soul base regeneration.
        /// </summary>
        /// <value>
        ///     The soul base regeneration.
        /// </value>
        public byte SoulBaseRegeneration { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the soul regeneration maximum.
        /// </summary>
        /// <value>
        ///     The soul regeneration maximum.
        /// </value>
        public byte SoulRegenerationMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the stamina maximum.
        /// </summary>
        /// <value>
        ///     The stamina maximum.
        /// </value>
        public int StaminaMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the stamina regeneration.
        /// </summary>
        /// <value>
        ///     The stamina regeneration.
        /// </value>
        public int StaminaRegeneration { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the stamina regeneration maximum.
        /// </summary>
        /// <value>
        ///     The stamina regeneration maximum.
        /// </value>
        public int StaminaRegenerationMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the armor base.
        /// </summary>
        /// <value>
        ///     The armor base.
        /// </value>
        public int ArmorBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the armor maximum.
        /// </summary>
        /// <value>
        ///     The armor maximum.
        /// </value>
        public int ArmorMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the armor per level.
        /// </summary>
        /// <value>
        ///     The armor per level.
        /// </value>
        public int ArmorPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the magic resistance base.
        /// </summary>
        /// <value>
        ///     The magic resistance base.
        /// </value>
        public int MagicResistanceBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the magic resistance maximum.
        /// </summary>
        /// <value>
        ///     The magic resistance maximum.
        /// </value>
        public int MagicResistanceMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the magic resistance per level.
        /// </summary>
        /// <value>
        ///     The magic resistance per level.
        /// </value>
        public int MagicResistancePerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the attack speed base.
        /// </summary>
        /// <value>
        ///     The attack speed base.
        /// </value>
        public long AttackSpeedBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the attack speed maximum.
        /// </summary>
        /// <value>
        ///     The attack speed maximum.
        /// </value>
        public long AttackSpeedMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the attack speed per level.
        /// </summary>
        /// <value>
        ///     The attack speed per level.
        /// </value>
        public long AttackSpeedPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the cooldown reduction base.
        /// </summary>
        /// <value>
        ///     The cooldown reduction base.
        /// </value>
        public byte CooldownReductionBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the cooldown reduction maximum.
        /// </summary>
        /// <value>
        ///     The cooldown reduction maximum.
        /// </value>
        public byte CooldownReductionMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the cooldown reduction per level.
        /// </summary>
        /// <value>
        ///     The cooldown reduction per level.
        /// </value>
        public byte CooldownReductionPerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the critical strike base.
        /// </summary>
        /// <value>
        ///     The critical strike base.
        /// </value>
        public byte CriticalStrikeBase { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the critical strike maximum.
        /// </summary>
        /// <value>
        ///     The critical strike maximum.
        /// </value>
        public byte CriticalStrikeMax { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the critical strike per level.
        /// </summary>
        /// <value>
        ///     The critical strike per level.
        /// </value>
        public byte CriticalStrikePerLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public byte Id { get; set; }
    }
}