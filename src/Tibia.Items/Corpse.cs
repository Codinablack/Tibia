﻿using Tibia.Data;

namespace Tibia.Items
{
    public class Corpse : ICorpse
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        public ushort SpriteId { get; set; }
    }
}