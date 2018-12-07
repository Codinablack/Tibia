namespace Tibia.Data
{
    public enum SessionStatus : byte
    {
        /// <summary>
        ///     The offline status.
        /// </summary>
        Offline = 0,

        /// <summary>
        ///     The online status.
        /// </summary>
        Online = 1,

        /// <summary>
        ///     The pending status.
        /// </summary>
        Pending = 2
    }
}