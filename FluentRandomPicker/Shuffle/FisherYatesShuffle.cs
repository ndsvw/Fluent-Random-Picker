using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Random;

namespace FluentRandomPicker.Shuffle
{
    /// <summary>
    /// The Fisher-Yates shuffle algorithm.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class FisherYatesShuffle<T> : IShuffle<T>
    {
        private readonly IRandomNumberGenerator _rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="FisherYatesShuffle{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        public FisherYatesShuffle(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <inheritdoc/>
        public IEnumerable<T> Shuffle(IEnumerable<T> elements)
        {
            return Shuffle(elements, elements.Count());
        }

        /// <inheritdoc/>
        public IEnumerable<T> Shuffle(IEnumerable<T> elements, int firstN)
        {
            var list = new List<T>(elements);
            for (int i = 0; i < firstN - 1; i++)
            {
                int k = _rng.NextInt(i, list.Count);
                T tmp = list[i];
                list[i] = list[k];
                list[k] = tmp;
            }

            return list;
        }
    }
}