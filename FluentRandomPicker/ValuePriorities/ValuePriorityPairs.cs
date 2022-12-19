using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentRandomPicker.ValuePriorities;

/// <summary>
/// A collection of ValuePriorityPairs.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
internal class ValuePriorityPairs<T> : IEnumerable<ValuePriorityPair<T>>
{
    private readonly List<ValuePriorityPair<T>> _pairs = new List<ValuePriorityPair<T>>();

    /// <summary>
    /// Allows index access to the pairs.
    /// </summary>
    /// <param name="i">The index.</param>
    public ValuePriorityPair<T> this[int i]
    {
        get { return _pairs[i]; }
    }

    /// <summary>
    /// Adds the pair to the existing paris.
    /// </summary>
    /// <param name="pair">The new pair.</param>
    public void Add(ValuePriorityPair<T> pair)
    {
        _pairs.Add(pair);
    }

    /// <summary>
    /// Adds the values with their priorities to the existing pairs.
    /// </summary>
    /// <param name="values">The values.</param>
    /// <param name="priorities">The priorities.</param>
    public void AddRange(IEnumerable<T> values, IEnumerable<int> priorities)
    {
        _pairs.AddRange(values.Zip(priorities, (v, p) => new ValuePriorityPair<T>(v, p)));
    }

    /// <inheritdoc/>
    public IEnumerator<ValuePriorityPair<T>> GetEnumerator()
    {
        return _pairs.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _pairs.GetEnumerator();
    }
}