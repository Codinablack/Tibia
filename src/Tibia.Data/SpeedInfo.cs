namespace Tibia.Data
{
    public class SpeedInfo
    {
        /// <summary>
        ///     Gets the walk speed.
        /// </summary>
        /// <value>
        ///     The walk speed.
        /// </value>
        public ushort WalkSpeed
        {
            get { return (ushort) (BonusSpeed + BaseSpeed); }
        }

        /// <summary>
        ///     Gets or sets the base speed.
        /// </summary>
        /// <value>
        ///     The base speed.
        /// </value>
        public ushort BaseSpeed { get; set; }

        /// <summary>
        ///     Gets or sets the bonus speed.
        /// </summary>
        /// <value>
        ///     The bonus speed.
        /// </value>
        public ushort BonusSpeed { get; set; }
    }
}