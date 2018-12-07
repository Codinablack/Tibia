using System;

namespace Tibia.Data
{
    public interface IQueryableChannel
    {
        /// <summary>
        ///     Occurs when the channel is querying.
        /// </summary>
        event EventHandler<QueryingChannelEventArgs> Querying;

        /// <summary>
        ///     Occurs when the channel is queried.
        /// </summary>
        event EventHandler<QueriedChannelEventArgs> Queried;

        /// <summary>
        ///     Queries the channel with the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        void Query(ICharacterSpawn characterSpawn);
    }
}