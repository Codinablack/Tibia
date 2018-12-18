using Tibia.Data;

namespace Tibia.Map
{
    public class Town : ITown
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Town" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="templePosition">The temple position.</param>
        public Town(uint id, string name, IVector3 templePosition)
        {
            Id = id;
            Name = name;
            TemplePosition = templePosition;
        }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; }
        /// <summary>
        ///     Gets the temple position.
        /// </summary>
        /// <value>
        ///     The temple position.
        /// </value>
        public IVector3 TemplePosition { get; }
    }
}