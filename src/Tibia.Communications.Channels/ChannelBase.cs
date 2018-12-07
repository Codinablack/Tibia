using System;
using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public abstract class ChannelBase : IChannel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelBase" /> class.
        /// </summary>
        protected ChannelBase()
        {
            Members = new Dictionary<uint, ICharacterSpawn>();
            Posting += OnPosting;
        }

        /// <summary>
        ///     Gets the minimum level.
        /// </summary>
        /// <value>
        ///     The minimum level.
        /// </value>
        public virtual int MinLevel => 0;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the members.
        /// </summary>
        /// <value>
        ///     The members.
        /// </value>
        public IDictionary<uint, ICharacterSpawn> Members { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public abstract string Name { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when a character is posting a message.
        /// </summary>
        public event EventHandler<ChannelPostingEventArgs> Posting;

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when a character posted a message.
        /// </summary>
        public event EventHandler<ChannelPostedEventArgs> Posted;

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when a character spawn is unregistering.
        /// </summary>
        public event EventHandler<ChannelUnregisteringEventArgs> Unregistering;

        /// <inheritdoc />
        /// <summary>
        ///     Registers the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public void Register(ICharacterSpawn characterSpawn)
        {
            if (Members.ContainsKey(characterSpawn.Id))
                return;

            ChannelRegisteringEventArgs channelRegisteringEventArgs = new ChannelRegisteringEventArgs(characterSpawn);
            Registering?.Invoke(this, channelRegisteringEventArgs);
            if (channelRegisteringEventArgs.Cancel)
                return;

            Members.Add(characterSpawn.Id, characterSpawn);

            ChannelRegisteredEventArgs channelRegisteredEventArgs = new ChannelRegisteredEventArgs(characterSpawn);
            Registered?.Invoke(this, channelRegisteredEventArgs);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Unregisters the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public void Unregister(ICharacterSpawn characterSpawn)
        {
            if (!Members.ContainsKey(characterSpawn.Id))
                return;

            ChannelUnregisteringEventArgs channelUnregisteringEventArgs = new ChannelUnregisteringEventArgs(characterSpawn);
            Unregistering?.Invoke(this, channelUnregisteringEventArgs);
            if (channelUnregisteringEventArgs.Cancel)
                return;

            Members.Remove(characterSpawn.Id);

            ChannelUnregisteredEventArgs channelUnregisteredEventArgs = new ChannelUnregisteredEventArgs(characterSpawn);
            Unregistered?.Invoke(this, channelUnregisteredEventArgs);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when a character spawn is registered.
        /// </summary>
        public event EventHandler<ChannelRegisteredEventArgs> Registered;

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when a character spawn is unregistered.
        /// </summary>
        public event EventHandler<ChannelUnregisteredEventArgs> Unregistered;

        /// <inheritdoc />
        /// <summary>
        ///     Posts the specified message.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="text">The text.</param>
        /// <param name="type">The type.</param>
        public void Post(ICharacterSpawn characterSpawn, string text, SpeechType type)
        {
            ChannelPostingEventArgs channelPostingEventArgs = new ChannelPostingEventArgs(characterSpawn, type, text);
            Posting?.Invoke(this, channelPostingEventArgs);
            if (channelPostingEventArgs.Cancel)
                return;

            SendMessage(characterSpawn, channelPostingEventArgs.Text, channelPostingEventArgs.Type);

            ChannelPostedEventArgs channelPostedEventArgs = new ChannelPostedEventArgs(characterSpawn, channelPostingEventArgs.Type, channelPostingEventArgs.Text);
            Posted?.Invoke(this, channelPostedEventArgs);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Occurs when a character spawn is registering.
        /// </summary>
        public event EventHandler<ChannelRegisteringEventArgs> Registering;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public ushort Id { get; set; }

        /// <summary>
        ///     Called when [posting].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelPostingEventArgs" /> instance containing the event data.</param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnPosting(object sender, ChannelPostingEventArgs e)
        {
            if (e.CharacterSpawn.Level.Current >= MinLevel)
                return;

            e.CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "You need to be at least level 10 to speak into this channel.");
            e.Cancel = true;
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="text">The text.</param>
        /// <param name="type">The type.</param>
        protected virtual void SendMessage(ICharacterSpawn characterSpawn, string text, SpeechType type)
        {
            foreach (ICharacterSpawn member in Members.Values)
                member.Connection.SendChannelMessage(characterSpawn, Id, type, text);
        }
    }
}