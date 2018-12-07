using System;

namespace Tibia.Data
{
    [Flags]
    public enum ItemFlags
    {
        /// <summary>
        ///     No flags.
        /// </summary>
        None = 0x0,

        /// <summary>
        ///     The solid block flag.
        /// </summary>
        SolidBlock = 0x1,

        /// <summary>
        ///     The projectile blocker flag.
        /// </summary>
        ProjectileBlocker = 0x2,

        /// <summary>
        ///     The path blocker flag.
        /// </summary>
        PathBlocker = 0x4,

        /// <summary>
        ///     The has height flag.
        /// </summary>
        HasHeight = 0x8,

        /// <summary>
        ///     The useable flag.
        /// </summary>
        Useable = 0x10,

        /// <summary>
        ///     The pickupable flag.
        /// </summary>
        Pickupable = 0x20,

        /// <summary>
        ///     The moveable flag.
        /// </summary>
        Moveable = 0x40,

        /// <summary>
        ///     The stackable flag.
        /// </summary>
        Stackable = 0x80,

        /// <summary>
        ///     The floor change down flag.
        /// </summary>
        FloorChangeDown = 0x100,

        /// <summary>
        ///     The floor change north flag.
        /// </summary>
        FloorChangeNorth = 0x200,

        /// <summary>
        ///     The floor change east flag.
        /// </summary>
        FloorChangeEast = 0x400,

        /// <summary>
        ///     The floor change south flag.
        /// </summary>
        FloorChangeSouth = 0x800,

        /// <summary>
        ///     The floor change west flag.
        /// </summary>
        FloorChangeWest = 0x1000,

        /// <summary>
        ///     The always on top flag.
        /// </summary>
        AlwaysOnTop = 0x2000,

        /// <summary>
        ///     The readable flag.
        /// </summary>
        Readable = 0x4000,

        /// <summary>
        ///     The rotatable flag.
        /// </summary>
        Rotatable = 0x8000,

        /// <summary>
        ///     The hangable flag.
        /// </summary>
        Hangable = 0x10000,

        /// <summary>
        ///     The vertical flag.
        /// </summary>
        Vertical = 0x20000,

        /// <summary>
        ///     The horizontal flag.
        /// </summary>
        Horizontal = 0x40000,

        /// <summary>
        ///     The cannot decay flag.
        /// </summary>
        CannotDecay = 0x80000,

        /// <summary>
        ///     The distance readable flag.
        /// </summary>
        DistanceReadable = 0x100000,

        /// <summary>
        ///     The unused flag.
        /// </summary>
        Unused = 0x200000,

        /// <summary>
        ///     The client charges flag.
        /// </summary>
        ClientCharges = 0x400000,

        /// <summary>
        ///     The look through flag.
        /// </summary>
        LookThrough = 0x800000,

        /// <summary>
        ///     The animation flag.
        /// </summary>
        Animation = 0x1000000,

        /// <summary>
        ///     The full tile flag.
        /// </summary>
        FullTile = 0x2000000,

        /// <summary>
        ///     The force use flag.
        /// </summary>
        ForceUse = 0x4000000
    }
}