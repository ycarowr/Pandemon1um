﻿using System;
using System.Runtime.CompilerServices;
using Random = UnityEngine.Random;

/// <summary>
///     A faster replacement for List&lt;T&gt;.
/// </summary>
/// <typeparam name="T">The type of item being held in the array.</typeparam>
[Serializable]
public class FastArray<T>
{
    protected T[] array;
    public int count;

    /// <summary>
    ///     Create a new FastArray with an initial capacity.
    /// </summary>
    /// <param name="capacity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FastArray(int capacity = 10)
    {
        array = new T[capacity];
        count = 0;
    }

    /// <summary>
    ///     Converts an array into a FastArray.  This does not make a new copy of the array - the new FastArray will use the
    ///     existing array internally.
    /// </summary>
    /// ///
    /// <param name="fromArray"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FastArray(T[] fromArray)
    {
        array = fromArray;
        count = array.Length;
    }

    public T this[int i]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => array[i];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => array[i] = value;
    }

    /// <summary>
    ///     Adds an item to the list. May need to allocate a larger array if there is not enough space.
    /// </summary>
    /// <param name="item"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(T item, bool allowExpand = true)
    {
        if (count == array.Length && allowExpand) DoubleCapacity();
        array[count] = item;
        count++;
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
        if (count == array.Length && allowExpand) DoubleCapacity();

        count++;
        for (var i = count; i > index; i--) array[i] = array[i - 1];
        array[index] = element;
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
        if (count == array.Length && allowExpand) DoubleCapacity();
        array[count] = array[index];
        array[index] = element;
        count++;
    }


    /// <summary>
    ///     Removes an item from the list and returns it, shifting each later item back by one space.  This is slower for
    ///     longer lists - for faster deletion, consider using RemoveFast().
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Remove(int index)
    {
        var output = array[index];
        for (var i = index + 1; i < array.Length; i++) array[i - 1] = array[i];
        count -= 1;

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
        var output = array[index];
        array[index] = array[count - 1];
        count--;

        return output;
    }

    /// <summary>
    ///     Removes the last item from the array and returns it.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Pop()
    {
        count--;
        return array[count];
    }

    /// <summary>
    ///     Clears the list.  Does not delete contents! References in "unused" slots will remain. To also set all references to
    ///     null, use HardClearLength() or HardClear().
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() => count = 0;

    /// <summary>
    ///     Clears the list and removes the references that it contained (up to the previous "count" value). To clear all
    ///     references in the list, including those outside of the active range, use HardClear().
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HardClearCount()
    {
        for (var i = 0; i < count; i++) array[i] = default;
        count = 0;
    }

    /// <summary>
    ///     Clears the list and removes all references that the array contained, even if they were outside of the active range.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HardClearAll()
    {
        for (var i = 0; i < array.Length; i++) array[i] = default;
        count = 0;
    }

    /// <summary>
    ///     Get a reference to the FastArray's internal array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] GetArray() => array;

    /// <summary>
    ///     Shuffles the FastArray into a random order.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Shuffle()
    {
        int j;
        T tmp;
        for (var i = count - 1; i > 0; i--)
        {
            j = Random.Range(0, i + 1);
            tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }

    /// <summary>
    ///     Returns the first item in the FastArray: this[0]
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T First() => this[0];

    /// <summary>
    ///     Returns the final item in the FastArray: this[count-1]
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Last() => this[count - 1];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void DoubleCapacity()
    {
        var newLength = count * 2;
        if (array.Length < 4) newLength = 8;
        var newArray = new T[newLength];
        array.CopyTo(newArray, 0);
        array = newArray;
    }
}