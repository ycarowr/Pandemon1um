namespace Tools.FastStructures
{
    public static class ArrayExtensions
    {
	    /// <summary>
	    ///     Converts an array into a FastList.  This does not
	    ///     make a new copy of the array - the new FastList
	    ///     will use the existing array internally.
	    /// </summary>
	    /// ///
	    /// <param name="fromArray"></param>
	    public static FastList<T> ToFastList<T>(this T[] array) where T : class => new FastList<T>(array);
    }
}