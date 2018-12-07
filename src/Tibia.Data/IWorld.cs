using Tibia.Core;

namespace Tibia.Data
{
    public interface IWorld : IEntity<byte>
    {
        /// <summary>
        ///     Gets or sets the ip address.
        /// </summary>
        /// <value>
        ///     The ip address.
        /// </value>
        string IpAddress { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is preview.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is preview; otherwise, <c>false</c>.
        /// </value>
        bool IsPreview { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the port.
        /// </summary>
        /// <value>
        ///     The port.
        /// </value>
        int Port { get; set; }
    }
}