using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentRandomPicker.ExtensionMethods;
using FluentRandomPicker.Random;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.Shuffle
{
    /// <summary>
    /// A shuffle algorithm that respects weights.
    /// This is an O(n*log(n)) implementation.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal sealed class SortingBasedWeightedLeftShuffle<T> : IShuffle<ValuePriorityPair<T>>
    {
        private readonly IRandomNumberGenerator _rng;

        private static readonly IndexRankTupleComparer _indexRankTupleComparer = new();

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
        public void Shuffle(ValuePriorityPair<T>[] elements)
        {
            Shuffle(elements, elements.Length);
        }

        /// <summary>
        /// Shuffles the first n elements and respects the probabilities in O(n*log(n)) time.
        /// </summary>
        /// <param name="elements">The elements (value and probability) to shuffle.</param>
        /// <param name="firstN">Limits how many of the first elements to shuffle.</param>
        public void Shuffle(ValuePriorityPair<T>[] elements, int firstN)
        {
            var n = elements.Length;

            var ranks = Enumerable.Range(0, n)
                            .Select(i => (Index: i, Rank: CalculateRank(elements[i].Priority)))
                            .ToArray();

            Array.Sort(ranks, elements, 0, elements.Length, _indexRankTupleComparer);
        }

        /// <summary>
        /// Calculates a rank based on a priority and a generated random number.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="baseValue">The minimum value of the rank.</param>
        /// <returns>A ranks based on the priority.</returns>
        internal double CalculateRank(int priority, int baseValue = 0)
        {
            var rank = Math.Pow(GenerateRandomDoubleBetween0ExclusiveAnd1Exclusive(), 1.0 / priority);

            // comparison without delta is intended
            if (rank == 1)
                return CalculateRank(priority / 2, baseValue + 1);

            return baseValue + rank;
        }

        private double GenerateRandomDoubleBetween0ExclusiveAnd1Exclusive()
        {
            // This conditional recursion is required to make sure, 0 is not returned.
            // With random double = exactly 0, the priorities would not matter at all.
            // Chances are low (like 1/(2^53)), but it's not impossible:
            // https://stackoverflow.com/questions/73916767/what-are-the-chances-of-random-nextdouble-being-exactly-0
            var randomDouble = _rng.NextDouble();

            // comparison without delta is intended
            return randomDouble != 0 ? randomDouble : GenerateRandomDoubleBetween0ExclusiveAnd1Exclusive();
        }

        private sealed class IndexRankTupleComparer : IComparer<(int Index, double Rank)>
        {
            public int Compare((int Index, double Rank) value1, (int Index, double Rank) value2)
            {
                return -value1.Rank.CompareTo(value2.Rank);
            }
        }
    }
}
