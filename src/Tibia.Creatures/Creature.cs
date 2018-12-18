using Tibia.Data;

namespace Tibia.Creatures
{
    public abstract class Creature : ICreature
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Creature" /> class.
        /// </summary>
        /// <param name="creatureType">Type of the creature.</param>
        /// <param name="speechBubble">The speech bubble.</param>
        protected Creature(CreatureType creatureType, SpeechBubble speechBubble)
        {
            CreatureType = creatureType;
            SpeechBubble = speechBubble;
            BaseSpeed = new SpeedInfo();
            Settings = new CreatureSettings();
        }
        /// <summary>
        ///     Gets or sets the maximum health.
        /// </summary>
        /// <value>
        ///     The maximum health.
        /// </value>
        public uint MaxHealth { get; set; }
        /// <summary>
        ///     Gets or sets the settings.
        /// </summary>
        /// <value>
        ///     The settings.
        /// </value>
        public CreatureSettings Settings { get; set; }
        /// <summary>
        ///     Gets or sets the outfit.
        /// </summary>
        /// <value>
        ///     The outfit.
        /// </value>
        public IOutfit Outfit { get; set; }
        /// <summary>
        ///     Gets or sets the mount.
        /// </summary>
        /// <value>
        ///     The mount.
        /// </value>
        public IMount Mount { get; set; }
        /// <summary>
        ///     Gets or sets the type of the creature.
        /// </summary>
        /// <value>
        ///     The type of the creature.
        /// </value>
        public CreatureType CreatureType { get; }
        /// <summary>
        ///     Gets or sets the speech bubble.
        /// </summary>
        /// <value>
        ///     The speech bubble.
        /// </value>
        public SpeechBubble SpeechBubble { get; set; }
        /// <summary>
        ///     Gets or sets the base speed.
        /// </summary>
        /// <value>
        ///     The base speed.
        /// </value>
        public SpeedInfo BaseSpeed { get; set; }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
        /// <summary>
        ///     Gets the render priority.
        /// </summary>
        /// <value>
        ///     The render priority.
        /// </value>
        public RenderPriority RenderPriority { get; set; } = RenderPriority.Medium;
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }
    }
}