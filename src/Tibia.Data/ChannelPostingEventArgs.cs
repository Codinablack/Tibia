using System.ComponentModel;

namespace Tibia.Data
{
    public class ChannelPostingEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.ChannelPostingEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="type">The type.</param>
        /// <param name="text">The text.</param>
        public ChannelPostingEventArgs(ICharacterSpawn characterSpawn, SpeechType type, string text)
        {
            CharacterSpawn = characterSpawn;
            Type = type;
            Text = text;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public ICharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public SpeechType Type { get; }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text { get; }
    }
}