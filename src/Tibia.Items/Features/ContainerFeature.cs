using Tibia.Data;

namespace Tibia.Items.Features
{
    public class ContainerFeature : PickupableFeature, IContainer
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the volume.
        /// </summary>
        /// <value>
        ///     The volume.
        /// </value>
        public byte Volume { get; set; }
    }
}