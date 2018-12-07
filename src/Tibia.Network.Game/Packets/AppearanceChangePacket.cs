using Tibia.Data;
using Tibia.Mounts;
using Tibia.Outfits;

namespace Tibia.Network.Game.Packets
{
    public class AppearanceChangePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the outfit.
        /// </summary>
        /// <value>
        ///     The outfit.
        /// </value>
        public IOutfit Outfit { get; set; }

        /// <summary>
        ///     Gets or sets the mount.
        /// </summary>
        /// <value>
        ///     The mount.
        /// </value>
        public IMount Mount { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="outfitService">The outfit service.</param>
        /// <param name="mountService">The mount service.</param>
        /// <returns>Th packet.</returns>
        public static AppearanceChangePacket Parse(NetworkMessage message, OutfitService outfitService, MountService mountService)
        {
            AppearanceChangePacket packet = new AppearanceChangePacket();

            packet.Outfit = outfitService.GetOutfitBySpriteId(message.ReadUInt16());
            packet.Outfit.Head = message.ReadByte();
            packet.Outfit.Body = message.ReadByte();
            packet.Outfit.Legs = message.ReadByte();
            packet.Outfit.Feet = message.ReadByte();
            packet.Outfit.Addons = message.ReadByte();
            packet.Mount = mountService.GetMountBySpriteId(message.ReadUInt16());

            return packet;
        }
    }
}