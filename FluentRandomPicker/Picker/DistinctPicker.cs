﻿using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.Random;
using FluentRandomPicker.Shuffle;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.Picker;

/// <summary>
/// An implementation of a picker that picks distinct values.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
internal sealed class DistinctPicker<T> : IPicker<IEnumerable<T>>
{
    private readonly int _numberOfElementsToPick;
    private readonly IRandomNumberGenerator _rng;
    private readonly ValuePriorityPairs<T> _pairs;

    /// <summary>
    /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="pairs">The value-priority paris to pick from.</param>
    public DistinctPicker(IRandomNumberGenerator rng, ValuePriorityPairs<T> pairs)
        : this(rng, pairs, 1)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="pairs">The value-priority paris to pick from.</param>
    /// <param name="numberOfElementsToPick">The number of elements to pick.</param>
    public DistinctPicker(IRandomNumberGenerator rng, ValuePriorityPairs<T> pairs, int numberOfElementsToPick)
    {
        _rng = rng;
        _numberOfElementsToPick = numberOfElementsToPick;
        _pairs = pairs;
    }

    /// <inheritdoc/>
    public PickResult<IEnumerable<T>> Pick()
    {
        if (_numberOfElementsToPick > _pairs.Count())
            throw new NotEnoughValuesToPickException();

        if (_numberOfElementsToPick < 0)
            throw new PickingNegativeNumberOfValuesNotPossibleException();

        if (_numberOfElementsToPick == 0)
            return new PickResult<IEnumerable<T>>(Enumerable.Empty<T>());

        var firstPriority = _pairs.First().Priority;
        if (_pairs.All(x => x.Priority == firstPriority))
            return new PickResult<IEnumerable<T>>(PickDistinctElementsWithEqualPriorities());

        return new PickResult<IEnumerable<T>>(PickDistinctElementsWithDifferentPriorities());
    }

    private IEnumerable<T> PickDistinctElementsWithDifferentPriorities()
    {
        var pairs = _pairs.ToArray();
        var shuffle = new SortingBasedWeightedLeftShuffle<T>(_rng);
        shuffle.Shuffle(pairs, _numberOfElementsToPick);
        return pairs.Take(_numberOfElementsToPick).Select(x => x.Value).ToList();
    }

    private IEnumerable<T> PickDistinctElementsWithEqualPriorities()
    {
        var values = _pairs.Select(p => p.Value).ToArray();
        var shuffle = new FisherYatesShuffle<T>(_rng);
        shuffle.Shuffle(values, _numberOfElementsToPick);
        return values.Take(_numberOfElementsToPick).ToList();
    }
}