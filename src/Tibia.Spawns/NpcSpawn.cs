using Tibia.Creatures;

namespace Tibia.Spawns
{
    public class NpcSpawn : CreatureSpawn
    {
        /// <summary>
        ///     Gets or sets the NPC.
        /// </summary>
        /// <value>
        ///     The NPC.
        /// </value>
        public Npc Npc { get; set; }
    }
}