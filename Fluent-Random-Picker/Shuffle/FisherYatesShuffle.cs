using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluent_Random_Picker.Shuffle
{
    /// <summary>
    /// The Fisher-Yates shuffle algorithm.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    public class FisherYatesShuffle<T> : IShuffle<T>
    {
        /// <inheritdoc/>
        public IEnumerable<T> Shuffle(IEnumerable<T> pElements)
        {
            return Shuffle(pElements, pElements.Count());
        }

        /// <inheritdoc/>
        public IEnumerable<T> Shuffle(IEnumerable<T> pElements, int pFirstN)
        {
            var rng = new Random();
            var list = new List<T>(pElements);
            for (int i = 0; i < pFirstN - 2; i++)
            {
                int k = rng.Next(i, list.Count);
                T tmp = list[i];
                list[i] = list[k];
                list[k] = tmp;
            }

            return list;
        }
    }
}