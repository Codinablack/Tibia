namespace Tibia.Data
{
    public interface ISummon
    {
        /// <summary>
        ///     Gets or sets the master.
        /// </summary>
        /// <value>
        ///     The master.
        /// </value>
        ICharacterSpawn Master { get; set; }
    }
}