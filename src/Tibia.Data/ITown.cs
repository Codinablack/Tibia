using Tibia.Core;

namespace Tibia.Data
{
    public interface ITown : IEntity<uint>
    {
        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; }

        /// <summary>
        ///     Gets the temple position.
        /// </summary>
        /// <value>
        ///     The temple position.
        /// </value>
        IVector3 TemplePosition { get; }
    }
}