using Tibia.Core;

namespace Tibia.Data
{
    public interface ICorpse : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        ushort SpriteId { get; set; }
    }
}