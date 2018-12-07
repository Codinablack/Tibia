using Tibia.Data.Services;

namespace Tibia.Data
{
    public interface ICreatureSpawnService : IService
    {
        /// <summary>
        ///     Gets the creature spawn by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The creature spawn.</returns>
        ICreatureSpawn GetCreatureSpawnById(uint id);
    }
}