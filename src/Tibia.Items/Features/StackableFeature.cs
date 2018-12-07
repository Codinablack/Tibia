using Tibia.Data;

namespace Tibia.Items.Features
{
    public class StackableFeature : ItemFeatureBase, IStackable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StackableFeature" /> class.
        /// </summary>
        /// <param name="maxStackableCount">The maximum stackable count.</param>
        public StackableFeature(uint maxStackableCount)
        {
            MaxStackableCount = maxStackableCount;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the maximum stackable count.
        /// </summary>
        /// <value>
        ///     The maximum stackable count.
        /// </value>
        public uint MaxStackableCount { get; set; }
    }
}