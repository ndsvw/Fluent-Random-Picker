using System.Collections.Generic;
using System.Linq;

namespace FluentRandomPicker.ValuePriorities
{
    /// <summary>
    /// An generator that generates value-priority-pairs for
    /// values without priorities.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    internal class NoValuePriorityPairsGenerator<T> : IValuePriorityPairsGenerator<T>
    {
        /// <inheritdoc/>
        public ValuePriorityPairs<T> Generate(IEnumerable<T> values, IEnumerable<int?> priorities)
        {
            var valuePriorityPairs = new ValuePriorityPairs<T>();
            valuePriorityPairs.AddRange(values, Enumerable.Repeat(1, values.Count()));

            return valuePriorityPairs;
        }
    }
}
