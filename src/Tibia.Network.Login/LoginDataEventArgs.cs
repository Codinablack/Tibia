using System;
using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Network.Login
{
    public class LoginDataEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Login.LoginDataEventArgs" /> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public LoginDataEventArgs(IAccount account)
        {
            Account = account;
            Characters = new HashSet<ICharacterSpawn>();
            Worlds = new HashSet<World>();
        }

        /// <summary>
        ///     Gets the account.
        /// </summary>
        /// <value>
        ///     The account.
        /// </value>
        public IAccount Account { get; }

        /// <summary>
        ///     Gets or sets the characters.
        /// </summary>
        /// <value>
        ///     The characters.
        /// </value>
        public ICollection<ICharacterSpawn> Characters { get; set; }

        /// <summary>
        ///     Gets or sets the worlds.
        /// </summary>
        /// <value>
        ///     The worlds.
        /// </value>
        public ICollection<World> Worlds { get; set; }

        /// <summary>
        ///     Gets or sets the notification.
        /// </summary>
        /// <value>
        ///     The notification.
        /// </value>
        public INotification Notification { get; set; }
    }
}