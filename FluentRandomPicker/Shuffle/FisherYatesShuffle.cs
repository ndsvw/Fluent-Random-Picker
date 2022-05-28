using System;
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
            var elementsList = elements.ToList();
            return Shuffle(elementsList, elementsList.Count);
        }

        /// <inheritdoc/>
        public IEnumerable<T> Shuffle(IEnumerable<T> elements, int firstN)
        {
            var list = elements.ToList();
            var lastIndex = Math.Min(firstN, list.Count) - 1;
            for (var i = 0; i <= lastIndex; i++)
            {
                var k = _rng.NextInt(i, list.Count);
                (list[i], list[k]) = (list[k], list[i]);
            }

            return list;
        }
    }
}