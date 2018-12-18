using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game.Events
{
    public class UsingItemEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.UsingItemEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        /// <param name="index">The index.</param>
        /// <param name="spriteId">The sprite identifier.</param>
        public UsingItemEventArgs(CharacterSpawn characterSpawn, IVector3 position, byte stackPosition, byte index, ushort spriteId)
        {
            CharacterSpawn = characterSpawn;
            Position = position;
            StackPosition = stackPosition;
            Index = index;
            SpriteId = spriteId;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public IVector3 Position { get; }

        /// <summary>
        ///     Gets the stack position.
        /// </summary>
        /// <value>
        ///     The stack position.
        /// </value>
        public byte StackPosition { get; }

        /// <summary>
        ///     Gets the index.
        /// </summary>
        /// <value>
        ///     The index.
        /// </value>
        public byte Index { get; }

        /// <summary>
        ///     Gets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        public ushort SpriteId { get; }
    }
}