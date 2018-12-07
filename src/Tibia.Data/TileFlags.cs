using System;

namespace Tibia.Data
{
    [Flags]
    public enum TileFlags
    {
        /// <summary>
        ///     No flags.
        /// </summary>
        None = 0x0,

        /// <summary>
        ///     The protection zone flag.
        /// </summary>
        ProtectionZone = 0x1,

        /// <summary>
        ///     The no PVP zone flag.
        /// </summary>
        NoPvpZone = 0x2,

        /// <summary>
        ///     The no logout zone flag.
        /// </summary>
        NoLogoutZone = 0x4,

        /// <summary>
        ///     The PVP zone flag.
        /// </summary>
        PvpZone = 0x8
    }
}