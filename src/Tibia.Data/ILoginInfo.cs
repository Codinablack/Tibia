using System;

namespace Tibia.Data
{
    public interface ILoginInfo
    {
        /// <summary>
        ///     Gets or sets the ip address.
        /// </summary>
        /// <value>
        ///     The ip address.
        /// </value>
        string IpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        DateTime Time { get; set; }
    }
}