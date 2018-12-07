using System;
using System.Collections.Generic;
using Tibia.Core;

namespace Tibia.Data
{
    public interface IChannel : IEntity<ushort>
    {
        /// <summary>
        ///     Gets or sets the members.
        /// </summary>
        /// <value>
        ///     The members.
        /// </value>
        IDictionary<uint, ICharacterSpawn> Members { get; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; }

        /// <summary>
        ///     Occurs when a character is posting a message.
        /// </summary>
        event EventHandler<ChannelPostingEventArgs> Posting;

        /// <summary>
        ///     Occurs when a character posted a message.
        /// </summary>
        event EventHandler<ChannelPostedEventArgs> Posted;

        /// <summary>
        ///     Occurs when a character spawn is unregistering.
        /// </summary>
        event EventHandler<ChannelUnregisteringEventArgs> Unregistering;

        /// <summary>
        ///     Registers the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        void Register(ICharacterSpawn characterSpawn);

        /// <summary>
        ///     Unregisters the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        void Unregister(ICharacterSpawn characterSpawn);

        /// <summary>
        ///     Occurs when a character spawn is registered.
        /// </summary>
        event EventHandler<ChannelRegisteredEventArgs> Registered;

        /// <summary>
        ///     Occurs when a character spawn is unregistered.
        /// </summary>
        event EventHandler<ChannelUnregisteredEventArgs> Unregistered;

        /// <summary>
        ///     Posts the specified message.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="text">The text.</param>
        /// <param name="type">The type.</param>
        void Post(ICharacterSpawn characterSpawn, string text, SpeechType type);

        /// <summary>
        ///     Occurs when a character is registering.
        /// </summary>
        event EventHandler<ChannelRegisteringEventArgs> Registering;
    }
}