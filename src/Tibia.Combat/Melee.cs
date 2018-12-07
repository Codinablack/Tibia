namespace Tibia.Combat
{
    public class Melee : CombatBase
    {
        /// <summary>
        ///     Gets or sets the minimum.
        /// </summary>
        /// <value>
        ///     The minimum.
        /// </value>
        public short Min { get; set; }

        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>
        ///     The maximum.
        /// </value>
        public short Max { get; set; }

        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        /// <value>
        ///     The skill.
        /// </value>
        public byte Skill { get; set; }

        /// <summary>
        ///     Gets or sets the attack.
        /// </summary>
        /// <value>
        ///     The attack.
        /// </value>
        public ushort Attack { get; set; }
    }
}