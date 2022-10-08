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
                .OrderByDescending(k => Math.Pow(GenerateRandomDoubleBetween0ExclusiveAnd1Exclusive(), 1.0 / elementsList[k].Priority))
                .Take(firstN)
                .Select(i => elementsList[i]);
        }

        private double GenerateRandomDoubleBetween0ExclusiveAnd1Exclusive()
        {
            // This conditional recursion is required to make sure, 0 is not returned.
            // With random double = exactly 0, the priorities would not matter at all.
            // Chances are low (like 1/(2^53)), but it's not impossible:
            // https://stackoverflow.com/questions/73916767/what-are-the-chances-of-random-nextdouble-being-exactly-0
            var randomDouble = _rng.NextDouble();
            if (randomDouble != 0) // comparison without delta is intended
                return randomDouble;
            return GenerateRandomDoubleBetween0ExclusiveAnd1Exclusive();
        }
    }
}
