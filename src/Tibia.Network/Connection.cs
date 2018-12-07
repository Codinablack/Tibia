using System;
using System.Net.Sockets;
using Tibia.Security.Cryptography;

namespace Tibia.Network
{
    public abstract class Connection
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Connection" /> class.
        /// </summary>
        /// <param name="xtea">The XTEA key.</param>
        protected Connection(Xtea xtea)
        {
            Xtea = xtea;
            IsInTransaction = false;
            IncomingMessage = new NetworkMessage(xtea, 0);
        }

        /// <summary>
        ///     Gets the incoming message.
        /// </summary>
        /// <value>
        ///     The incoming message.
        /// </value>
        public NetworkMessage IncomingMessage { get; }

        /// <summary>
        ///     Gets the IP address.
        /// </summary>
        /// <value>
        ///     The IP address.
        /// </value>
        public string IpAddress
        {
            get { return Socket.RemoteEndPoint.ToString(); }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is in transaction.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is in transaction; otherwise, <c>false</c>.
        /// </value>
        public bool IsInTransaction { get; }

        /// <summary>
        ///     Gets the socket.
        /// </summary>
        /// <value>
        ///     The socket.
        /// </value>
        public Socket Socket { get; protected set; }

        /// <summary>
        ///     Gets the stream.
        /// </summary>
        /// <value>
        ///     The stream.
        /// </value>
        public NetworkStream Stream { get; protected set; }

        /// <summary>
        ///     Gets the XTEA key.
        /// </summary>
        /// <value>
        ///     The XTEA key.
        /// </value>
        public Xtea Xtea { get; protected set; }

        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public virtual void Close()
        {
            Stream?.Close();
            Socket?.Close();
        }

        /// <summary>
        ///     Ends the read.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>Whether the read is completed successfully.</returns>
        protected bool EndRead(IAsyncResult result)
        {
            try
            {
                int read = Stream.EndRead(result);
                if (read == 0)
                {
                    // Client disconnected
                    Close();
                    return false;
                }

                int size = BitConverter.ToUInt16(IncomingMessage.Buffer, 0) + 2;
                while (read < size)
                {
                    if (Stream.CanRead)
                        read += Stream.Read(IncomingMessage.Buffer, read, size - read);
                }

                IncomingMessage.Length = size;
                IncomingMessage.Position = 0;

                // TODO: Validate total length
                IncomingMessage.ReadUInt16();

                // TODO: Validate Adler-32 checksum
                IncomingMessage.ReadUInt32();
                return true;
            }
            catch (Exception)
            {
                Close();
                return false;
            }
        }

        /// <summary>
        ///     Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(NetworkMessage message)
        {
            Send(message, true);
        }

        /// <summary>
        ///     Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="useEncryption">if set to <c>true</c> [use encryption].</param>
        /// <exception cref="Exception"></exception>
        public void Send(NetworkMessage message, bool useEncryption)
        {
            if (IsInTransaction)
            {
                if (useEncryption == false)
                    throw new Exception("Cannot send a packet without encryption as part of a transaction.");

                IncomingMessage.AddBytes(message.ReadPacket());
            }
            else
            {
                SendMessage(message, useEncryption);
            }
        }

        /// <summary>
        ///     Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="useEncryption">if set to <c>true</c> [use encryption].</param>
        protected void SendMessage(NetworkMessage message, bool useEncryption)
        {
            if (useEncryption)
            {
                if (!message.PrepareToSend(Xtea))
                    Close();
            }
            else
            {
                if (!message.PrepareToSend())
                    Close();
            }

            try
            {
                Stream.BeginWrite(message.Buffer, 0, message.Length, null, null);
            }
            catch
            {
                Close();
            }
        }
    }
}