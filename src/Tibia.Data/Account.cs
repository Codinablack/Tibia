using System;
using System.Collections.Generic;
using Tibia.InteropServices;

namespace Tibia.Data
{
    public class Account : IAccount
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Account" /> class.
        /// </summary>
        public Account()
        {
            Friends = new HashSet<IFriend>();
            CharacterSpawns = new HashSet<ICharacterSpawn>();
        }
        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        /// <value>
        ///     The user name.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        ///     Gets or sets the friends.
        /// </summary>
        /// <value>
        ///     The friends.
        /// </value>
        public ICollection<IFriend> Friends { get; set; }
        /// <summary>
        ///     Gets or sets the character spawns.
        /// </summary>
        /// <value>
        ///     The character spawns.
        /// </value>
        public ICollection<ICharacterSpawn> CharacterSpawns { get; set; }
        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; set; }
        /// <summary>
        ///     Gets or sets the operating system platform.
        /// </summary>
        /// <value>
        ///     The operating system platform.
        /// </value>
        public OSPlatform OSPlatform { get; set; }
        /// <summary>
        ///     Gets or sets the client version.
        /// </summary>
        /// <value>
        ///     The client version.
        /// </value>
        public ushort ClientVersion { get; set; }
        /// <summary>
        ///     Gets or sets the premium expiration date.
        /// </summary>
        /// <value>
        ///     The premium expiration date.
        /// </value>
        public DateTime PremiumExpirationDate { get; set; }
        /// <summary>
        ///     Gets or sets the notification.
        /// </summary>
        /// <value>
        ///     The notification.
        /// </value>
        public INotification Notification { get; set; }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
    }
}