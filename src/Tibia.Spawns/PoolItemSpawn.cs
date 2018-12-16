using Tibia.Data;

namespace Tibia.Spawns
{
    public class PoolItemSpawn : ItemSpawn, IPoolItemSpawn
    {
        /// <summary>
        ///     Gets or sets the fluid.
        /// </summary>
        /// <value>
        ///     The fluid.
        /// </value>
        public IFluid Fluid { get; set; }
    }
}