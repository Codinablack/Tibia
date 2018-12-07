using Tibia.Core;

namespace Tibia.Data
{
    public interface IHouse : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        ///     The owner identifier.
        /// </value>
        int? OwnerId { get; set; }
    }
}