using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Random;

namespace FluentRandomPicker.Shuffle;

/// <summary>
/// The Fisher-Yates shuffle algorithm.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
internal sealed class FisherYatesShuffle<T> : IShuffle<T>
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
    public void Shuffle(T[] elements)
    {
        Shuffle(elements, elements.Length);
    }

    /// <inheritdoc/>
    public void Shuffle(T[] elements, int firstN)
    {
        var lastIndex = Math.Min(firstN, elements.Length) - 1;
        for (var i = 0; i <= lastIndex; i++)
        {
            var k = _rng.NextInt(i, elements.Length);
            (elements[i], elements[k]) = (elements[k], elements[i]);
        }
    }
}