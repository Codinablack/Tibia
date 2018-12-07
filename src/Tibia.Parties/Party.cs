using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Parties
{
    public class Party : IParty
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the leader.
        /// </summary>
        /// <value>
        ///     The leader.
        /// </value>
        public ICharacterSpawn Leader { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is actively sharing experience.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is actively sharing experience; otherwise, <c>false</c>.
        /// </value>
        public bool IsActivelySharingExp { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance has shared experience enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has shared experience enabled; otherwise, <c>false</c>.
        /// </value>
        public bool HasSharedExpEnabled { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the members.
        /// </summary>
        /// <value>
        ///     The members.
        /// </value>
        public ICollection<ICharacterSpawn> Members { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the invitations.
        /// </summary>
        /// <value>
        ///     The invitations.
        /// </value>
        public ICollection<ICharacterSpawn> Invitations { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the shared experience maximum range.
        /// </summary>
        /// <value>
        ///     The shared experience maximum range.
        /// </value>
        public IVector3 SharedExpMaximumRange { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the shared experience level.
        /// </summary>
        /// <value>
        ///     The shared experience level.
        /// </value>
        public Percent SharedExpLevel { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the shared experience multiple vocations bonus.
        /// </summary>
        /// <value>
        ///     The shared experience multiple vocations bonus.
        /// </value>
        public Percent SharedExpMultipleVocationsBonus { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public ushort Id { get; set; }
    }
}