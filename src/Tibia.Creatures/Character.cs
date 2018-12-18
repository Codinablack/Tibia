using Tibia.Data;

namespace Tibia.Creatures
{
    public class Character : Creature, ICharacter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Creatures.Character" /> class.
        /// </summary>
        public Character()
            : base(CreatureType.Character, SpeechBubble.None)
        {
        }
        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public SessionStatus Status { get; set; }
    }
}