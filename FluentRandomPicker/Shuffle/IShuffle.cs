using System.Collections.Generic;

namespace FluentRandomPicker.Shuffle
{
    /// <summary>
    /// An interface for shuffle algorithms.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    internal interface IShuffle<T>
    {
        /// <summary>
        /// Shuffles the elements.
        /// </summary>
        /// <param name="elements">The elements to shuffle.</param>
        /// <returns>The same elements in a random order.</returns>
        IEnumerable<T> Shuffle(IEnumerable<T> elements);

        /// <summary>
        /// Shuffles the first n elements.
        /// </summary>
        /// <param name="elements">The elements to shuffle.</param>
        /// <param name="firstN">Specifies that only the first n elements will be shuffled.</param>
        /// <returns>The same elements, but the first n are in a random order.</returns>
        IEnumerable<T> Shuffle(IEnumerable<T> elements, int firstN);
    }
}
