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
        void Shuffle(T[] elements);

        /// <summary>
        /// Shuffles the first n elements.
        /// </summary>
        /// <param name="elements">The elements to shuffle.</param>
        /// <param name="firstN">Specifies that only the first n elements will be shuffled.</param>
        void Shuffle(T[] elements, int firstN);
    }
}
