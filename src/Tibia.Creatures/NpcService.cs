using System.Collections.Generic;
using System.Linq;
using Tibia.Data.Services;

namespace Tibia.Creatures
{
    public class NpcService : IService
    {
        private readonly IDictionary<string, HashSet<INpc>> _npcByName;
        private readonly ICollection<INpc> _npcs;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NpcService" /> class.
        /// </summary>
        /// <param name="npcs">The NPC's.</param>
        public NpcService(IEnumerable<INpc> npcs)
        {
            _npcs = new HashSet<INpc>(npcs);
            _npcByName = _npcs.GroupBy(s => s.Name.Trim().ToLower()).ToDictionary(s => s.Key, s => new HashSet<INpc>(s));
        }

        /// <summary>
        ///     Gets the NPC's.
        /// </summary>
        /// <value>
        ///     The NPC's.
        /// </value>
        public IEnumerable<INpc> Npcs => _npcs;

        /// <summary>
        ///     Gets the NPC's by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The collection of NPC's.</returns>
        public IEnumerable<INpc> GetNpcsByName(string name)
        {
            name = name.Trim().ToLower();
            return _npcByName.ContainsKey(name) ? _npcByName[name] : null;
        }
    }
}