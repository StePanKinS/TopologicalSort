namespace TpSort
{
    /// <summary>
    /// Represents a pair of objects with a larger and smaller element
    /// </summary>
    /// <typeparam name="T">Objects type</typeparam>
    public class Pair<T>
    {
        public Pair(T smaller, T larger)
        {
            Smaller = smaller;
            Larger = larger;
        }
        public T Smaller { get; set; }
        public T Larger { get; set; }
    }
}
