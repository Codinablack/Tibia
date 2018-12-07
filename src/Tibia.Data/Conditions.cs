using System;

namespace Tibia.Data
{
    [Flags]
    public enum Conditions
    {
        /// <summary>
        ///     The default condition.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The poisoned condition.
        /// </summary>
        Poisoned = 1,

        /// <summary>
        ///     The burning condition.
        /// </summary>
        Burning = 2,

        /// <summary>
        ///     The electrified condition.
        /// </summary>
        Electrified = 4,

        /// <summary>
        ///     The drunk condition.
        /// </summary>
        Drunk = 8,

        /// <summary>
        ///     The magic shield condition.
        /// </summary>
        MagicShield = 16,

        /// <summary>
        ///     The slowed condition.
        /// </summary>
        Slowed = 32,

        /// <summary>
        ///     The hasted condition.
        /// </summary>
        Hasted = 64,

        /// <summary>
        ///     The in combat condition.
        /// </summary>
        InCombat = 128,

        /// <summary>
        ///     The drowning condition.
        /// </summary>
        Drowning = 256,

        /// <summary>
        ///     The freezing condition.
        /// </summary>
        Freezing = 512,

        /// <summary>
        ///     The dazzled condition.
        /// </summary>
        Dazzled = 1024,

        /// <summary>
        ///     The cursed condition.
        /// </summary>
        Cursed = 2048,

        /// <summary>
        ///     The strengthened condition.
        /// </summary>
        Strengthened = 4096,

        /// <summary>
        ///     The protection zone blocked condition.
        /// </summary>
        ProtectionZoneBlocked = 8192,

        /// <summary>
        ///     The in protection zone condition.
        /// </summary>
        InProtectionZone = 16384,

        /// <summary>
        ///     The bleeding condition.
        /// </summary>
        Bleeding = 32768,

        /// <summary>
        ///     The hungry condition.
        /// </summary>
        Hungry = 65536
    }
}