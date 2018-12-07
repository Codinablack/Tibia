using System;

namespace Tibia.Data
{
    public interface IVector3 : IVector2, IEquatable<IVector3>
    {
        /// <summary>
        ///     Gets or sets the z.
        /// </summary>
        /// <value>
        ///     The z.
        /// </value>
        int Z { get; }

        /// <summary>
        ///     Gets the direction.
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <returns>The direction.</returns>
        Direction GetDirection(IVector3 targetPosition);

        /// <summary>
        ///     Determines whether [is in range] [the specified target position].
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="range">The range.</param>
        /// <returns>
        ///     <c>true</c> if [is in range] [the specified target position]; otherwise, <c>false</c>.
        /// </returns>
        bool IsInRange(IVector3 targetPosition, IVector3 range);

        /// <summary>
        ///     Determines whether [is next to] [the specified target position].
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="range">The range.</param>
        /// <returns>
        ///     <c>true</c> if [is next to] [the specified target position]; otherwise, <c>false</c>.
        /// </returns>
        bool IsNextTo(IVector3 targetPosition, IVector3 range);

        /// <summary>
        ///     Determines whether [is in same floor] [the specified target position].
        /// </summary>
        /// <param name="targetPosition">The target position.</param>
        /// <returns>
        ///     <c>true</c> if [is in same floor] [the specified target position]; otherwise, <c>false</c>.
        /// </returns>
        bool IsInSameFloor(IVector3 targetPosition);

        /// <summary>
        ///     Gets the offset of the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="step">The step.</param>
        /// <returns>The offset.</returns>
        IVector3 Offset(Direction direction, int step = 1);
    }
}