using Tibia.Core;

namespace Tibia.Data
{
    public interface IVocation : IEntity<byte>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        VocationType Type { get; set; }

        /// <summary>
        ///     Gets or sets the capacity base.
        /// </summary>
        /// <value>
        ///     The capacity base.
        /// </value>
        int CapacityBase { get; set; }

        /// <summary>
        ///     Gets or sets the capacity maximum.
        /// </summary>
        /// <value>
        ///     The capacity maximum.
        /// </value>
        int CapacityMax { get; set; }

        /// <summary>
        ///     Gets or sets the capacity per level.
        /// </summary>
        /// <value>
        ///     The capacity per level.
        /// </value>
        int CapacityPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the health base.
        /// </summary>
        /// <value>
        ///     The health base.
        /// </value>
        int HealthBase { get; set; }

        /// <summary>
        ///     Gets or sets the health maximum.
        /// </summary>
        /// <value>
        ///     The health maximum.
        /// </value>
        int HealthMax { get; set; }

        /// <summary>
        ///     Gets or sets the health per level.
        /// </summary>
        /// <value>
        ///     The health per level.
        /// </value>
        int HealthPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the health regeneration maximum.
        /// </summary>
        /// <value>
        ///     The health regeneration maximum.
        /// </value>
        int HealthRegenerationMax { get; set; }

        /// <summary>
        ///     Gets or sets the health base regeneration.
        /// </summary>
        /// <value>
        ///     The health base regeneration.
        /// </value>
        int HealthBaseRegeneration { get; set; }

        /// <summary>
        ///     Gets or sets the health regeneration per level.
        /// </summary>
        /// <value>
        ///     The health regeneration per level.
        /// </value>
        int HealthRegenerationPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the resource base.
        /// </summary>
        /// <value>
        ///     The resource base.
        /// </value>
        int ResourceBase { get; set; }

        /// <summary>
        ///     Gets or sets the resource maximum.
        /// </summary>
        /// <value>
        ///     The resource maximum.
        /// </value>
        int ResourceMax { get; set; }

        /// <summary>
        ///     Gets or sets the resource per level.
        /// </summary>
        /// <value>
        ///     The resource per level.
        /// </value>
        int ResourcePerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the resource regeneration maximum.
        /// </summary>
        /// <value>
        ///     The resource regeneration maximum.
        /// </value>
        int ResourceRegenerationMax { get; set; }

        /// <summary>
        ///     Gets or sets the resource base regeneration.
        /// </summary>
        /// <value>
        ///     The resource base regeneration.
        /// </value>
        int ResourceBaseRegeneration { get; set; }

        /// <summary>
        ///     Gets or sets the resource regeneration.
        /// </summary>
        /// <value>
        ///     The resource regeneration.
        /// </value>
        int ResourceRegeneration { get; set; }

        /// <summary>
        ///     Gets or sets the armor penetration base.
        /// </summary>
        /// <value>
        ///     The armor penetration base.
        /// </value>
        int ArmorPenetrationBase { get; set; }

        /// <summary>
        ///     Gets or sets the armor penetration maximum.
        /// </summary>
        /// <value>
        ///     The armor penetration maximum.
        /// </value>
        int ArmorPenetrationMax { get; set; }

        /// <summary>
        ///     Gets or sets the armor penetration per level.
        /// </summary>
        /// <value>
        ///     The armor penetration per level.
        /// </value>
        int ArmorPenetrationPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the magic penetration base.
        /// </summary>
        /// <value>
        ///     The magic penetration base.
        /// </value>
        int MagicPenetrationBase { get; set; }

        /// <summary>
        ///     Gets or sets the magic penetration maximum.
        /// </summary>
        /// <value>
        ///     The magic penetration maximum.
        /// </value>
        int MagicPenetrationMax { get; set; }

        /// <summary>
        ///     Gets or sets the magic penetration per level.
        /// </summary>
        /// <value>
        ///     The magic penetration per level.
        /// </value>
        int MagicPenetrationPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the life steal base.
        /// </summary>
        /// <value>
        ///     The life steal base.
        /// </value>
        int LifeStealBase { get; set; }

        /// <summary>
        ///     Gets or sets the life steal maximum.
        /// </summary>
        /// <value>
        ///     The life steal maximum.
        /// </value>
        int LifeStealMax { get; set; }

        /// <summary>
        ///     Gets or sets the life steal per level.
        /// </summary>
        /// <value>
        ///     The life steal per level.
        /// </value>
        int LifeStealPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the spell vamp base.
        /// </summary>
        /// <value>
        ///     The spell vamp base.
        /// </value>
        int SpellVampBase { get; set; }

        /// <summary>
        ///     Gets or sets the spell vamp maximum.
        /// </summary>
        /// <value>
        ///     The spell vamp maximum.
        /// </value>
        int SpellVampMax { get; set; }

        /// <summary>
        ///     Gets or sets the spell vamp per level.
        /// </summary>
        /// <value>
        ///     The spell vamp per level.
        /// </value>
        int SpellVampPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the attack range base.
        /// </summary>
        /// <value>
        ///     The attack range base.
        /// </value>
        int AttackRangeBase { get; set; }

        /// <summary>
        ///     Gets or sets the attack range maximum.
        /// </summary>
        /// <value>
        ///     The attack range maximum.
        /// </value>
        int AttackRangeMax { get; set; }

        /// <summary>
        ///     Gets or sets the attack range per level.
        /// </summary>
        /// <value>
        ///     The attack range per level.
        /// </value>
        int AttackRangePerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the tenacity base.
        /// </summary>
        /// <value>
        ///     The tenacity base.
        /// </value>
        int TenacityBase { get; set; }

        /// <summary>
        ///     Gets or sets the tenacity maximum.
        /// </summary>
        /// <value>
        ///     The tenacity maximum.
        /// </value>
        int TenacityMax { get; set; }

        /// <summary>
        ///     Gets or sets the tenacity per level.
        /// </summary>
        /// <value>
        ///     The tenacity per level.
        /// </value>
        int TenacityPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the movement speed base.
        /// </summary>
        /// <value>
        ///     The movement speed base.
        /// </value>
        int MovementSpeedBase { get; set; }

        /// <summary>
        ///     Gets or sets the movement speed maximum.
        /// </summary>
        /// <value>
        ///     The movement speed maximum.
        /// </value>
        int MovementSpeedMax { get; set; }

        /// <summary>
        ///     Gets or sets the movement speed per level.
        /// </summary>
        /// <value>
        ///     The movement speed per level.
        /// </value>
        int MovementSpeedPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the soul base.
        /// </summary>
        /// <value>
        ///     The soul base.
        /// </value>
        byte SoulBase { get; set; }

        /// <summary>
        ///     Gets or sets the soul maximum.
        /// </summary>
        /// <value>
        ///     The soul maximum.
        /// </value>
        byte SoulMax { get; set; }

        /// <summary>
        ///     Gets or sets the soul per level.
        /// </summary>
        /// <value>
        ///     The soul per level.
        /// </value>
        byte SoulPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the soul base regeneration.
        /// </summary>
        /// <value>
        ///     The soul base regeneration.
        /// </value>
        byte SoulBaseRegeneration { get; set; }

        /// <summary>
        ///     Gets or sets the soul regeneration maximum.
        /// </summary>
        /// <value>
        ///     The soul regeneration maximum.
        /// </value>
        byte SoulRegenerationMax { get; set; }

        /// <summary>
        ///     Gets or sets the stamina maximum.
        /// </summary>
        /// <value>
        ///     The stamina maximum.
        /// </value>
        int StaminaMax { get; set; }

        /// <summary>
        ///     Gets or sets the stamina regeneration.
        /// </summary>
        /// <value>
        ///     The stamina regeneration.
        /// </value>
        int StaminaRegeneration { get; set; }

        /// <summary>
        ///     Gets or sets the stamina regeneration maximum.
        /// </summary>
        /// <value>
        ///     The stamina regeneration maximum.
        /// </value>
        int StaminaRegenerationMax { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        bool IsDefault { get; set; }

        /// <summary>
        ///     Gets or sets the armor base.
        /// </summary>
        /// <value>
        ///     The armor base.
        /// </value>
        int ArmorBase { get; set; }

        /// <summary>
        ///     Gets or sets the armor maximum.
        /// </summary>
        /// <value>
        ///     The armor maximum.
        /// </value>
        int ArmorMax { get; set; }

        /// <summary>
        ///     Gets or sets the armor per level.
        /// </summary>
        /// <value>
        ///     The armor per level.
        /// </value>
        int ArmorPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the magic resistance base.
        /// </summary>
        /// <value>
        ///     The magic resistance base.
        /// </value>
        int MagicResistanceBase { get; set; }

        /// <summary>
        ///     Gets or sets the magic resistance maximum.
        /// </summary>
        /// <value>
        ///     The magic resistance maximum.
        /// </value>
        int MagicResistanceMax { get; set; }

        /// <summary>
        ///     Gets or sets the magic resistance per level.
        /// </summary>
        /// <value>
        ///     The magic resistance per level.
        /// </value>
        int MagicResistancePerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the attack speed base.
        /// </summary>
        /// <value>
        ///     The attack speed base.
        /// </value>
        long AttackSpeedBase { get; set; }

        /// <summary>
        ///     Gets or sets the attack speed maximum.
        /// </summary>
        /// <value>
        ///     The attack speed maximum.
        /// </value>
        long AttackSpeedMax { get; set; }

        /// <summary>
        ///     Gets or sets the attack speed per level.
        /// </summary>
        /// <value>
        ///     The attack speed per level.
        /// </value>
        long AttackSpeedPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the cooldown reduction base.
        /// </summary>
        /// <value>
        ///     The cooldown reduction base.
        /// </value>
        byte CooldownReductionBase { get; set; }

        /// <summary>
        ///     Gets or sets the cooldown reduction maximum.
        /// </summary>
        /// <value>
        ///     The cooldown reduction maximum.
        /// </value>
        byte CooldownReductionMax { get; set; }

        /// <summary>
        ///     Gets or sets the cooldown reduction per level.
        /// </summary>
        /// <value>
        ///     The cooldown reduction per level.
        /// </value>
        byte CooldownReductionPerLevel { get; set; }

        /// <summary>
        ///     Gets or sets the critical strike base.
        /// </summary>
        /// <value>
        ///     The critical strike base.
        /// </value>
        byte CriticalStrikeBase { get; set; }

        /// <summary>
        ///     Gets or sets the critical strike maximum.
        /// </summary>
        /// <value>
        ///     The critical strike maximum.
        /// </value>
        byte CriticalStrikeMax { get; set; }

        /// <summary>
        ///     Gets or sets the critical strike per level.
        /// </summary>
        /// <value>
        ///     The critical strike per level.
        /// </value>
        byte CriticalStrikePerLevel { get; set; }
    }
}