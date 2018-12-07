using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Vocations
{
    public class VocationService : IService
    {
        private readonly IDictionary<byte, IVocation> _vocationById;
        private readonly ICollection<IVocation> _vocations;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VocationService" /> class.
        /// </summary>
        /// <param name="vocations">The vocations.</param>
        public VocationService(IEnumerable<IVocation> vocations)
        {
            _vocations = new HashSet<IVocation>(vocations);
            _vocationById = _vocations.ToDictionary(s => s.Id);
        }

        /// <summary>
        ///     Gets the vocations.
        /// </summary>
        /// <value>
        ///     The vocations.
        /// </value>
        public IEnumerable<IVocation> Vocations => _vocations;

        /// <summary>
        ///     Gets the vocation by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The vocation.</returns>
        public IVocation GetVocationById(byte id)
        {
            return _vocationById.ContainsKey(id) ? _vocationById[id] : null;
        }
    }
}