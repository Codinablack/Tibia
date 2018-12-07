using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Tibia.Data;
using Tibia.Network.Login.Packets;
using Tibia.Security.Cryptography;

namespace Tibia.Network.Login
{
    public class LoginConnection : Connection
    {
        /// <summary>
        ///     Occurs when the user is authenticated.
        /// </summary>
        public event EventHandler<AuthenticationEventArgs> Authenticate;

        /// <summary>
        ///     Occurs when the login data is requested.
        /// </summary>
        public event EventHandler<LoginDataEventArgs> RequestLoginData;

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Login.LoginConnection" /> class.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        public LoginConnection(Xtea xtea)
            : base(xtea)
        {
        }

        /// <summary>
        ///     Called when [message received].
        /// </summary>
        /// <param name="result">The result.</param>
        public void OnMessageReceived(IAsyncResult result)
        {
            TcpListener clientListener = (TcpListener) result.AsyncState;
            Socket = clientListener.EndAcceptSocket(result);
            Stream = new NetworkStream(Socket);
            Stream.BeginRead(IncomingMessage.Buffer, 0, 2, OnClientReadHandshakeMessage, null);
        }

        /// <summary>
        ///     Called when [client read handshake message].
        /// </summary>
        /// <param name="result">The result.</param>
        /// <exception cref="InvalidNetworkProtocolException"></exception>
        /// <exception cref="InvalidRsaException"></exception>
        /// <exception cref="AuthenticationFailedException"></exception>
        private void OnClientReadHandshakeMessage(IAsyncResult result)
        {
            // TODO: 2) Disconnect: Validation of timestamp and random number (sent before first login packet?)
            // TODO: 3) Disconnect: Only clients with current protocol allowed (Change to automatic update)
            // TODO: 4) Disconnect: Gamestate
            // TODO: 5) Disconnect: IP banishments (Change to automatic timer backwards)
            // TODO: 6) Character data (e.g.: Skin & Mount) to render on client side

            if (!EndRead(result))
                return;

            try
            {
                if (IncomingMessage.ReadNetworkProtocol() != NetworkProtocol.Login)
                    throw new InvalidNetworkProtocolException();

                AccountPacket accountPacket = AccountPacket.Parse(IncomingMessage);
                if (!accountPacket.IsRsaValid)
                    throw new InvalidRsaException();

                AuthenticationEventArgs authenticationEventArgs = new AuthenticationEventArgs(accountPacket.Username, accountPacket.Password);
                Authenticate?.Invoke(this, authenticationEventArgs);

                if (authenticationEventArgs.Cancel)
                    throw new AuthenticationFailedException();

                Xtea = accountPacket.Xtea;

                Account account = new Account
                {
                    UserName = accountPacket.Username,
                    Password = accountPacket.Password,
                    OSPlatform = accountPacket.OSPlatform,
                    ClientVersion = accountPacket.Version
                };

                LoginDataEventArgs loginDataEventArgs = new LoginDataEventArgs(account);
                RequestLoginData?.Invoke(this, loginDataEventArgs);

                SendLoginWindow((loginDataEventArgs.Account.PremiumExpirationDate - DateTime.Today).Days, loginDataEventArgs.Account.UserName, loginDataEventArgs.Account.Password, loginDataEventArgs.Characters, loginDataEventArgs.Worlds, loginDataEventArgs.Notification);
            }
            catch
            {
                // TODO: Log invalid data from the client
            }
            finally
            {
                Close();
            }
        }

        /// <summary>
        ///     Disconnects this instance with the specified reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public void Disconnect(string reason)
        {
            NetworkMessage message = new NetworkMessage(Xtea);

            DisconnectPacket.Add(message, reason);

            Send(message);
            Close();
        }

        /// <summary>
        ///     Sends the login window.
        /// </summary>
        /// <param name="remainingPremiumDays">The remaining premium days.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="characters">The characters.</param>
        /// <param name="worlds">The worlds.</param>
        /// <param name="messageOfTheDay">The message of the day.</param>
        public void SendLoginWindow(int remainingPremiumDays, string userName, string password, ICollection<ICharacterSpawn> characters, ICollection<World> worlds, INotification messageOfTheDay)
        {
            NetworkMessage message = new NetworkMessage(Xtea);

            MessageOfTheDayPacket.Add(message, messageOfTheDay);
            SessionKeyPacket.Add(message, userName, password);
            CharacterListPacket.Add(message, worlds, characters);
            PremiumDaysPacket.Add(message, remainingPremiumDays);

            Send(message);
        }
    }
}