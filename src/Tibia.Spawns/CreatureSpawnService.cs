using System.Collections.Generic;
using System.Linq;
using Tibia.Data;

namespace Tibia.Spawns
{
    public class CreatureSpawnService : ICreatureSpawnService
    {
        private readonly IDictionary<uint, ICreatureSpawn> _creatureSpawnById;
        private readonly ICollection<ICreatureSpawn> _creatureSpawns;
        private readonly IDictionary<string, HashSet<ICreatureSpawn>> _creatureSpawnsByName;
        private readonly ICollection<SpawnSource> _spawnSources;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreatureSpawnService" /> class.
        /// </summary>
        /// <param name="spawnSources">The spawn sources.</param>
        public CreatureSpawnService(IEnumerable<SpawnSource> spawnSources)
        {
            _spawnSources = new HashSet<SpawnSource>(spawnSources);
            _creatureSpawns = new HashSet<ICreatureSpawn>();
            _creatureSpawnById = _creatureSpawns.ToDictionary(s => s.Id);
            _creatureSpawnsByName = _creatureSpawns.GroupBy(s => s.Creature.Name.Trim().ToLower()).ToDictionary(s => s.Key, s => new HashSet<ICreatureSpawn>(s));
        }

        /// <summary>
        ///     Gets the total count.
        /// </summary>
        /// <value>
        ///     The total count.
        /// </value>
        public uint TotalCount => (uint) _creatureSpawns.Count;

        /// <summary>
        ///     Gets the character spawns.
        /// </summary>
        /// <value>
        ///     The character spawns.
        /// </value>
        public IEnumerable<ICharacterSpawn> CharacterSpawns => _creatureSpawns.OfType<ICharacterSpawn>();

        /// <summary>
        ///     Gets the spawn sources.
        /// </summary>
        /// <value>
        ///     The spawn sources.
        /// </value>
        public IEnumerable<SpawnSource> SpawnSources => _spawnSources;

        /// <summary>
        ///     Gets the creature spawns.
        /// </summary>
        /// <value>
        ///     The creature spawns.
        /// </value>
        public IEnumerable<ICreatureSpawn> CreatureSpawns => _creatureSpawns;

        /// <inheritdoc />
        /// <summary>
        ///     Gets the creature spawn by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The creature spawn.</returns>
        public ICreatureSpawn GetCreatureSpawnById(uint id)
        {
            return _creatureSpawnById.ContainsKey(id) ? _creatureSpawnById[id] : null;
        }

        /// <summary>
        ///     Registers the creature.
        /// </summary>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public void RegisterCreature(ICreatureSpawn creatureSpawn)
        {
            _creatureSpawns.Add(creatureSpawn);

            if (!_creatureSpawnById.ContainsKey(creatureSpawn.Id))
                _creatureSpawnById.Add(creatureSpawn.Id, null);

            _creatureSpawnById[creatureSpawn.Id] = creatureSpawn;

            string lowerCaseName = creatureSpawn.Creature.Name.Trim().ToLower();
            if (!_creatureSpawnsByName.ContainsKey(lowerCaseName))
                _creatureSpawnsByName.Add(lowerCaseName, new HashSet<ICreatureSpawn>());

            _creatureSpawnsByName[lowerCaseName].Add(creatureSpawn);
        }

        /// <summary>
        ///     Gets the creature spawns by name.
        /// </summary>
        /// <param name="creatureName">Name of the creature.</param>
        /// <returns>The creature spawns.</returns>
        public IEnumerable<ICreatureSpawn> GetCreatureSpawnsByName(string creatureName)
        {
            if (!_creatureSpawnsByName.ContainsKey(creatureName))
                yield break;

            foreach (ICreatureSpawn creatureSpawn in _creatureSpawnsByName[creatureName])
                yield return creatureSpawn;
        }
    }
}