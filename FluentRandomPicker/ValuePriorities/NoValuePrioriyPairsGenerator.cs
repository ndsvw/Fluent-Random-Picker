using System.Collections.Generic;

namespace FluentRandomPicker.ValuePriorities
{
    /// <summary>
    /// An generator that generates value-priority-pairs for
    /// values without priorities.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    internal class NoValuePrioriyPairsGenerator<T> : IValuePriorityPairsGenerator<T>
    {
        /// <inheritdoc/>
        public ValuePriorityPairs<T> Generate(IEnumerable<T> values, IEnumerable<int?> priorities)
        {
            var valuePriorityPairs = new ValuePriorityPairs<T>();
            foreach (var value in values)
            {
                valuePriorityPairs.Add(new ValuePriorityPair<T>(value, 1));
            }

            return valuePriorityPairs;
        }
    }
}
