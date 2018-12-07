namespace Tibia.Data
{
    public enum CreatureType : byte
    {
        /// <summary>
        ///     The character type.
        /// </summary>
        Character = 0,

        /// <summary>
        ///     The monster type.
        /// </summary>
        Monster = 1,

        /// <summary>
        ///     The NPC type.
        /// </summary>
        Npc = 2,

        /// <summary>
        ///     The summon own type.
        /// </summary>
        SummonOwn = 3,

        /// <summary>
        ///     The summon others type.
        /// </summary>
        SummonOthers = 4
    }
}