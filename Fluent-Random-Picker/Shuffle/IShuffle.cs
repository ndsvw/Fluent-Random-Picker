using System.Collections.Generic;

namespace Fluent_Random_Picker.Shuffle
{
    /// <summary>
    /// An interface for shuffle algorithms.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IShuffle<T>
    {
        /// <summary>
        /// Shuffles the elements.
        /// </summary>
        /// <param name="pElements">The elements to shuffle.</param>
        /// <returns>The same elements in a random order.</returns>
        IEnumerable<T> Shuffle(IEnumerable<T> pElements);

        /// <summary>
        /// Shuffles the elements.
        /// </summary>
        /// <param name="pElements">The elements to shuffle.</param>
        /// <param name="pFirstN">Specifies that only the first n elements will be shuffled.</param>
        /// <returns>The same elements in a random order.</returns>
        IEnumerable<T> Shuffle(IEnumerable<T> pElements, int pFirstN);
    }
}
