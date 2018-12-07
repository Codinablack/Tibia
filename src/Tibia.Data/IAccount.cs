using System;
using System.Collections.Generic;
using Tibia.Core;
using Tibia.InteropServices;

namespace Tibia.Data
{
    public interface IAccount : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the client version.
        /// </summary>
        /// <value>
        ///     The client version.
        /// </value>
        ushort ClientVersion { get; set; }

        /// <summary>
        ///     Gets or sets the notification.
        /// </summary>
        /// <value>
        ///     The notification.
        /// </value>
        INotification Notification { get; set; }

        /// <summary>
        ///     Gets or sets the os platform.
        /// </summary>
        /// <value>
        ///     The os platform.
        /// </value>
        OSPlatform OSPlatform { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        ///     Gets or sets the premium expiration date.
        /// </summary>
        /// <value>
        ///     The premium expiration date.
        /// </value>
        DateTime PremiumExpirationDate { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        /// <value>
        ///     The user name.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the friends.
        /// </summary>
        /// <value>
        ///     The friends.
        /// </value>
        ICollection<IFriend> Friends { get; set; }

        /// <summary>
        ///     Gets or sets the character spawns.
        /// </summary>
        /// <value>
        ///     The character spawns.
        /// </value>
        ICollection<ICharacterSpawn> CharacterSpawns { get; set; }
    }
}