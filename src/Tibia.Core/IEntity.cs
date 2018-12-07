namespace Tibia.Core
{
    public interface IEntity<T>
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        T Id { get; set; }
    }
}