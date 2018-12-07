using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class ItemUsePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        /// <value>
        ///     The index.
        /// </value>
        public byte Index { get; set; }

        /// <summary>
        ///     Gets or sets the stack position.
        /// </summary>
        /// <value>
        ///     The stack position.
        /// </value>
        public byte StackPosition { get; set; }

        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        public ushort SpriteId { get; set; }

        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public IVector3 Position { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static ItemUsePacket Parse(NetworkMessage message)
        {
            ItemUsePacket packet = new ItemUsePacket();
            packet.Position = message.ReadVector3();
            packet.SpriteId = message.ReadUInt16();
            packet.StackPosition = message.ReadByte();
            packet.Index = message.ReadByte();
            return packet;
        }
    }
}