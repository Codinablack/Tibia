﻿using System;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class OpenedChannelEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.OpenedChannelEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="channel">The channel.</param>
        public OpenedChannelEventArgs(CharacterSpawn characterSpawn, IChannel channel)
        {
            CharacterSpawn = characterSpawn;
            Channel = channel;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the channel.
        /// </summary>
        /// <value>
        ///     The channel.
        /// </value>
        public IChannel Channel { get; }
    }
}