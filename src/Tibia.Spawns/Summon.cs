using Tibia.Data;

namespace Tibia.Spawns
{
    public class Summon : MonsterSpawn, ISummon
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the master.
        /// </summary>
        /// <value>
        ///     The master.
        /// </value>
        public ICharacterSpawn Master { get; set; }
    }
}