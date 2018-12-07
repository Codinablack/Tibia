using Tibia.Data;

namespace Tibia.Spawns
{
    public abstract class SpawnSettingsBase
    {
        /// <summary>
        ///     Gets or sets the absolute position.
        /// </summary>
        /// <value>
        ///     The absolute position.
        /// </value>
        public IVector3 AbsolutePosition { get; set; }

        /// <summary>
        ///     Gets or sets the spawn source.
        /// </summary>
        /// <value>
        ///     The spawn source.
        /// </value>
        public SpawnSource SpawnSource { get; set; }
    }
}