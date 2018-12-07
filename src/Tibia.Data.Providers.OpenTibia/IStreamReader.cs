namespace Tibia.Data.Providers.OpenTibia
{
    public interface IStreamReader
    {
        /// <summary>
        ///     Peeks the character.
        /// </summary>
        /// <returns>The peeked char.</returns>
        int PeekChar();

        /// <summary>
        ///     Reads the byte.
        /// </summary>
        /// <returns>The byte.</returns>
        byte ReadByte();

        /// <summary>
        ///     Reads the integer.
        /// </summary>
        /// <returns>The integer.</returns>
        int ReadInt32();

        /// <summary>
        ///     Reads the single.
        /// </summary>
        /// <returns>The single.</returns>
        float ReadSingle();

        /// <summary>
        ///     Reads the string.
        /// </summary>
        /// <returns>The string.</returns>
        string ReadString();

        /// <summary>
        ///     Reads the unsigned short integer.
        /// </summary>
        /// <returns>The unsigned short integer.</returns>
        ushort ReadUInt16();

        /// <summary>
        ///     Reads the unsigned integer.
        /// </summary>
        /// <returns>The unsigned integer.</returns>
        uint ReadUInt32();
    }
}