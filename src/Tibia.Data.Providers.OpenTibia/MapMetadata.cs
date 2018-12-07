namespace Tibia.Data.Providers.OpenTibia
{
    public class MapMetadata
    {
        /// <summary>
        ///     Gets or sets the version.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public uint Version { get; set; }

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public ushort Width { get; set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public ushort Height { get; set; }

        /// <summary>
        ///     Gets or sets the maximum item schema version.
        /// </summary>
        /// <value>
        ///     The maximum item schema version.
        /// </value>
        public uint MaxItemSchemaVersion { get; set; }

        /// <summary>
        ///     Gets or sets the minimum item schema version.
        /// </summary>
        /// <value>
        ///     The minimum item schema version.
        /// </value>
        public uint MinItemSchemaVersion { get; set; }
    }
}