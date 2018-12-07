namespace Tibia.Data
{
    public enum SpeechType : byte
    {
        /// <summary>
        ///     The say type.
        /// </summary>
        Say = 1,

        /// <summary>
        ///     The whisper type.
        /// </summary>
        Whisper = 2,

        /// <summary>
        ///     The yell type.
        /// </summary>
        Yell = 3,

        /// <summary>
        ///     The private from type.
        /// </summary>
        PrivateFrom = 4,

        /// <summary>
        ///     The private to type.
        /// </summary>
        PrivateTo = 5,

        /// <summary>
        ///     The yellow type.
        /// </summary>
        Yellow = 7,

        /// <summary>
        ///     The orange type.
        /// </summary>
        Orange = 8,

        /// <summary>
        ///     The private NP type.
        /// </summary>
        PrivateNp = 10,

        /// <summary>
        ///     The private PN type.
        /// </summary>
        PrivatePn = 12,

        /// <summary>
        ///     The broadcast type.
        /// </summary>
        Broadcast = 13,

        /// <summary>
        ///     The red type. This is used in the form of <code>c# text</code> by the legacy client.
        /// </summary>
        Red = 14,

        /// <summary>
        ///     The private red from type. This is used in the form of <code>@name@text</code> by the legacy client.
        /// </summary>
        PrivateRedFrom = 15,

        /// <summary>
        ///     The private red to type. This is used in the form of <code>@name@text</code> by the legacy client.
        /// </summary>
        PrivateRedTo = 16,

        /// <summary>
        ///     The monster say type.
        /// </summary>
        MonsterSay = 36,

        /// <summary>
        ///     The monster yell type.
        /// </summary>
        MonsterYell = 37,

        /// <summary>
        ///     The channel R2 type. This is used in the form of <code>d#</code> by the legacy client.
        /// </summary>
        ChannelR2 = 0xFF
    }
}