using Tibia.Map;

namespace Tibia.Data.Providers.OpenTibia
{
    public interface IMapStreamReader
    {
        /// <summary>
        ///     Reads the metadata.
        /// </summary>
        /// <returns>The metadata.</returns>
        MapMetadata ReadMetadata();

        /// <summary>
        ///     Reads the vector2.
        /// </summary>
        /// <returns>The vector2.</returns>
        Vector2 ReadVector2();

        /// <summary>
        ///     Reads the vector3.
        /// </summary>
        /// <returns>The vector3.</returns>
        Vector3 ReadVector3();
    }
}