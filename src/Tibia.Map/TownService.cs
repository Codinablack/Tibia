using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Map
{
    public class TownService : IService
    {
        private readonly IDictionary<uint, ITown> _townById;
        private readonly IDictionary<string, ITown> _townByName;
        private readonly ICollection<ITown> _towns;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TownService" /> class.
        /// </summary>
        /// <param name="towns">The towns.</param>
        public TownService(IEnumerable<ITown> towns)
        {
            _towns = new HashSet<ITown>(towns);
            _townById = _towns.ToDictionary(s => s.Id);
            _townByName = _towns.ToDictionary(s => s.Name.Trim().ToLower());
        }

        /// <summary>
        ///     Gets the towns.
        /// </summary>
        /// <value>
        ///     The towns.
        /// </value>
        public IEnumerable<ITown> Towns => _towns;

        /// <summary>
        ///     Gets the town by identifier.
        /// </summary>
        /// <param name="townId">The town identifier.</param>
        /// <returns>The town.</returns>
        public ITown GetTownById(uint townId)
        {
            return _townById.ContainsKey(townId) ? _townById[townId] : null;
        }

        /// <summary>
        ///     Gets the town by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The town</returns>
        public ITown GetTownByName(string name)
        {
            return _townByName.ContainsKey(name) ? _townByName[name] : null;
        }
    }
}