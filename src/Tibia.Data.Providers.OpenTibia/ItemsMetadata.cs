using System;

namespace Tibia.Data.Providers.OpenTibia
{
    public class ItemsMetadata
    {
        /// <summary>
        ///     The CSD version size.
        /// </summary>
        public const int CsdVersionSize = 128;

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public ushort InfoSize { get; set; }

        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public Version Version { get; set; }

        /// <summary>
        ///     Gets or sets the CSD version.
        /// </summary>
        /// <value>
        ///     The CSD version.
        /// </value>
        public byte[] CsdVersion { get; set; }
    }
}