using Tibia.Creatures;

namespace Tibia.Spawns
{
    public class MonsterSpawnSettings : SpawnSettingsBase
    {
        /// <summary>
        ///     Gets or sets the monster.
        /// </summary>
        /// <value>
        ///     The monster.
        /// </value>
        public IMonster Monster { get; set; }
    }
}