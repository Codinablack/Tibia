using System.Collections.Generic;

namespace Tibia.Data
{
    public interface IPrivateChannel : IChannel
    {
        /// <summary>
        ///     Gets or sets the invitations.
        /// </summary>
        /// <value>
        ///     The invitations.
        /// </value>
        ICollection<ICharacterSpawn> Invitations { get; }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        ICharacterSpawn Owner { get; set; }

        /// <summary>
        ///     Invites the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        void Invite(ICharacterSpawn characterSpawn);

        /// <summary>
        ///     Uninvites the specified character spawn.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        void Uninvite(ICharacterSpawn characterSpawn);

        /// <summary>
        ///     Determines whether the specified character spawn is invited.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <returns>
        ///     <c>true</c> if the specified character spawn is invited; otherwise, <c>false</c>.
        /// </returns>
        bool IsInvited(ICharacterSpawn characterSpawn);

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        void Close();
    }
}