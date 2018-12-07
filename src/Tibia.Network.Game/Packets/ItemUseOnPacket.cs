using Tibia.Map;

namespace Tibia.Network.Game.Packets
{
    public class ItemUseOnPacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets from stack position.
        /// </summary>
        /// <value>
        ///     From stack position.
        /// </value>
        public byte FromStackPosition { get; set; }

        /// <summary>
        ///     Gets or sets from sprite identifier.
        /// </summary>
        /// <value>
        ///     From sprite identifier.
        /// </value>
        public int FromSpriteId { get; set; }

        /// <summary>
        ///     Gets or sets from position.
        /// </summary>
        /// <value>
        ///     From position.
        /// </value>
        public Vector3 FromPosition { get; set; }

        /// <summary>
        ///     Gets or sets to stack position.
        /// </summary>
        /// <value>
        ///     To stack position.
        /// </value>
        public byte ToStackPosition { get; set; }

        /// <summary>
        ///     Gets or sets to sprite identifier.
        /// </summary>
        /// <value>
        ///     To sprite identifier.
        /// </value>
        public int ToSpriteId { get; set; }

        /// <summary>
        ///     Gets or sets to position.
        /// </summary>
        /// <value>
        ///     To position.
        /// </value>
        public Vector3 ToPosition { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static ItemUseOnPacket Parse(NetworkMessage message)
        {
            return new ItemUseOnPacket
            {
                FromPosition = message.ReadVector3(),
                FromSpriteId = message.ReadUInt16(),
                FromStackPosition = message.ReadByte(),
                ToPosition = message.ReadVector3(),
                ToSpriteId = message.ReadUInt16(),
                ToStackPosition = message.ReadByte()
            };
        }
    }
}