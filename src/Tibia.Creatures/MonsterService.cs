using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Creatures
{
    public class MonsterService : IService
    {
        private readonly ICollection<IMonster> _monsters;
        private readonly IDictionary<uint, HashSet<IMonster>> _monstersByGroupId;
        private readonly IDictionary<string, HashSet<IMonster>> _monstersByName;
        private readonly IDictionary<RaceType, HashSet<IMonster>> _monstersByRaceType;
        private readonly IDictionary<string, IMonster> _monsterByUniqueName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MonsterService" /> class.
        /// </summary>
        /// <param name="monsters">The monsters.</param>
        public MonsterService(IEnumerable<IMonster> monsters)
        {
            _monsters = new HashSet<IMonster>(monsters);
            _monstersByName = _monsters.GroupBy(s => s.Name.Trim().ToLower()).ToDictionary(s => s.Key, s => new HashSet<IMonster>(s));
            _monsterByUniqueName = _monsters.ToDictionary(s => s.UniqueName.Trim().ToLower());

            _monstersByGroupId = _monsters.GroupBy(s => s.MonsterGroupId).ToDictionary(s => s.Key, s => new HashSet<IMonster>(s));
            _monstersByRaceType = _monsters.GroupBy(s => s.RaceType).ToDictionary(s => s.Key, s => new HashSet<IMonster>(s));
        }

        /// <summary>
        ///     Gets the monsters.
        /// </summary>
        /// <value>
        ///     The monsters.
        /// </value>
        public IEnumerable<IMonster> Monsters => _monsters;

        /// <summary>
        ///     Gets the monsters by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The collection of monsters.</returns>
        public IEnumerable<IMonster> GetMonstersByName(string name)
        {
            name = name.Trim().ToLower();
            return _monstersByName.ContainsKey(name) ? _monstersByName[name] : null;
        }

        /// <summary>
        ///     Gets the monster by unique name.
        /// </summary>
        /// <param name="name">The unique name.</param>
        /// <returns>The monster.</returns>
        public IMonster GetMonsterByUniqueName(string name)
        {
            name = name.Trim().ToLower();
            return _monsterByUniqueName.ContainsKey(name) ? _monsterByUniqueName[name] : null;
        }

        /// <summary>
        ///     Gets the monsters by group identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The collection of monsters.</returns>
        public IEnumerable<IMonster> GetMonstersByGroupId(uint id)
        {
            return _monstersByGroupId.ContainsKey(id) ? _monstersByGroupId[id] : null;
        }

        /// <summary>
        ///     Gets the type of the monsters by race.
        /// </summary>
        /// <param name="raceType">Type of the race.</param>
        /// <returns>The collection of monsters.</returns>
        public IEnumerable<IMonster> GetMonstersByRaceType(RaceType raceType)
        {
            return _monstersByRaceType.ContainsKey(raceType) ? _monstersByRaceType[raceType] : null;
        }
    }
}