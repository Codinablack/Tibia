using System.IO;
using Tibia.Map;

namespace Tibia.Data.Providers.OpenTibia
{
    public class MapStreamReader : StreamReaderBase, IMapStreamReader
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.Providers.OpenTibia.MapStreamReader" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public MapStreamReader(Stream stream)
            : base(stream)
        {
        }
        /// <summary>
        ///     Reads the vector3.
        /// </summary>
        /// <returns>The vector3.</returns>
        public Vector3 ReadVector3()
        {
            return new Vector3(ReadUInt16(), ReadUInt16(), ReadByte());
        }
        /// <summary>
        ///     Reads the metadata.
        /// </summary>
        /// <returns>The metadata.</returns>
        public MapMetadata ReadMetadata()
        {
            return new MapMetadata
            {
                Version = ReadUInt32(),
                Width = ReadUInt16(),
                Height = ReadUInt16(),
                MaxItemSchemaVersion = ReadUInt32(),
                MinItemSchemaVersion = ReadUInt32()
            };
        }
        /// <summary>
        ///     Reads the vector2.
        /// </summary>
        /// <returns>The vector2.</returns>
        public Vector2 ReadVector2()
        {
            return new Vector2(ReadByte(), ReadByte());
        }
    }
}