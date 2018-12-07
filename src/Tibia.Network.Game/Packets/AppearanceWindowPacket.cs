using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class AppearanceWindowPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="currentOutfit">The current outfit.</param>
        /// <param name="currentMount">The current mount.</param>
        /// <param name="outfits">The outfits.</param>
        /// <param name="mounts">The mounts.</param>
        public static void Add(NetworkMessage message, IOutfit currentOutfit, IMount currentMount, ICollection<IOutfit> outfits, ICollection<IMount> mounts)
        {
            message.AddPacketType(GamePacketType.AppearanceWindow);
            message.AddAppearance(currentOutfit, currentMount);
            AddOutfits(message, outfits);
            AddMounts(message, mounts);
        }

        /// <summary>
        ///     Adds the outfits.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="outfits">The outfits.</param>
        private static void AddOutfits(NetworkMessage message, ICollection<IOutfit> outfits)
        {
            message.AddByte((byte) outfits.Count);
            foreach (IOutfit skin in outfits)
                message.AddOutfit(skin);
        }

        /// <summary>
        ///     Adds the mounts.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="mounts">The mounts.</param>
        private static void AddMounts(NetworkMessage message, ICollection<IMount> mounts)
        {
            message.AddByte((byte) mounts.Count);
            foreach (IMount mount in mounts)
                message.AddMount(mount);
        }
    }
}