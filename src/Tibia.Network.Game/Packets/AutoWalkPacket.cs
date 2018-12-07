using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Map;

namespace Tibia.Network.Game.Packets
{
    public class AutoWalkPacket : IClientPacket
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AutoWalkPacket" /> class.
        /// </summary>
        public AutoWalkPacket()
        {
            Directions = new Queue<Direction>();
        }

        /// <summary>
        ///     Gets or sets the directions count.
        /// </summary>
        /// <value>
        ///     The directions count.
        /// </value>
        public byte DirectionsCount { get; set; }

        /// <summary>
        ///     Gets or sets the directions.
        /// </summary>
        /// <value>
        ///     The directions.
        /// </value>
        public Queue<Direction> Directions { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static AutoWalkPacket Parse(NetworkMessage message)
        {
            AutoWalkPacket packet = new AutoWalkPacket();

            packet.DirectionsCount = message.ReadByte();
            if (packet.DirectionsCount <= 0)
                return packet;

            for (int index = 0; index < packet.DirectionsCount; index++)
            {
                AutoWalkDirection autoWalkDirection = (AutoWalkDirection) message.ReadByte();
                packet.Directions.Enqueue(autoWalkDirection.ToDirection());
            }

            packet.Directions = new Queue<Direction>(packet.Directions.Reverse());
            return packet;
        }
    }
}