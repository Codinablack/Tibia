using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public class AdvertisingChannel : PublicQueryableChannel
    {
        private readonly IDictionary<uint, Stopwatch> _muteConditionByCharacterSpawnId;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Communications.Channels.AdvertisingChannel" /> class.
        /// </summary>
        public AdvertisingChannel()
        {
            _muteConditionByCharacterSpawnId = new Dictionary<uint, Stopwatch>();
            Registered += OnRegistered;
            Unregistered += OnUnregistered;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        public override string Name => "Advertising";

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        /// <inheritdoc />
        public override ChannelType Type => ChannelType.Advertising;

        /// <summary>
        ///     Gets the mute span.
        /// </summary>
        /// <value>
        ///     The mute span.
        /// </value>
        public TimeSpan MuteSpan { get; } = TimeSpan.FromMinutes(2);

        /// <inheritdoc />
        /// <summary>
        ///     Gets the minimum level.
        /// </summary>
        /// <value>
        ///     The minimum level.
        /// </value>
        public override int MinLevel => 10;

        /// <summary>
        ///     Called when [unregistered].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelUnregisteredEventArgs" /> instance containing the event data.</param>
        private void OnUnregistered(object sender, ChannelUnregisteredEventArgs e)
        {
            Posting -= OnPosting;
            Posted -= OnPosted;
        }

        /// <summary>
        ///     Called when [registered].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelRegisteredEventArgs" /> instance containing the event data.</param>
        private void OnRegistered(object sender, ChannelRegisteredEventArgs e)
        {
            if (!_muteConditionByCharacterSpawnId.ContainsKey(e.CharacterSpawn.Id))
                _muteConditionByCharacterSpawnId.Add(e.CharacterSpawn.Id, null);

            Posting += OnPosting;
            Posted += OnPosted;
        }

        /// <summary>
        ///     Called when [posted].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelPostedEventArgs" /> instance containing the event data.</param>
        private void OnPosted(object sender, ChannelPostedEventArgs e)
        {
            _muteConditionByCharacterSpawnId[e.CharacterSpawn.Id].Restart();
        }

        /// <summary>
        ///     Called when [posting].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelPostingEventArgs" /> instance containing the event data.</param>
        private void OnPosting(object sender, ChannelPostingEventArgs e)
        {
            if (_muteConditionByCharacterSpawnId[e.CharacterSpawn.Id] == null)
                _muteConditionByCharacterSpawnId[e.CharacterSpawn.Id] = new Stopwatch();

            // TODO: Permissions to ignore channel constraints
            Stopwatch muteCondition = _muteConditionByCharacterSpawnId[e.CharacterSpawn.Id];
            if (!muteCondition.IsRunning || muteCondition.Elapsed >= MuteSpan)
                return;

            e.CharacterSpawn.Connection.SendTextMessage(TextMessageType.StatusSmall, "You may place an offer every 2 minutes.");
            e.Cancel = true;
        }
    }
}