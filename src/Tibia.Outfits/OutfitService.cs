using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Outfits
{
    public class OutfitService : IService
    {
        private readonly IDictionary<uint, IOutfit> _outfitById;
        private readonly ICollection<IOutfit> _outfits;
        private readonly Dictionary<bool, HashSet<IOutfit>> _outfitsByPremiumStatus;
        private readonly IDictionary<Sex, HashSet<IOutfit>> _outfitsBySex;
        private readonly IDictionary<ushort, IOutfit> _outfitBySpriteId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OutfitService" /> class.
        /// </summary>
        /// <param name="outfits">The outfits.</param>
        public OutfitService(IEnumerable<IOutfit> outfits)
        {
            _outfits = new HashSet<IOutfit>(outfits);
            _outfitById = _outfits.ToDictionary(s => s.Id);
            _outfitsBySex = _outfits.GroupBy(s => s.Sex).ToDictionary(s => s.Key, s => new HashSet<IOutfit>(s));
            _outfitBySpriteId = _outfits.ToDictionary(s => s.SpriteId);
            _outfitsByPremiumStatus = _outfits.GroupBy(s => s.IsPremium).ToDictionary(s => s.Key, s => new HashSet<IOutfit>(s));
        }

        /// <summary>
        ///     Gets the collection of outfits.
        /// </summary>
        /// <returns>The collection of outfits.</returns>
        public IEnumerable<IOutfit> GetAll()
        {
            return _outfits;
        }

        /// <summary>
        ///     Gets the outfit by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The outfit.</returns>
        public IOutfit GetOutfitById(uint id)
        {
            return _outfitById.ContainsKey(id) ? _outfitById[id] : null;
        }

        /// <summary>
        ///     Gets the outfits by sex.
        /// </summary>
        /// <param name="sex">The sex.</param>
        /// <returns>The collection of outfits.</returns>
        public IEnumerable<IOutfit> GetOutfitsBySex(Sex sex)
        {
            return _outfitsBySex.ContainsKey(sex) ? _outfitsBySex[sex] : null;
        }

        /// <summary>
        ///     Gets the outfit by sprite identifier.
        /// </summary>
        /// <param name="spriteId">The sprite identifier.</param>
        /// <returns>The outfit.</returns>
        public IOutfit GetOutfitBySpriteId(ushort spriteId)
        {
            return _outfitBySpriteId.ContainsKey(spriteId) ? _outfitBySpriteId[spriteId] : null;
        }

        /// <summary>
        ///     Gets the outfits by premium status.
        /// </summary>
        /// <param name="premium">if set to <c>true</c> [premium].</param>
        /// <returns>The collection of outfits.</returns>
        public IEnumerable<IOutfit> GetOutfitsByPremiumStatus(bool premium)
        {
            return _outfitsByPremiumStatus.ContainsKey(premium) ? _outfitsByPremiumStatus[premium] : null;
        }
    }
}