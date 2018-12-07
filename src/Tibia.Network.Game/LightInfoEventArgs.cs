using System;
using Tibia.Map;

namespace Tibia.Network.Game
{
    public class LightInfoEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets or sets the light information.
        /// </summary>
        public LightInfo LightInfo { get; set; }
    }
}