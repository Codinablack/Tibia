namespace Tibia.Data
{
    public class World : IWorld
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the IP address.
        /// </summary>
        /// <value>
        ///     The IP address.
        /// </value>
        public string IpAddress { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the port.
        /// </summary>
        /// <value>
        ///     The port.
        /// </value>
        public int Port { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is preview.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is preview; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreview { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the world identifier.
        /// </summary>
        /// <value>
        ///     The world identifier.
        /// </value>
        public byte Id { get; set; }
    }
}