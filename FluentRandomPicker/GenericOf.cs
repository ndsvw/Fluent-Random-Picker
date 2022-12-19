using System.Collections.Generic;
using FluentRandomPicker.FluentInterfaces;
using FluentRandomPicker.FluentInterfaces.Selectors;
using FluentRandomPicker.Random;

namespace FluentRandomPicker;

/// <summary>
/// A generic implementation of the fluent part "Of".
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
public class GenericOf<T>
{
    private readonly IRandomNumberGenerator _rng;

    private GenericOf()
    {
        _rng = new DefaultRandomNumberGenerator();
    }

    private GenericOf(int seed)
    {
        _rng = new DefaultRandomNumberGenerator(seed);
    }

    private GenericOf(IRandomNumberGenerator rng)
    {
        _rng = rng;
    }

    /// <summary>
    /// Creates an instance of <see cref="GenericOf{T}"/>.
    /// </summary>
    /// <returns>A <see cref="GenericOf{T}"/> instance.</returns>
    public static GenericOf<T> Create()
    {
        return new GenericOf<T>();
    }

    /// <summary>
    /// Creates an instance of <see cref="GenericOf{T}"/>.
    /// </summary>
    /// <param name="seed">The seed.</param>
    /// <returns>A <see cref="GenericOf{T}"/> instance.</returns>
    public static GenericOf<T> Create(int seed)
    {
        return new GenericOf<T>(seed);
    }

    /// <summary>
    /// Creates an instance of <see cref="GenericOf{T}"/>.
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <returns>A <see cref="GenericOf{T}"/> instance.</returns>
    public static GenericOf<T> Create(IRandomNumberGenerator rng)
    {
        return new GenericOf<T>(rng);
    }

    /// <summary>
    /// Specifies the first value.
    /// </summary>
    /// <param name="t">The value.</param>
    /// <returns>An object that can have an optional value priority and needs at least one more value.</returns>
    public ISpecifyValueOrGenesisValuePriority<T> Value(T t)
    {
        return new RandomPicker<T>(_rng).Value(t);
    }

    /// <summary>
    /// Specifies multiple values.
    /// </summary>
    /// <param name="ts">The values.</param>
    /// <returns>An object that can have optional value priorities.</returns>
    public ISpecifyValuePrioritiesOrPick<T> Values(IEnumerable<T> ts)
    {
        return new RandomPicker<T>(_rng).Values(ts);
    }

    /// <summary>
    /// Specifies one or more elements that contain(s) information like weights or percentages.
    /// </summary>
    /// <param name="ts">The elements.</param>
    /// <returns>An object that allows specifying selectors to determine how to get the value (picked instance) of the element or its weight/percentage.</returns>
    public ISpecifySelector<T> PrioritizedElements(IEnumerable<T> ts)
    {
        return new SelectorBasedRandomPicker<T>(_rng).PrioritizedElements(ts);
    }
}