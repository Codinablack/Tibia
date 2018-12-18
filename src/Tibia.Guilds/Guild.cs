using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Guilds
{
    public class Guild : IGuild
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Guild" /> class.
        /// </summary>
        public Guild()
        {
            Ranks = new List<IGuildRank>();
        }
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        ///     Gets or sets the ranks.
        /// </summary>
        /// <value>
        ///     The ranks.
        /// </value>
        public List<IGuildRank> Ranks { get; set; }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public ushort Id { get; set; }
    }
}