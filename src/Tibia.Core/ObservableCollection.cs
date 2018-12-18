using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tibia.Core
{
    public class ObservableCollection<T> : ICollection<T>
    {
        private readonly ICollection<T> _items;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ObservableCollection{T}" /> class.
        /// </summary>
        public ObservableCollection()
        {
            _items = new HashSet<T>();
        }
        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        ///     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        public void Add(T item)
        {
            AddingItemEventArgs<T> addingItemEventArgs = new AddingItemEventArgs<T>(item);
            AddingItem?.Invoke(this, addingItemEventArgs);
            if (addingItemEventArgs.Cancel)
                return;

            _items.Add(item);

            AddedItemEventArgs<T> addedItemEventArgs = new AddedItemEventArgs<T>(addingItemEventArgs.Item);
            AddedItem?.Invoke(this, addedItemEventArgs);
        }
        /// <summary>
        ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }
        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> is found in the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }
        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an
        ///     <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements
        ///     copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }
        /// <summary>
        ///     Removes the first occurrence of a specific object from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> was successfully removed from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if
        ///     <paramref name="item">item</paramref> is not found in the original
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        public bool Remove(T item)
        {
            return _items.Remove(item);
        }
        /// <summary>
        ///     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        public int Count => _items.Count;
        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        public bool IsReadOnly => _items.IsReadOnly;

        /// <summary>
        ///     Occurs when this instance added an item.
        /// </summary>
        public event EventHandler<AddedItemEventArgs<T>> AddedItem;

        /// <summary>
        ///     Occurs when this instance is adding an item.
        /// </summary>
        public event EventHandler<AddingItemEventArgs<T>> AddingItem;
    }

    public class AddedItemEventArgs<T> : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.AddedItemEventArgs" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public AddedItemEventArgs(T item)
        {
            Item = item;
        }

        /// <summary>
        ///     Gets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        public T Item { get; }
    }

    public class AddingItemEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.AddingItemEventArgs" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public AddingItemEventArgs(T item)
        {
            Item = item;
        }

        /// <summary>
        ///     Gets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        public T Item { get; }
    }
}