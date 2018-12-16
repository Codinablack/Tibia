using System.Collections.Generic;
using System.Linq;
using Tibia.Data;

namespace Tibia.Map
{
    public static class TileExtensions
    {
        /// <summary>
        ///     Determines whether this instance is walkable.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <returns>
        ///     <c>true</c> if the specified tile is walkable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWalkable(this ITile tile)
        {
            // TODO: This implementation is not efficient and will be replaced with simple properties in IItem class
            return tile.Ground != null && !tile.Ground.Item.IsSolidBlock;
        }

        /// <summary>
        ///     Gets the items before medium priority.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <returns>The items before medium priority.</returns>
        public static IEnumerable<IItemSpawn> GetItemsBeforeMediumPriority(this ITile tile)
        {
            return tile.Items.Where(s => s.Item.RenderPriority < RenderPriority.Medium);
        }

        /// <summary>
        ///     Gets the items after medium priority.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <returns>The items after medium priority.</returns>
        public static IEnumerable<IItemSpawn> GetItemsAfterMediumPriority(this ITile tile)
        {
            return tile.Items.Where(s => s.Item.RenderPriority > RenderPriority.Medium);
        }

        /// <summary>
        ///     Gets the position from the floor change.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="tileService">The tile service.</param>
        /// <param name="floorChangeDirection">The floor change direction.</param>
        /// <returns>The position.</returns>
        public static IVector3 GetPositionFromFloorChange(this ITile tile, TileService tileService, FloorChangeDirection floorChangeDirection)
        {
            int x = tile.Position.X;
            int y = tile.Position.Y;
            int z = tile.Position.Z;
            switch (floorChangeDirection)
            {
                case FloorChangeDirection.North:
                    z -= 1;
                    y -= 1;
                    break;
                case FloorChangeDirection.South:
                    z -= 1;
                    y += 1;
                    break;
                case FloorChangeDirection.East:
                    z -= 1;
                    x += 1;
                    break;
                case FloorChangeDirection.West:
                    z -= 1;
                    x -= 1;
                    break;
                case FloorChangeDirection.Up:
                    z -= 1;
                    y += 1;
                    break;
                case FloorChangeDirection.Down:
                    z += 1;
                    ITile targetTile = tileService.GetTileByPosition(new Vector3(x, y, z));
                    switch (targetTile.FloorChangeDirection)
                    {
                        case FloorChangeDirection.North:
                            y += 1;
                            break;
                        case FloorChangeDirection.South:
                            y -= 1;
                            break;
                        case FloorChangeDirection.East:
                            x -= 1;
                            break;
                        case FloorChangeDirection.West:
                            x += 1;
                            break;
                    }
                    break;
            }

            return new Vector3(x, y, z);
        }
    }
}