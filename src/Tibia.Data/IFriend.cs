using Tibia.Core;

namespace Tibia.Data
{
    public interface IFriend : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the account.
        /// </summary>
        /// <value>
        ///     The account.
        /// </value>
        IAccount Account { get; set; }

        /// <summary>
        ///     Gets or sets the account identifier.
        /// </summary>
        /// <value>
        ///     The account identifier.
        /// </value>
        uint AccountId { get; set; }

        /// <summary>
        ///     Gets or sets the character.
        /// </summary>
        /// <value>
        ///     The character.
        /// </value>
        ICharacter Character { get; set; }

        /// <summary>
        ///     Gets or sets the character spawn identifier.
        /// </summary>
        /// <value>
        ///     The character spawn identifier.
        /// </value>
        uint CharacterId { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        uint Icon { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [notify on login].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [notify on login]; otherwise, <c>false</c>.
        /// </value>
        bool NotifyOnLogin { get; set; }
    }
}