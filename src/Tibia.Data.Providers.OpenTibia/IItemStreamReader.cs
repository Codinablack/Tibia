namespace Tibia.Data.Providers.OpenTibia
{
    public interface IItemStreamReader
    {
        /// <summary>
        ///     Reads the metadata.
        /// </summary>
        /// <returns>The metadata.</returns>
        ItemsMetadata ReadMetadata();
    }
}