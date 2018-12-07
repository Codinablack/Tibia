using Tibia.Core;

namespace Tibia.Data
{
    public interface ICreature : IThing, IEntity<uint>
    {
        /// <summary>
        ///     Gets the type of the creature.
        /// </summary>
        /// <value>
        ///     The type of the creature.
        /// </value>
        CreatureType CreatureType { get; }

        /// <summary>
        ///     Gets or sets the speech bubble.
        /// </summary>
        /// <value>
        ///     The speech bubble.
        /// </value>
        SpeechBubble SpeechBubble { get; set; }

        /// <summary>
        ///     Gets or sets the base speed.
        /// </summary>
        /// <value>
        ///     The base speed.
        /// </value>
        SpeedInfo BaseSpeed { get; set; }

        /// <summary>
        ///     Gets or sets the maximum health.
        /// </summary>
        /// <value>
        ///     The maximum health.
        /// </value>
        uint MaxHealth { get; set; }

        /// <summary>
        ///     Gets or sets the settings.
        /// </summary>
        /// <value>
        ///     The settings.
        /// </value>
        CreatureSettings Settings { get; set; }

        /// <summary>
        ///     Gets or sets the outfit.
        /// </summary>
        /// <value>
        ///     The outfit.
        /// </value>
        IOutfit Outfit { get; set; }

        /// <summary>
        ///     Gets or sets the mount.
        /// </summary>
        /// <value>
        ///     The mount.
        /// </value>
        IMount Mount { get; set; }
    }
}