using System;

namespace Tibia.Data
{
    public static class Vector3Extensions
    {
        /// <summary>
        ///     Converts the vector to its <see cref="SlotType" /> equivalent.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The <see cref="SlotType" /> equivalent.</returns>
        public static SlotType ToSlotType(this IVector3 vector)
        {
            return (SlotType) Convert.ToByte(vector.Y);
        }
    }
}