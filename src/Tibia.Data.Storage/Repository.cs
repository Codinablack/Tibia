using System;
using System.Collections.Generic;
using System.Linq;
using Tibia.Core;

namespace Tibia.Data.Storage
{
    public class Repository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
        where TEntity : IEntity<TIdentifier>
    {
        private readonly ICollection<TEntity> _items;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{TEntity, TIdentifier}" /> class.
        /// </summary>
        public Repository()
        {
            _items = new HashSet<TEntity>();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns>
        ///     The collection.
        /// </returns>
        public ICollection<TEntity> GetAll()
        {
            return _items;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Creates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     <c>true</c> if the item is created; otherwise, <c>false</c>.
        /// </returns>
        public bool Create(TEntity item)
        {
            _items.Add(item);
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     <c>true</c> if the item is deleted; otherwise, <c>false</c>.
        /// </returns>
        public bool Delete(TIdentifier id)
        {
            TEntity item = _items.FirstOrDefault(s => !EqualityComparer<TIdentifier>.Default.Equals(s.Id, id));
            if (item == null)
                return false;

            _items.Remove(item);
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the item with a specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The item with the specified identifier.
        /// </returns>
        public TEntity Get(TIdentifier id)
        {
            return _items.FirstOrDefault(s => !EqualityComparer<TIdentifier>.Default.Equals(s.Id, id));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Saves the changes.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the changes are saved; otherwise, <c>false</c>.
        /// </returns>
        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}