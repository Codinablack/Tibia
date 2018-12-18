using Tibia.Data;

namespace Tibia.Outfits
{
    public class Outfit : IOutfit
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Outfit" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Outfit(string name)
        {
            Name = name;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Outfits.Outfit" /> class.
        /// </summary>
        public Outfit()
            : this(0, 0, 0, 0, 0, 0)
        {
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Outfits.Outfit" /> class.
        /// </summary>
        /// <param name="spriteId">The sprite identifier.</param>
        /// <param name="head">The head.</param>
        /// <param name="body">The body.</param>
        /// <param name="legs">The legs.</param>
        /// <param name="feet">The feet.</param>
        /// <param name="addons">The addons.</param>
        public Outfit(ushort spriteId, byte head, byte body, byte legs, byte feet, byte addons)
            : this(string.Empty)
        {
            SpriteId = spriteId;
            Item = 0;
            Head = head;
            Body = body;
            Legs = legs;
            Feet = feet;
            Addons = addons;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Outfits.Outfit" /> class.
        /// </summary>
        /// <param name="spriteId">The sprite identifier.</param>
        /// <param name="item">The item.</param>
        public Outfit(ushort spriteId, ushort item)
            : this(string.Empty)
        {
            SpriteId = spriteId;
            Item = item;
            Head = 0;
            Body = 0;
            Legs = 0;
            Feet = 0;
            Addons = 0;
        }
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Outfits.Outfit" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="spriteId">The sprite identifier.</param>
        /// <param name="addons">The addons.</param>
        public Outfit(string name, ushort spriteId, byte addons)
            : this(name)
        {
            SpriteId = spriteId;
            Item = 0;
            Head = 0;
            Body = 0;
            Legs = 0;
            Feet = 0;
            Addons = addons;
        }
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        public ushort SpriteId { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is premium.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is premium; otherwise, <c>false</c>.
        /// </value>
        public bool IsPremium { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is unlocked.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is unlocked; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnlocked { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is god tier.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is god tier; otherwise, <c>false</c>.
        /// </value>
        public bool IsGodTier { get; set; }
        /// <summary>
        ///     Gets or sets the addons.
        /// </summary>
        /// <value>
        ///     The addons.
        /// </value>
        public byte Addons { get; set; }
        /// <summary>
        ///     Gets or sets the body.
        /// </summary>
        /// <value>
        ///     The body.
        /// </value>
        public byte Body { get; set; }
        /// <summary>
        ///     Gets or sets the feet.
        /// </summary>
        /// <value>
        ///     The feet.
        /// </value>
        public byte Feet { get; set; }
        /// <summary>
        ///     Gets or sets the head.
        /// </summary>
        /// <value>
        ///     The head.
        /// </value>
        public byte Head { get; set; }
        /// <summary>
        ///     Gets or sets the legs.
        /// </summary>
        /// <value>
        ///     The legs.
        /// </value>
        public byte Legs { get; set; }
        /// <summary>
        ///     Gets or sets the sex.
        /// </summary>
        /// <value>
        ///     The sex.
        /// </value>
        public Sex Sex { get; set; }
        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        public ushort Item { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }
        /// <summary>
        ///     Gets or sets the corpse identifier.
        /// </summary>
        /// <value>
        ///     The corpse identifier.
        /// </value>
        public int CorpseId { get; set; }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
    }
}