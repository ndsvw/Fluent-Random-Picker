using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Random;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.Shuffle
{
    /// <summary>
    /// A shuffle algorithm that respects weights.
    /// This is a ~ linear run time implementation.
    /// More: https://www.researchgate.net/publication/51962025_Roulette-wheel_selection_via_stochastic_acceptance.
    /// Please not: The performance can be pretty bad if at least one weight is very high and others are very low
    ///             (e.g. [100.000.000, 1, 2, 3]).
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class StochasticAcceptanceBasedWeightedLeftShuffle<T> : IShuffle<ValuePriorityPair<T>>
    {
        private readonly IRandomNumberGenerator _rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="StochasticAcceptanceBasedWeightedLeftShuffle{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        public StochasticAcceptanceBasedWeightedLeftShuffle(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <summary>
        /// Shuffles the elements and respects the probabilities in O(n) time.
        /// </summary>
        /// <param name="elements">The elements (value and probability) to shuffle.</param>
        /// <returns>The shuffled elements.</returns>
        public IEnumerable<ValuePriorityPair<T>> Shuffle(IEnumerable<ValuePriorityPair<T>> elements)
        {
            var elementsList = elements.ToList();
            return Shuffle(elementsList, elementsList.Count);
        }

        /// <summary>
        /// Shuffles the first n elements and respects the probabilities in O(n) time.
        /// </summary>
        /// <param name="elements">The elements (value and probability) to shuffle.</param>
        /// <param name="firstN">Limits how many of the first elements to shuffle.</param>
        /// <returns>The shuffled elements.</returns>
        public IEnumerable<ValuePriorityPair<T>> Shuffle(IEnumerable<ValuePriorityPair<T>> elements, int firstN)
        {
            var max = elements.Max(v => v.Priority);

            var list = elements.ToList();
            var lastIndex = Math.Min(firstN, list.Count) - 1;
            for (int i = 0; i <= lastIndex; i++)
            {
                int randomIndex = RouletteWheelSelection(list, i, max);
                Swap(list, i, randomIndex);

                // could be optimized by updating the max sometimes, but it's O(n)..
            }

            return list;
        }

        private static void Swap<TElement>(IList<TElement> elements, int index1, int index2)
        {
            (elements[index1], elements[index2]) = (elements[index2], elements[index1]);
        }

        private int RouletteWheelSelection(IList<ValuePriorityPair<T>> pairs, int startIndex, int max)
        {
            var castMax = (double)max;
            while (true)
            {
                var randomDouble = _rng.NextDouble();
                var randomIndex = _rng.NextInt(startIndex, pairs.Count);
                if (randomDouble <= pairs[randomIndex].Priority / castMax)
                    return randomIndex;
            }
        }
    }
}
