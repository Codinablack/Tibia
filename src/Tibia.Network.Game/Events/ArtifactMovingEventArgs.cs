using System.ComponentModel;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class ArtifactMovingEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArtifactMovingEventArgs" /> class.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="spriteId">The sprite identifier.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="count">The count.</param>
        public ArtifactMovingEventArgs(IVector3 sourcePosition, byte sourceStackPosition, ushort spriteId, IVector3 targetPosition, byte count)
        {
            SourcePosition = sourcePosition;
            SourceStackPosition = sourceStackPosition;
            SpriteId = spriteId;
            TargetPosition = targetPosition;
            Count = count;
        }

        /// <summary>
        ///     Gets the source position.
        /// </summary>
        /// <value>
        ///     The source position.
        /// </value>
        public IVector3 SourcePosition { get; }

        /// <summary>
        ///     Gets the source stack position.
        /// </summary>
        /// <value>
        ///     The source stack position.
        /// </value>
        public byte SourceStackPosition { get; }

        /// <summary>
        ///     Gets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        public ushort SpriteId { get; }

        /// <summary>
        ///     Gets the target position.
        /// </summary>
        /// <value>
        ///     The target position.
        /// </value>
        public IVector3 TargetPosition { get; }

        /// <summary>
        ///     Gets the count.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public byte Count { get; }
    }
}