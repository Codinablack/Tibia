using System.Collections.Generic;

namespace Tibia.Data.Storage
{
    public interface IRepository<TEntity, in TIdentifier>
    {
        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns>The collection.</returns>
        // TODO: GetAll should be IQueryable
        ICollection<TEntity> GetAll();

        /// <summary>
        ///     Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if the item is created; otherwise, <c>false</c>.</returns>
        bool Create(TEntity item);

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if the item is deleted; otherwise, <c>false</c>.</returns>
        bool Delete(TIdentifier id);

        /// <summary>
        ///     Gets the item with a specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The item with the specified identifier.</returns>
        TEntity Get(TIdentifier id);

        /// <summary>
        ///     Saves the changes.
        /// </summary>
        /// <returns><c>true</c> if the changes are saved; otherwise, <c>false</c>.</returns>
        bool SaveChanges();
    }
}