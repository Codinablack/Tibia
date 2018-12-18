using System;
using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public class PrivateChannel : ChannelBase, IPrivateChannel, IQueryableChannel
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Communications.Channels.PrivateChannel" /> class.
        /// </summary>
        public PrivateChannel()
        {
            Invitations = new HashSet<ICharacterSpawn>();
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public new uint Id { get; set; }

        /// <inheritdoc cref="ChannelBase.Name" />
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public override string Name => $"{Owner.Character.Name}'s private channel";
        /// <summary>
        ///     Gets or sets the invitations.
        /// </summary>
        /// <value>
        ///     The invitations.
        /// </value>
        public ICollection<ICharacterSpawn> Invitations { get; }
        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public ICharacterSpawn Owner { get; set; }
        /// <summary>
        ///     Invites the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public void Invite(ICharacterSpawn characterSpawn)
        {
            if (Invitations.Contains(characterSpawn))
                return;

            Invitations.Add(characterSpawn);
        }
        /// <summary>
        ///     Uninvites the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public void Uninvite(ICharacterSpawn characterSpawn)
        {
            if (!Invitations.Contains(characterSpawn))
                return;

            Invitations.Remove(characterSpawn);
        }
        /// <summary>
        ///     Determines whether the specified character spawn is invited.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <returns>
        ///     <c>true</c> if the specified character spawn is invited; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInvited(ICharacterSpawn characterSpawn)
        {
            return Invitations.Contains(characterSpawn);
        }
        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public void Close()
        {
        }
        /// <summary>
        ///     Occurs when the channel is querying.
        /// </summary>
        public event EventHandler<QueryingChannelEventArgs> Querying;
        /// <summary>
        ///     Occurs when the channel is queried.
        /// </summary>
        public event EventHandler<QueriedChannelEventArgs> Queried;
        /// <summary>
        ///     Queries the channel with the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public void Query(ICharacterSpawn characterSpawn)
        {
            QueryingChannelEventArgs queryingChannelEventArgs = new QueryingChannelEventArgs(characterSpawn);
            Querying?.Invoke(this, queryingChannelEventArgs);
            if (queryingChannelEventArgs.Cancel)
                return;

            QueriedChannelEventArgs queriedChannelEventArgs = new QueriedChannelEventArgs(characterSpawn);
            Queried?.Invoke(this, queriedChannelEventArgs);
        }

        /// <summary>
        ///     Executes the query.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        protected virtual void ExecuteQuery(ICharacterSpawn characterSpawn)
        {
        }
    }
}