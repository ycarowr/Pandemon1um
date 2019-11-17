using System;
using System.Runtime.CompilerServices;
using Random = UnityEngine.Random;

namespace Tools.FastStructures
{
    /// <summary>
    ///     A faster replacement for List&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">The type of item being held in the array.</typeparam>
    [Serializable]
    public class FastList<T> where T : class
    {
        protected T[] Array;

        /// <summary>
        ///     Create a new FastList with an initial capacity.
        /// </summary>
        /// <param name="capacity"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FastList(int capacity = 16)
        {
            Array = new T[capacity];
            Length = 0;
        }

        /// <summary>
        ///     Converts an array into a FastList.  This does not make a new copy of the array - the new FastList will use the
        ///     existing array internally.
        /// </summary>
        /// ///
        /// <param name="fromArray"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FastList(T[] fromArray)
        {
            Array = fromArray;
            Length = Array.Length;
        }

        public int Length { get; set; }

        public T this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Array[i];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Array[i] = value;
        }

        /// <summary>
        ///     Whether the item is present in the array or not.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(T item)
        {
            for (var i = 0; i < Length; i++)
                if (item == Array[i])
                    return true;
            return false;
        }

        /// <summary>
        ///     Adds an item to the list. May need to allocate a larger array if there is not enough space.
        /// </summary>
        /// <param name="item"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item, bool allowExpand = true)
        {
            if (Length == Array.Length && allowExpand) DoubleCapacity();

            Array[Length] = item;
            Length++;
        }

        /// <summary>
        ///     Insert an element at a specified index, shifting every later item forward by one space. May need to allocate a
        ///     larger array if there is not enough space.  This is slower for longer lists - for faster insertion, consider using
        ///     FastInsert().
        /// </summary>
        /// <param name="element"></param>
        /// <param name="index"></param>
        public void Insert(T element, int index, bool allowExpand = true)
        {
            if (Length == Array.Length && allowExpand) DoubleCapacity();

            Length++;
            for (var i = Length; i > index; i--) Array[i] = Array[i - 1];

            Array[index] = element;
        }

        /// <summary>
        ///     Insert an element at the specified index, shifting the original value to the end of the list. May need to allocate
        ///     a larger array if there is not enough space.  Faster than Insert(), but does not preserve your array's order.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="index"></param>
        /// <param name="allowExpand"></param>
        public void InsertFast(T element, int index, bool allowExpand = true)
        {
            if (Length == Array.Length && allowExpand) DoubleCapacity();

            Array[Length] = Array[index];
            Array[index] = element;
            Length++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(T item)
        {
            if (!Has(item))
                return false;
            var index = IndexOf(item);
            RemoveFast(index);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RemoveFast(T item)
        {
            if (!Has(item))
                return false;
            var index = IndexOf(item);
            RemoveFast(index);
            return true;
        }

        /// <summary>
        ///     Removes an item from the list and returns it, shifting each later item back by one space.  This is slower for
        ///     longer lists - for faster deletion, consider using RemoveFast().
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Remove(int index)
        {
            var output = Array[index];
            for (var i = index + 1; i < Array.Length; i++) Array[i - 1] = Array[i];

            Length -= 1;

            return output;
        }

        /// <summary>
        ///     Removes an item from the list and returns it, swapping the previous final-item into its slot. Faster than Remove(),
        ///     but does not preserve your array's order.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T RemoveFast(int index)
        {
            var output = Array[index];
            Array[index] = Array[Length - 1];
            Length--;

            return output;
        }

        /// <summary> Get an element from a specific position.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get(int index) => Array[index];

        /// <summary> Get an item randomly from the collection.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T RandomItem()
        {
            var rdn = Random.Range(0, Length);
            return Array[rdn];
        }

        /// <summary> Get and Remove an item randomly from the collection. Uses Remove.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T RemoveRandomItem()
        {
            var rdnIndex = Random.Range(0, Length);
            return Remove(rdnIndex);
        }

        /// <summary> Get and Remove an item randomly from the collection. Uses Remove Fast.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T RemoveRandomItemFast()
        {
            var rdnIndex = Random.Range(0, Length);
            return RemoveFast(rdnIndex);
        }

        /// <summary>
        ///     Removes the last item from the array and returns it.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Pop()
        {
            Length--;
            return Array[Length];
        }

        /// <summary>
        ///     Clears the list.  Does not delete contents! References in "unused" slots will remain. To also set all references to
        ///     null, use HardClearLength() or HardClear().
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear() => Length = 0;

        /// <summary>
        ///     Clears the list and removes the references that it contained (up to the previous "count" value). To clear all
        ///     references in the list, including those outside of the active range, use HardClear().
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HardClearCount()
        {
            for (var i = 0; i < Length; i++) Array[i] = default;

            Length = 0;
        }

        /// <summary>
        ///     Clears the list and removes all references that the array contained, even if they were outside of the active range.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HardClearAll()
        {
            for (var i = 0; i < Array.Length; i++) Array[i] = default;

            Length = 0;
        }

        /// <summary>
        ///     Get a reference to the FastList's internal array.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] GetArray() => Array;

        /// <summary>
        ///     Shuffles the FastList into a random order.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Shuffle()
        {
            int j;
            T tmp;
            for (var i = Length - 1; i > 0; i--)
            {
                j = Random.Range(0, i + 1);
                tmp = Array[i];
                Array[i] = Array[j];
                Array[j] = tmp;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item)
        {
            for (var i = 0; i < Length; i++)
                if (Array[i] == item)
                    return i;
            return -1;
        }

        /// <summary>
        ///     Returns the first item in the FastList: this[0]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T First() => this[0];

        /// <summary>
        ///     Returns the final item in the FastList: this[count-1]
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Last() => this[Length - 1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void DoubleCapacity()
        {
            var newLength = Length * 2;
            if (Array.Length < 4) newLength = 8;

            var newArray = new T[newLength];
            Array.CopyTo(newArray, 0);
            Array = newArray;
        }
    }
}