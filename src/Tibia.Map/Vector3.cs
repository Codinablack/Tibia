using System;
using Tibia.Data;

namespace Tibia.Map
{
    public class Vector3 : IVector3
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Vector3" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        /// <value>
        ///     The x.
        /// </value>
        public int X { get; }
        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        /// <value>
        ///     The y.
        /// </value>
        public int Y { get; }
        /// <summary>
        ///     Gets or sets the z.
        /// </summary>
        /// <value>
        ///     The z.
        /// </value>
        public int Z { get; }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public Vector3Type Type
        {
            get
            {
                if (X == 65535)
                {
                    if ((Y & 0x40) != 0)
                        return Vector3Type.Container;

                    return Vector3Type.Slot;
                }

                return Vector3Type.Ground;
            }
        }
        /// <summary>
        ///     Gets the direction.
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <returns>
        ///     The direction.
        /// </returns>
        public Direction GetDirection(IVector3 targetPosition)
        {
            return GetDirection(this, targetPosition);
        }
        /// <summary>
        ///     Determines whether [is in range] [the specified target position].
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="range">The range.</param>
        /// <returns>
        ///     <c>true</c> if [is in range] [the specified target position]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRange(IVector3 targetPosition, IVector3 range)
        {
            return IsInRange(this, targetPosition, range);
        }
        /// <summary>
        ///     Determines whether [is next to] [the specified target position].
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="range">The range.</param>
        /// <returns>
        ///     <c>true</c> if [is next to] [the specified target position]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNextTo(IVector3 targetPosition, IVector3 range)
        {
            return IsInSameFloor(this, targetPosition) && IsInRange(this, targetPosition, range);
        }
        /// <summary>
        ///     Determines whether [is in same floor] [the specified target position].
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <returns>
        ///     <c>true</c> if [is in same floor] [the specified target position]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInSameFloor(IVector3 targetPosition)
        {
            return IsInSameFloor(this, targetPosition);
        }
        /// <summary>
        ///     Gets the offset of the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="step">The step.</param>
        /// <returns>
        ///     The offset.
        /// </returns>
        public IVector3 Offset(Direction direction, int step = 1)
        {
            int x = X;
            int y = Y;
            int z = Z;

            switch (direction)
            {
                case Direction.North:
                    y -= step;
                    break;
                case Direction.South:
                    y += step;
                    break;
                case Direction.West:
                    x -= step;
                    break;
                case Direction.East:
                    x += step;
                    break;
                case Direction.NorthWest:
                    x -= step;
                    y -= step;
                    break;
                case Direction.SouthWest:
                    x -= step;
                    y += step;
                    break;
                case Direction.NorthEast:
                    x += step;
                    y -= step;
                    break;
                case Direction.SouthEast:
                    x += step;
                    y += step;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return new Vector3(x, y, z);
        }
        /// <summary>
        ///     Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="T:Tibia.Map.Vector3" /> is equal to this instance; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public bool Equals(IVector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            if (Equals(left, null) && Equals(right, null))
                return true;

            if (Equals(left, null) || Equals(right, null))
                return false;

            return left.Equals(right);

        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     Determines whether [is in same floor] [the specified source position].
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <returns>
        ///     <c>true</c> if [is in same floor] [the specified source position]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsInSameFloor(IVector3 sourcePosition, IVector3 targetPosition)
        {
            return sourcePosition.Z == targetPosition.Z;
        }

        /// <summary>
        ///     Gets the direction.
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <returns>The direction.</returns>
        private static Direction GetDirection(IVector3 sourcePosition, IVector3 targetPosition)
        {
            int dx = sourcePosition.X - targetPosition.X;
            int dy = sourcePosition.Y - targetPosition.Y;

            if (dx == 0) return dy < 0 ? Direction.South : Direction.North;
            if (dx < 0)
            {
                if (dy == 0) return Direction.East;
                return dy < 0 ? Direction.SouthEast : Direction.NorthEast;
            }
            if (dy == 0) return Direction.West;
            return dy < 0 ? Direction.SouthWest : Direction.NorthWest;
        }

        /// <summary>
        ///     Determines whether [is in range] [the specified source position].
        /// </summary>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="range">The range.</param>
        /// <returns>
        ///     <c>true</c> if [is in range] [the specified source position]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsInRange(IVector3 sourcePosition, IVector3 targetPosition, IVector3 range)
        {
            int dx = Math.Abs(sourcePosition.X - targetPosition.X);
            if (dx > range.X) return false;

            int dy = Math.Abs(sourcePosition.Y - targetPosition.Y);
            if (dy > range.Y) return false;

            int dz = Math.Abs(sourcePosition.Z - targetPosition.Z);
            return dz <= range.Z;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vector3) obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Z;
                return hashCode;
            }
        }
    }
}