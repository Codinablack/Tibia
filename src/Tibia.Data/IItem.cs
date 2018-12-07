using System.Collections.Generic;
using Tibia.Core;

namespace Tibia.Data
{
    public interface IItem : IThing, IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the article.
        /// </summary>
        /// <value>
        ///     The article.
        /// </value>
        string Article { get; set; }

        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        ushort SpriteId { get; set; }

        /// <summary>
        ///     Gets or sets the type of the group.
        /// </summary>
        /// <value>
        ///     The type of the group.
        /// </value>
        ItemGroupType GroupType { get; set; }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        /// <value>
        ///     The speed.
        /// </value>
        ushort? Speed { get; set; }

        /// <summary>
        ///     Gets or sets the write once item identifier.
        /// </summary>
        /// <value>
        ///     The write once item identifier.
        /// </value>
        ushort? WriteOnceItemId { get; set; }

        /// <summary>
        ///     Gets or sets the features.
        /// </summary>
        /// <value>
        ///     The features.
        /// </value>
        ICollection<IItemFeature> Features { get; }

        /// <summary>
        ///     Gets or sets the floor change direction.
        /// </summary>
        /// <value>
        ///     The floor change direction.
        /// </value>
        FloorChangeDirection FloorChangeDirection { get; set; }
    }
}