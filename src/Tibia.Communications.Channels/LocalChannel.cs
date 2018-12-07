using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public class LocalChannel : PublicChannel
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        public override string Name => "Default";

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public override ChannelType Type => ChannelType.Local;

        /// <inheritdoc />
        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="text">The text.</param>
        /// <param name="type">The type.</param>
        protected override void SendMessage(ICharacterSpawn characterSpawn, string text, SpeechType type)
        {
            foreach (ICharacterSpawn member in Members.Values)
                member.Connection.SendCreatureSpeech(characterSpawn, type, text, characterSpawn.Tile.Position);
        }
    }
}