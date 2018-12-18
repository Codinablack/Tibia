using Tibia.Data;

namespace Tibia.Communications
{
    public class Friend : IFriend
    {
        /// <summary>
        ///     Gets or sets the character identifier.
        /// </summary>
        /// <value>
        ///     The character identifier.
        /// </value>
        public uint CharacterId { get; set; }
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        public uint Icon { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether [notify on login].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [notify on login]; otherwise, <c>false</c>.
        /// </value>
        public bool NotifyOnLogin { get; set; }
        /// <summary>
        ///     Gets or sets the account.
        /// </summary>
        /// <value>
        ///     The account.
        /// </value>
        public IAccount Account { get; set; }
        /// <summary>
        ///     Gets or sets the account identifier.
        /// </summary>
        /// <value>
        ///     The account identifier.
        /// </value>
        public uint AccountId { get; set; }
        /// <summary>
        ///     Gets or sets the character.
        /// </summary>
        /// <value>
        ///     The character.
        /// </value>
        public ICharacter Character { get; set; }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
    }
}