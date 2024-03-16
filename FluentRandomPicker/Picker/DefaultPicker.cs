using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.Random;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.Picker;

/// <summary>
/// A default implementation of a picker.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
internal sealed class DefaultPicker<T> : IPicker<IEnumerable<T>>
{
    private readonly int _numberOfElementsToPick;
    private readonly IRandomNumberGenerator _rng;
    private readonly ValuePriorityPairs<T> _pairs;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultPicker{T}"/> class.
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="pairs">The value-priority paris to pick from.</param>
    public DefaultPicker(IRandomNumberGenerator rng, ValuePriorityPairs<T> pairs)
        : this(rng, pairs, 1)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultPicker{T}"/> class.
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="pairs">The value-priority paris to pick from.</param>
    /// <param name="numberOfElementsToPick">The number of elements to pick.</param>
    public DefaultPicker(IRandomNumberGenerator rng, ValuePriorityPairs<T> pairs, int numberOfElementsToPick)
    {
        _rng = rng;
        _numberOfElementsToPick = numberOfElementsToPick;
        _pairs = pairs;
    }

    /// <inheritdoc/>
    public PickResult<IEnumerable<T>> Pick()
    {
        if (_numberOfElementsToPick < 0)
            throw new PickingNegativeNumberOfValuesNotPossibleException();

        if (_numberOfElementsToPick == 0)
            return new PickResult<IEnumerable<T>>(Enumerable.Empty<T>());

        var prioritySum = _pairs.Sum(v => (long)v.Priority);
        var values = Enumerable.Range(0, _numberOfElementsToPick).Select(_ => PickPrioritized(prioritySum));
        return new PickResult<IEnumerable<T>>(values);
    }

    private T PickPrioritized(long prioritySum)
    {
        var n = (long)(_rng.NextDouble() * prioritySum);

        long localSum = 0;
        foreach (var pair in _pairs)
        {
            localSum += pair.Priority;

            if (localSum >= n + 1)
                return pair.Value;
        }

        throw new ArgumentException("Sum of priorities was wrong", nameof(prioritySum));
    }
}