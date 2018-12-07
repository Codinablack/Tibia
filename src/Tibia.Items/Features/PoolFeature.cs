using Tibia.Data;

namespace Tibia.Items.Features
{
    public class PoolFeature : ItemFeatureBase, IPool
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the fluid.
        /// </summary>
        /// <value>
        ///     The fluid.
        /// </value>
        public IFluid Fluid { get; set; }
    }
}