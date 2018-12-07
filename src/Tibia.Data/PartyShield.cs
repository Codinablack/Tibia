namespace Tibia.Data
{
    public enum PartyShield : byte
    {
        /// <summary>
        ///     The none type.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The host type.
        /// </summary>
        Host = 1,

        /// <summary>
        ///     The guest type.
        /// </summary>
        Guest = 2,

        /// <summary>
        ///     The member type.
        /// </summary>
        Member = 3,

        /// <summary>
        ///     The leader type.
        /// </summary>
        Leader = 4,

        /// <summary>
        ///     The member shared exp type.
        /// </summary>
        MemberSharedExp = 5,

        /// <summary>
        ///     The leader shared exp type.
        /// </summary>
        LeaderSharedExp = 6,

        /// <summary>
        ///     The member shared exp inactive type.
        /// </summary>
        MemberSharedExpInactive = 7,

        /// <summary>
        ///     The leader shared exp inactive type.
        /// </summary>
        LeaderSharedExpInactive = 8,

        /// <summary>
        ///     The member no shared exp type.
        /// </summary>
        MemberNoSharedExp = 9,

        /// <summary>
        ///     The leader no shared exp type.
        /// </summary>
        LeaderNoSharedExp = 10,

        /// <summary>
        ///     The other party member type.
        /// </summary>
        OtherPartyMember = 11
    }
}