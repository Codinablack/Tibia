using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Network.Login.Packets
{
    internal sealed class CharacterListPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="worlds">The worlds.</param>
        /// <param name="characters">The characters.</param>
        public static void Add(NetworkMessage message, ICollection<World> worlds, ICollection<ICharacterSpawn> characters)
        {
            message.AddPacketType(LoginPacketType.CharacterList);
            AddWorlds(message, worlds);
            AddCharacters(message, characters);
        }

        /// <summary>
        ///     Adds the worlds.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="worlds">The worlds.</param>
        private static void AddWorlds(NetworkMessage message, ICollection<World> worlds)
        {
            message.AddByte((byte) worlds.Count);
            foreach (World world in worlds)
            {
                message.AddByte(world.Id);
                message.AddString(world.Name);
                message.AddString(world.IpAddress);
                message.AddUInt16((ushort) world.Port);
                message.AddBoolean(world.IsPreview);
            }
        }

        /// <summary>
        ///     Adds the characters.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characters">The characters.</param>
        private static void AddCharacters(NetworkMessage message, ICollection<ICharacterSpawn> characters)
        {
            message.AddByte((byte) characters.Count);
            foreach (ICharacterSpawn character in characters)
            {
                message.AddByte(character.WorldId);
                message.AddString(character.Character.Name);
            }
        }
    }
}