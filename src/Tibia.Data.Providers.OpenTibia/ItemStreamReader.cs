using System;
using System.IO;

namespace Tibia.Data.Providers.OpenTibia
{
    public class ItemStreamReader : StreamReaderBase, IItemStreamReader
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.Providers.OpenTibia.ItemStreamReader" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public ItemStreamReader(Stream stream)
            : base(stream)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Reads the metadata.
        /// </summary>
        /// <returns>The metadata.</returns>
        public ItemsMetadata ReadMetadata()
        {
            return new ItemsMetadata
            {
                Version = new Version((int)ReadUInt32(), (int)ReadUInt32(), (int)ReadUInt32()),
                CsdVersion = ReadBytes(ItemsMetadata.CsdVersionSize)
            };
        }
    }
}