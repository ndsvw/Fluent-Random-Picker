using System.Collections.Generic;

namespace FluentRandomPicker.ValuePriorities
{
    /// <summary>
    /// An interface for a class that takes some values and priorities,
    /// and sets missing priorities based on their priority types.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    internal interface IValuePriorityPairsGenerator<T>
    {
        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="priorities">The priorities (some may be null).</param>
        /// <returns>A collection of value-priority-pairs (an <see cref="ValuePriorityPairs{T}"/> instance).</returns>
        ValuePriorityPairs<T> Generate(IReadOnlyCollection<T> values, IReadOnlyCollection<int?> priorities);
    }
}
