using System;

namespace Tibia.Data
{
    public struct LoginInfo : ILoginInfo
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public DateTime Time { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the IP address.
        /// </summary>
        /// <value>
        ///     The IP address.
        /// </value>
        public string IpAddress { get; set; }
    }
}