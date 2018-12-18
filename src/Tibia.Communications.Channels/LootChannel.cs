using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public class LootChannel : ChannelBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Communications.Channels.LootChannel" /> class.
        /// </summary>
        public LootChannel()
        {
            Registered += OnRegistered;
            Unregistered += OnUnregistered;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public override string Name => "Loot";

        /// <summary>
        ///     Called when [unregistered].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelUnregisteredEventArgs" /> instance containing the event data.</param>
        private void OnUnregistered(object sender, ChannelUnregisteredEventArgs e)
        {
            Posting -= OnPosting;
        }

        /// <summary>
        ///     Called when [registered].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelRegisteredEventArgs" /> instance containing the event data.</param>
        private void OnRegistered(object sender, ChannelRegisteredEventArgs e)
        {
            Posting += OnPosting;
        }

        /// <summary>
        ///     Called when [posting].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ChannelPostingEventArgs" /> instance containing the event data.</param>
        private static void OnPosting(object sender, ChannelPostingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}