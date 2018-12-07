namespace Tibia.Map
{
    public enum AutoWalkDirection : byte
    {
        /// <summary>
        ///     The default direction.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The east direction.
        /// </summary>
        East = 1,

        /// <summary>
        ///     The north east direction.
        /// </summary>
        NorthEast = 2,

        /// <summary>
        ///     The north direction.
        /// </summary>
        North = 3,

        /// <summary>
        ///     The north west direction.
        /// </summary>
        NorthWest = 4,

        /// <summary>
        ///     The west direction.
        /// </summary>
        West = 5,

        /// <summary>
        ///     The south west direction.
        /// </summary>
        SouthWest = 6,

        /// <summary>
        ///     The south direction.
        /// </summary>
        South = 7,

        /// <summary>
        ///     The south east direction.
        /// </summary>
        SouthEast = 8
    }
}