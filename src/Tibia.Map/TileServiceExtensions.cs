using System.Collections.Generic;
using System.Linq;
using Tibia.Data;

namespace Tibia.Map
{
    public static class TileServiceExtensions
    {
        /// <summary>
        ///     Gets the items before medium priority.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="tile">The tile.</param>
        /// <returns>The items before medium priority.</returns>
        public static IEnumerable<IItemSpawn> GetItemsBeforeMediumPriority(this TileService service, ITile tile)
        {
            return service.Items(tile.Position).Where(s => s.Item.RenderPriority < RenderPriority.Medium);
        }

        /// <summary>
        ///     Gets the items after medium priority.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="tile">The tile.</param>
        /// <returns>The items after medium priority.</returns>
        public static IEnumerable<IItemSpawn> GetItemsAfterMediumPriority(this TileService service, ITile tile)
        {
            return service.Items(tile.Position).Where(s => s.Item.RenderPriority > RenderPriority.Medium);
        }
    }
}