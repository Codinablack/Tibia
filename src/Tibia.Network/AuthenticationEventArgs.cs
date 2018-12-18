using System.ComponentModel;

namespace Tibia.Network
{
    public class AuthenticationEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.AuthenticationEventArgs" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public AuthenticationEventArgs(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        ///     Gets the username.
        /// </summary>
        /// <value>
        ///     The username.
        /// </value>
        public string Username { get; }

        /// <summary>
        ///     Gets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        public string Password { get; }
    }
}