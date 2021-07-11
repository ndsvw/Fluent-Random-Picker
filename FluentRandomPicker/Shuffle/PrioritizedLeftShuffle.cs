using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Random;

namespace FluentRandomPicker.Shuffle
{
    /// <summary>
    /// A shuffle algorithm that respects probabilities.
    /// This is a high-performance O(n) implementation.
    /// More: https://www.researchgate.net/publication/51962025_Roulette-wheel_selection_via_stochastic_acceptance.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class PrioritizedLeftShuffle<T> : IShuffle<ValuePriorityPair<T>>
    {
        private readonly IRandomNumberGenerator _rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrioritizedLeftShuffle{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        public PrioritizedLeftShuffle(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <summary>
        /// Shuffles the elements and respects the probabilities in O(n) time.
        /// </summary>
        /// <param name="pairs">The elements (value and probability) to shuffle.</param>
        /// <returns>The shuffled elements.</returns>
        public IEnumerable<ValuePriorityPair<T>> Shuffle(IEnumerable<ValuePriorityPair<T>> pairs)
        {
            return Shuffle(pairs, pairs.Count());
        }

        /// <summary>
        /// Shuffles the first n elements and respects the probabilities in O(n) time.
        /// </summary>
        /// <param name="pairs">The elements (value and probability) to shuffle.</param>
        /// <param name="firstN">Limits how many of the first elements to shuffle.</param>
        /// <returns>The shuffled elements.</returns>
        public IEnumerable<ValuePriorityPair<T>> Shuffle(IEnumerable<ValuePriorityPair<T>> pairs, int firstN)
        {
            var max = pairs.Max(v => v.Priority);

            var list = new List<ValuePriorityPair<T>>(pairs);
            var lastIndex = firstN < pairs.Count() ? firstN - 1 : firstN - 2;
            for (int i = 0; i <= lastIndex; i++)
            {
                int randomIndex = RouletteWheelSelection(list, i, max);
                Swap(list, i, randomIndex);

                // could be optimized by updating the max sometimes, but it's O(n)..
            }

            return list;
        }

        private static void Swap<TEelment>(IList<TEelment> elements, int index1, int index2)
        {
            var tmp = elements[index1];
            elements[index1] = elements[index2];
            elements[index2] = tmp;
        }

        private int RouletteWheelSelection(IList<ValuePriorityPair<T>> pairs, int startIndex, int max)
        {
            while (true)
            {
                var randomDouble = _rng.NextDouble();
                var randomIndex = _rng.NextInt(startIndex, pairs.Count);
                if (randomDouble <= pairs[randomIndex].Priority / (double)max)
                    return randomIndex;
            }
        }
    }
}
