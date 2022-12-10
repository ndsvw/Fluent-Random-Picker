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

            var ranks = Enumerable.Range(0, n)
                            .Select(i => (Index: i, Rank: CalculateRank(elementsList[i].Priority)))
                            .ToArray();

            Array.Sort(ranks, _indexRankTupleComparer);

            ImproveAccuracyIfNecessary(ranks, elementsList);

            return ranks.Take(firstN).Select(x => elementsList[x.Index]);
        }

        private double CalculateRank(int priority)
        {
            return Math.Pow(GenerateRandomDoubleBetween0ExclusiveAnd1Exclusive(), 1.0 / priority);
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

        /// <summary>
        /// Improves the accuracy of the ranks if necessary.
        /// In rarer cases, multiple ranks can be exactly 1 although the priorities differ a lot:
        /// https://github.com/ndsvw/Fluent-Random-Picker/issues/14#issuecomment-1264280921
        /// This solves this problem.
        /// </summary>
        /// <param name="sortedRanks">The already generated ranks (sorted descending).</param>
        /// <param name="elementsList">The elements.</param>
        /// <param name="level">The accuracy level. The longer there are multiple ranks being exactly 1, the higher the level.</param>
        internal void ImproveAccuracyIfNecessary((int Index, double Rank)[] sortedRanks, IList<ValuePriorityPair<T>> elementsList, int level = 2)
        {
            if (sortedRanks.Length == 0)
                return;

            int numberOf1Ranks = 0;

            // comparison without delta is intended
            while (sortedRanks[numberOf1Ranks].Rank == 1)
            {
                var index = sortedRanks[numberOf1Ranks].Index;
                var newRank = level + CalculateRank(elementsList[index].Priority / level);
                sortedRanks[numberOf1Ranks] = (index, newRank);
                numberOf1Ranks++;
            }

            if (numberOf1Ranks >= 2)
            {
                Array.Sort(sortedRanks, 0, numberOf1Ranks, _indexRankTupleComparer);
                ImproveAccuracyIfNecessary(sortedRanks, elementsList, level * 2);
            }
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
