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
    internal class PrioritizedLeftShuffle<T>
    {
        private readonly IRandomNumberGenerator m_Rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrioritizedLeftShuffle{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        public PrioritizedLeftShuffle(IRandomNumberGenerator pRng)
        {
            m_Rng = pRng;
        }

        /// <summary>
        /// Shuffles the elements and respects the probabilities in O(n) time.
        /// </summary>
        /// <param name="pPairs">The elements (value and probability) to shuffle.</param>
        /// <param name="pFirstN">Limits how many of the first elements to shuffle.</param>
        /// <returns>The shuffled elements.</returns>
        public IEnumerable<T> Shuffle(IEnumerable<ValuePriorityPair<T>> pPairs, int pFirstN)
        {
            var max = pPairs.Max(v => v.Priority);

            var list = new List<ValuePriorityPair<T>>(pPairs);
            for (int i = 0; i < pFirstN - 1; i++)
            {
                int randomIndex = RouletteWheelSelection(list, i, max);
                Swap(list, i, randomIndex);

                // could be optimized by updating the max sometimes, but it's O(n)..
            }

            return list.Select(p => p.Value);
        }

        private static void Swap<TEelment>(IList<TEelment> pElements, int pIndex1, int pIndex2)
        {
            var tmp = pElements[pIndex1];
            pElements[pIndex1] = pElements[pIndex2];
            pElements[pIndex2] = tmp;
        }

        private int RouletteWheelSelection(IList<ValuePriorityPair<T>> pPairs, int pStartIndex, int pMax)
        {
            while (true)
            {
                var randomDouble = m_Rng.NextDouble();
                var randomIndex = m_Rng.NextInt(pStartIndex, pPairs.Count);
                if (randomDouble <= pPairs[randomIndex].Priority / (double)pMax)
                    return randomIndex;
            }
        }
    }
}
