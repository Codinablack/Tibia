using System;
using Tibia.Data;

namespace Tibia.Map
{
    public static class AutoWalkDirectionExtensions
    {
        /// <summary>
        ///     Converts a specified auto-walk direction to its equivalent direction.
        /// </summary>
        /// <param name="autoWalkDirection">The auto-walk direction.</param>
        /// <returns>The direction.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">autoWalkDirection - Invalid AutoWalkDirection value</exception>
        public static Direction ToDirection(this AutoWalkDirection autoWalkDirection)
        {
            switch (autoWalkDirection)
            {
                case AutoWalkDirection.North:
                    return Direction.North;
                case AutoWalkDirection.NorthEast:
                    return Direction.NorthEast;
                case AutoWalkDirection.East:
                    return Direction.East;
                case AutoWalkDirection.SouthEast:
                    return Direction.SouthEast;
                case AutoWalkDirection.South:
                    return Direction.South;
                case AutoWalkDirection.SouthWest:
                    return Direction.SouthWest;
                case AutoWalkDirection.West:
                    return Direction.West;
                case AutoWalkDirection.NorthWest:
                    return Direction.NorthWest;
                case AutoWalkDirection.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(autoWalkDirection), autoWalkDirection, "Invalid AutoWalkDirection value");
            }

            return Direction.North;
        }
    }
}