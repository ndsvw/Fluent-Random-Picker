using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Random;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.Shuffle;

/// <summary>
/// A shuffle algorithm that respects weights.
/// This is a ~ linear run time implementation.
/// More: https://www.researchgate.net/publication/51962025_Roulette-wheel_selection_via_stochastic_acceptance.
/// Please note: The performance can be pretty bad if at least one weight is very high and others are very low
///             (e.g. [100.000.000, 1, 2, 3]).
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
internal sealed class StochasticAcceptanceBasedWeightedLeftShuffle<T> : IShuffle<ValuePriorityPair<T>>
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
    public void Shuffle(ValuePriorityPair<T>[] elements)
    {
        Shuffle(elements, elements.Length);
    }

    /// <summary>
    /// Shuffles the first n elements and respects the probabilities in O(n) time.
    /// </summary>
    /// <param name="elements">The elements (value and probability) to shuffle.</param>
    /// <param name="firstN">Limits how many of the first elements to shuffle.</param>
    public void Shuffle(ValuePriorityPair<T>[] elements, int firstN)
    {
        var max = elements.Max(v => v.Priority);

        var lastIndex = Math.Min(firstN, elements.Length) - 1;
        for (int i = 0; i <= lastIndex; i++)
        {
            int randomIndex = RouletteWheelSelection(elements, i, max);
            Swap(elements, i, randomIndex);

            // could be optimized by updating the max sometimes, but it's O(n)..
        }
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