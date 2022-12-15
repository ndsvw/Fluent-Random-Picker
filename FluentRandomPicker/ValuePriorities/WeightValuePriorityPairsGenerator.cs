using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentRandomPicker.ValuePriorities
{
    /// <summary>
    /// An generator that generates value-priority-pairs for
    /// values with weights.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    internal sealed class WeightValuePriorityPairsGenerator<T> : IValuePriorityPairsGenerator<T>
    {
        /// <inheritdoc/>
        public ValuePriorityPairs<T> Generate(IReadOnlyCollection<T> values, IReadOnlyCollection<int?> priorities)
        {
            var numberOfValues = values.Count;
            var numberOfPriorities = priorities.Count;
            if (numberOfValues == numberOfPriorities)
                return GeneratePairs(values, priorities);
            if (numberOfValues < numberOfPriorities)
                throw new ArgumentException("The number of values must not be smaller than the number of priorities.", nameof(priorities));

            priorities = priorities.Union(Enumerable.Repeat(default(int?), numberOfValues - numberOfPriorities)).ToList();

            return GeneratePairs(values, priorities);
        }

        private ValuePriorityPairs<T> GeneratePairs(IReadOnlyCollection<T> values, IReadOnlyCollection<int?> priorities)
        {
            var valuePriorityPairs = new ValuePriorityPairs<T>();
            var numberOfNullPriorities = priorities.Count(x => x == default);

            if (numberOfNullPriorities == 0)
            {
                valuePriorityPairs.AddRange(values, priorities.Cast<int>());
                return valuePriorityPairs;
            }

            const int replacementValue = 1;
            var numericPriorities = priorities.Select(x => x ?? replacementValue);

            valuePriorityPairs.AddRange(values, numericPriorities);
            return valuePriorityPairs;
        }
    }
}
