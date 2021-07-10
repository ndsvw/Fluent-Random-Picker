using System.Collections.Generic;

namespace FluentRandomPicker.Interfaces
{
    /// <summary>
    /// An interface to specify that the Pick methods can be called.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface ICanPick<out T>
    {
        /// <summary>
        /// Picks n values.
        /// </summary>
        /// <param name="n">The number of values to pick. Values can be picked multiple times.</param>
        /// <returns>The picked values.</returns>
        IEnumerable<T> Pick(int n);

        /// <summary>
        /// Picks one value.
        /// </summary>
        /// <returns>The picked value.</returns>
        T PickOne();

        /// <summary>
        /// Picks n distinct values.
        /// </summary>
        /// <param name="n">The number of distinct values to pick.</param>
        /// <returns>The picked values.</returns>
        IEnumerable<T> PickDistinct(int n);
    }
}
