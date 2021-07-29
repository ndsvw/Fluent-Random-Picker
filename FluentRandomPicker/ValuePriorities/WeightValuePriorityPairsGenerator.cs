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
    internal class WeightValuePriorityPairsGenerator<T> : IValuePriorityPairsGenerator<T>
    {
        /// <inheritdoc/>
        public ValuePriorityPairs<T> Generate(IEnumerable<T> values, IEnumerable<int?> priorities)
        {
            var numberOfValues = values.Count();
            var numberOfPriorities = priorities.Count();
            if (numberOfValues == numberOfPriorities)
                return GeneratePairs(values, priorities);
            if (numberOfValues < numberOfPriorities)
                throw new ArgumentException("The number of values must not be smaller than the number of priorities.", nameof(priorities));

            priorities = priorities.Union(Enumerable.Repeat(default(int?), numberOfValues - numberOfPriorities));

            return GeneratePairs(values, priorities);
        }

        private ValuePriorityPairs<T> GeneratePairs(IEnumerable<T> values, IEnumerable<int?> priorities)
        {
            var numberOfNullPriorities = priorities.Count(x => x == default);

            if (numberOfNullPriorities == 0)
                return Zip(values, priorities.Cast<int>());

            const int ReplacementValue = 1;
            priorities = priorities.Select(x => x == null ? ReplacementValue : x);

            return Zip(values, priorities.Cast<int>());
        }

        private static ValuePriorityPairs<T> Zip(IEnumerable<T> values, IEnumerable<int> priorities)
        {
            var valuePriorityPairs = new ValuePriorityPairs<T>();
            var priorityEnumerator = priorities.GetEnumerator();
            foreach (var value in values)
            {
                priorityEnumerator.MoveNext();
                valuePriorityPairs.Add(new ValuePriorityPair<T>(value, priorityEnumerator.Current));
            }

            return valuePriorityPairs;
        }
    }
}
