using System;

namespace Tibia.Network.Game
{
    public class OnlineTimeEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.OnlineTimeEventArgs" /> class.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        public OnlineTimeEventArgs(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }

        /// <summary>
        ///     Gets the time span.
        /// </summary>
        /// <value>
        ///     The time span.
        /// </value>
        public TimeSpan TimeSpan { get; set; }
    }
}