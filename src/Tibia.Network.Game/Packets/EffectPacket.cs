using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class EffectPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="position">The position.</param>
        /// <param name="effect">The effect.</param>
        public static void Add(NetworkMessage message, IVector3 position, Effect effect)
        {
            message.AddPacketType(GamePacketType.Effect);
            message.AddVector3(position);
            message.AddEffect(effect);
        }
    }
}