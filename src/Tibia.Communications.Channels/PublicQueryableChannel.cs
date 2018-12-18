using System;
using Tibia.Data;

namespace Tibia.Communications.Channels
{
    public abstract class PublicQueryableChannel : PublicChannel, IQueryableChannel
    {
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