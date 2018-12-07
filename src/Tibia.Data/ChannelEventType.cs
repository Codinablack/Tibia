namespace Tibia.Data
{
    public enum ChannelEventType : byte
    {
        /// <summary>
        ///     The join event.
        /// </summary>
        Join = 0,

        /// <summary>
        ///     The leave event.
        /// </summary>
        Leave = 1,

        /// <summary>
        ///     The invite event.
        /// </summary>
        Invite = 2,

        /// <summary>
        ///     The revoke access event.
        /// </summary>
        RevokeAccess = 3
    }
}