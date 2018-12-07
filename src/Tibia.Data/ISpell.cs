using Tibia.Core;

namespace Tibia.Data
{
    public interface ISpell : ICombat, IEntity<byte>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }
    }
}