using System.IO;

namespace Tibia.Data.Providers.OpenTibia
{
    public class MapFileReader : FileReaderBase<MapStreamReader>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Reads the property.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>Whether the property is read successfully.</returns>
        public override bool ReadProperty(Node node, out MapStreamReader reader)
        {
            byte[] buffer = ReadBuffer(node, out long size);
            if (buffer == null)
            {
                reader = null;
                return false;
            }

            reader = new MapStreamReader(new MemoryStream(buffer, 0, (int) size));
            return true;
        }
    }
}