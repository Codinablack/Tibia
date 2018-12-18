using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class ArtifactMovePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public byte Count { get; set; }

        /// <summary>
        ///     Gets or sets the target position.
        /// </summary>
        /// <value>
        ///     The target position.
        /// </value>
        public IVector3 TargetPosition { get; set; }

        /// <summary>
        ///     Gets or sets the source stack position.
        /// </summary>
        /// <value>
        ///     The source stack position.
        /// </value>
        public byte SourceStackPosition { get; set; }

        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        public ushort SpriteId { get; set; }

        /// <summary>
        ///     Gets or sets the source position.
        /// </summary>
        /// <value>
        ///     The source position.
        /// </value>
        public IVector3 SourcePosition { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static ArtifactMovePacket Parse(NetworkMessage message)
        {
            ArtifactMovePacket packet = new ArtifactMovePacket();
            packet.SourcePosition = message.ReadVector3();
            packet.SpriteId = message.ReadUInt16();
            packet.SourceStackPosition = message.ReadByte();
            packet.TargetPosition = message.ReadVector3();
            packet.Count = message.ReadByte();
            return packet;
        }
    }
}