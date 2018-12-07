using Tibia.Creatures;

namespace Tibia.Spawns
{
    public class NpcSpawnSettings : SpawnSettingsBase
    {
        /// <summary>
        ///     Gets or sets the NPC.
        /// </summary>
        /// <value>
        ///     The NPC.
        /// </value>
        public INpc Npc { get; set; }
    }
}