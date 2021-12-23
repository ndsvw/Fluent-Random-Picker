using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Random;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.Shuffle
{
    /// <summary>
    /// A shuffle algorithm that respects weights.
    /// This is an O(n*log(n)) implementation.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class SortingBasedWeightedLeftShuffle<T> : IShuffle<ValuePriorityPair<T>>
    {
        private readonly IRandomNumberGenerator _rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortingBasedWeightedLeftShuffle{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        public SortingBasedWeightedLeftShuffle(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <summary>
        /// Shuffles the elements and respects the probabilities in O(n*log(n)) time.
        /// </summary>
        /// <param name="elements">The elements (value and probability) to shuffle.</param>
        /// <returns>The shuffled elements.</returns>
        public IEnumerable<ValuePriorityPair<T>> Shuffle(IEnumerable<ValuePriorityPair<T>> elements)
        {
            var elementsList = elements.ToList();
            return Shuffle(elementsList, elementsList.Count);
        }

        /// <summary>
        /// Shuffles the first n elements and respects the probabilities in O(n*log(n)) time.
        /// </summary>
        /// <param name="elements">The elements (value and probability) to shuffle.</param>
        /// <param name="firstN">Limits how many of the first elements to shuffle.</param>
        /// <returns>The shuffled elements.</returns>
        public IEnumerable<ValuePriorityPair<T>> Shuffle(IEnumerable<ValuePriorityPair<T>> elements, int firstN)
        {
            var elementsList = elements.ToList();
            var n = elementsList.Count;

            return Enumerable.Range(0, n)
                .OrderByDescending(k => Math.Pow(_rng.NextDouble(), 1.0 / elementsList[k].Priority))
                .Take(firstN)
                .Select(i => elementsList[i]);
        }
    }
}
