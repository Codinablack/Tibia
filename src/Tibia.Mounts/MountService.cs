using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Mounts
{
    public class MountService : IService
    {
        private readonly IDictionary<uint, IMount> _mountById;
        private readonly ICollection<IMount> _mounts;
        private readonly Dictionary<bool, HashSet<IMount>> _mountsByPremiumStatus;
        private readonly IDictionary<ushort, IMount> _mountBySpriteId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MountService" /> class.
        /// </summary>
        /// <param name="mounts">The mounts.</param>
        public MountService(IEnumerable<IMount> mounts)
        {
            _mounts = new HashSet<IMount>(mounts);
            _mountById = _mounts.ToDictionary(s => s.Id);
            _mountBySpriteId = _mounts.ToDictionary(s => s.SpriteId);
            _mountsByPremiumStatus = _mounts.GroupBy(s => s.IsPremium).ToDictionary(s => s.Key, s => new HashSet<IMount>(s));
        }

        /// <summary>
        ///     Gets the collection of mounts.
        /// </summary>
        /// <returns>The collection of mounts.</returns>
        public IEnumerable<IMount> GetAll()
        {
            return _mounts;
        }

        /// <summary>
        ///     Gets the mount by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The mount.</returns>
        public IMount GetMountById(uint id)
        {
            return _mountById.ContainsKey(id) ? _mountById[id] : null;
        }

        /// <summary>
        ///     Gets the mount by sprite identifier.
        /// </summary>
        /// <param name="spriteId">The sprite identifier.</param>
        /// <returns>The mount.</returns>
        public IMount GetMountBySpriteId(ushort spriteId)
        {
            return _mountBySpriteId.ContainsKey(spriteId) ? _mountBySpriteId[spriteId] : null;
        }

        /// <summary>
        ///     Gets the mounts by premium status.
        /// </summary>
        /// <param name="premium">if set to <c>true</c> [premium].</param>
        /// <returns>The collection of mounts.</returns>
        public IEnumerable<IMount> GetMountsByPremiumStatus(bool premium)
        {
            return _mountsByPremiumStatus.ContainsKey(premium) ? _mountsByPremiumStatus[premium] : null;
        }
    }
}