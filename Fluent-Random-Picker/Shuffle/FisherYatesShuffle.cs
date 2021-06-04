using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker.Random;

namespace Fluent_Random_Picker.Shuffle
{
    /// <summary>
    /// The Fisher-Yates shuffle algorithm.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class FisherYatesShuffle<T> : IShuffle<T>
    {
        private readonly IRandomNumberGenerator m_Rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="FisherYatesShuffle{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        public FisherYatesShuffle(IRandomNumberGenerator pRng)
        {
            m_Rng = pRng;
        }

        /// <inheritdoc/>
        public IEnumerable<T> Shuffle(IEnumerable<T> pElements)
        {
            return Shuffle(pElements, pElements.Count());
        }

        /// <inheritdoc/>
        public IEnumerable<T> Shuffle(IEnumerable<T> pElements, int pFirstN)
        {
            var list = new List<T>(pElements);
            for (int i = 0; i < pFirstN - 1; i++)
            {
                int k = m_Rng.NextInt(i, list.Count);
                T tmp = list[i];
                list[i] = list[k];
                list[k] = tmp;
            }

            return list;
        }
    }
}