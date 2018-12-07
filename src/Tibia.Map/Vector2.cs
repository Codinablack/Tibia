using Tibia.Data;

namespace Tibia.Map
{
    public class Vector2 : IVector2
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector2" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        /// <value>
        ///     The x.
        /// </value>
        public int X { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public int Y { get; }
    }
}