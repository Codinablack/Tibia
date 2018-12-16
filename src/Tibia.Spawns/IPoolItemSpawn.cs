using Tibia.Data;

namespace Tibia.Spawns
{
    public interface IPoolItemSpawn
    {
        /// <summary>
        ///     Gets or sets the fluid.
        /// </summary>
        /// <value>
        ///     The fluid.
        /// </value>
        IFluid Fluid { get; set; }
    }
}