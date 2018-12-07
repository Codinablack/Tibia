using System.Collections.Generic;
using Tibia.Core;

namespace Tibia.Data
{
    public interface IParty : IEntity<ushort>
    {
        /// <summary>
        ///     Gets or sets the leader.
        /// </summary>
        /// <value>
        ///     The leader.
        /// </value>
        ICharacterSpawn Leader { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is actively sharing experience.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is actively sharing experience; otherwise, <c>false</c>.
        /// </value>
        bool IsActivelySharingExp { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has shared experience enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has shared experience enabled; otherwise, <c>false</c>.
        /// </value>
        bool HasSharedExpEnabled { get; set; }

        /// <summary>
        ///     Gets or sets the members.
        /// </summary>
        /// <value>
        ///     The members.
        /// </value>
        ICollection<ICharacterSpawn> Members { get; set; }

        /// <summary>
        ///     Gets or sets the invitations.
        /// </summary>
        /// <value>
        ///     The invitations.
        /// </value>
        ICollection<ICharacterSpawn> Invitations { get; set; }

        /// <summary>
        ///     Gets or sets the shared experience maximum range.
        /// </summary>
        /// <value>
        ///     The shared experience maximum range.
        /// </value>
        IVector3 SharedExpMaximumRange { get; set; }

        /// <summary>
        ///     Gets or sets the shared experience level.
        /// </summary>
        /// <value>
        ///     The shared experience level.
        /// </value>
        Percent SharedExpLevel { get; set; }

        /// <summary>
        ///     Gets or sets the shared experience multiple vocations bonus.
        /// </summary>
        /// <value>
        ///     The shared experience multiple vocations bonus.
        /// </value>
        Percent SharedExpMultipleVocationsBonus { get; set; }
    }
}