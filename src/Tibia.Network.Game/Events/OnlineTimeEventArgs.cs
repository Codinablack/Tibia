using System;

namespace Tibia.Network.Game.Events
{
    public class OnlineTimeEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.OnlineTimeEventArgs" /> class.
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