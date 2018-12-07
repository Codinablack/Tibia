using Tibia.Data;
using Tibia.Outfits;

namespace Tibia.Creatures
{
    public class Npc : Creature, INpc
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Creatures.Npc" /> class.
        /// </summary>
        /// <param name="speechBubble">The speech bubble.</param>
        public Npc(SpeechBubble speechBubble)
            : base(CreatureType.Npc, speechBubble)
        {
            Outfit = new Outfit();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Creatures.Npc" /> class.
        /// </summary>
        public Npc()
            : this(SpeechBubble.None)
        {
        }
    }
}