using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentRandomPicker.Random;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.Shuffle;

/// <summary>
/// A shuffle algorithm that respects weights.
/// This is an O(n*log(n)) implementation.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
internal sealed class SortingBasedWeightedLeftShuffle<T> : IShuffle<ValuePriorityPair<T>>
{
    private readonly IRandomNumberGenerator _rng;

    private static readonly RankComparer _rankComparer = new();

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
            .Select(i => CalculateRank(elements[i].Priority))
            .ToArray();

        Array.Sort(ranks, elements, 0, elements.Length, _rankComparer);
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

    private sealed class RankComparer : IComparer<double>
    {
        public int Compare(double value1, double value2)
        {
            return -value1.CompareTo(value2);
        }
    }
}