namespace Tibia.Data
{
    public enum TextMessageType : byte
    {
        /// <summary>
        ///     The console blue type.
        /// </summary>
        ConsoleBlue = 4,

        /// <summary>
        ///     The console red type.
        /// </summary>
        ConsoleRed = 13,

        /// <summary>
        ///     The status default type. This is rendered as white at the bottom of the game window and in the console.
        /// </summary>
        StatusDefault = 17,

        /// <summary>
        ///     The status warning type. This is rendered as red in the game window and in the console.
        /// </summary>
        StatusWarning = 18,

        /// <summary>
        ///     The event advance type. This is rendereed as white in the game window and in the console.
        /// </summary>
        EventAdvance = 19,

        /// <summary>
        ///     The status small type. This is rendered as white at the bottom of the game window.
        /// </summary>
        StatusSmall = 21,

        /// <summary>
        ///     The information description type. This is rendered as green in the game window and in the console.
        /// </summary>
        InformationDescription = 22,

        /// <summary>
        ///     The damage dealt type.
        /// </summary>
        DamageDealt = 23,

        /// <summary>
        ///     The damage received type.
        /// </summary>
        DamageReceived = 24,

        /// <summary>
        ///     The heal type.
        /// </summary>
        Heal = 25,

        /// <summary>
        ///     The experience type.
        /// </summary>
        Experience = 26,

        /// <summary>
        ///     The damage others type.
        /// </summary>
        DamageOthers = 27,

        /// <summary>
        ///     The heal others type.
        /// </summary>
        HealOthers = 28,

        /// <summary>
        ///     The experience others type.
        /// </summary>
        ExperienceOthers = 29,

        /// <summary>
        ///     The event default type. This is redered as white at the bottom of the game window and in the console.
        /// </summary>
        EventDefault = 30,

        /// <summary>
        ///     The loot type.
        /// </summary>
        Loot = 31,

        /// <summary>
        ///     The event orange type. This is rendered as orange in the console.
        /// </summary>
        EventOrange = 36,

        /// <summary>
        ///     The status orange type. This is rendered as orange in the console.s
        /// </summary>
        StatusOrange = 37
    }
}