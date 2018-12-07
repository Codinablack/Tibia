using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Spells
{
    public class SpellService : IService
    {
        private readonly IDictionary<byte, ISpell> _spellById;
        private readonly ICollection<ISpell> _spells;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SpellService" /> class.
        /// </summary>
        /// <param name="spells">The spells.</param>
        public SpellService(IEnumerable<ISpell> spells)
        {
            _spells = new HashSet<ISpell>(spells);
            _spellById = _spells.ToDictionary(s => s.Id);
        }

        /// <summary>
        ///     Gets the spells.
        /// </summary>
        /// <value>
        ///     The spells.
        /// </value>
        public IEnumerable<ISpell> Spells => _spells;

        /// <summary>
        ///     Gets the spell by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The spell.</returns>
        public ISpell GetSpellById(byte id)
        {
            return _spellById.ContainsKey(id) ? _spellById[id] : null;
        }
    }
}