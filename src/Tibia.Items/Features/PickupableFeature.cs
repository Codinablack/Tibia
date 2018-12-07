using Tibia.Data;

namespace Tibia.Items.Features
{
    public class PickupableFeature : ItemFeatureBase, IPickupable
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the weight.
        /// </summary>
        /// <value>
        ///     The weight.
        /// </value>
        public double Weight { get; set; }
    }
}