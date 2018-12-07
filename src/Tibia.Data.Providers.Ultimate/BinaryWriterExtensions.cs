namespace Tibia.Data.Providers.Ultimate
{
    public static class BinaryWriterExtensions
    {
        /// <summary>
        ///     Writes the vector3.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public static void WriteVector3(this BinaryWriter writer, IVector3 value)
        {
            writer.WriteUInt16((ushort) value.X);
            writer.WriteUInt16((ushort) value.Y);
            writer.WriteByte((byte) value.Z);
        }

        /// <summary>
        ///     Writes the vector2.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public static void WriteVector2(this BinaryWriter writer, IVector2 value)
        {
            writer.WriteUInt16((ushort) value.X);
            writer.WriteUInt16((ushort) value.Y);
        }
    }
}