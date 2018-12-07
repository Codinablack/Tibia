using Tibia.Data;

namespace Tibia.Map
{
    public class Waypoint : IWaypoint
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Waypoint" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="position">The position.</param>
        public Waypoint(string name, IVector3 position)
        {
            Name = name;
            Position = position;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public IVector3 Position { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; }
    }
}