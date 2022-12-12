using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentRandomPicker.ValuePriorities
{
    /// <summary>
    /// An generator that generates value-priority-pairs for
    /// values with percentages.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    internal class PercentageValuePriorityPairsGenerator<T> : IValuePriorityPairsGenerator<T>
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

            priorities = priorities.Concat(Enumerable.Repeat(default(int?), numberOfValues - numberOfPriorities)).ToList();

            return GeneratePairs(values, priorities);
        }

        private ValuePriorityPairs<T> GeneratePairs(IReadOnlyCollection<T> values, IReadOnlyCollection<int?> priorities)
        {
            var sumOfPercentages = priorities.Where(x => x != null).Sum(x => x!.Value);
            if (sumOfPercentages > 100)
                throw new ArgumentException("The sum of the percentages must not be larger than 100.", nameof(priorities));

            var numberOfNullPriorities = priorities.Count(x => x == default);
            if (sumOfPercentages == 100 && numberOfNullPriorities > 0)
                throw new ArgumentException("There can't be omitted percentage values when the existing percentage values do already sum up to 100.", nameof(priorities));

            if (sumOfPercentages < 100 && numberOfNullPriorities == 0)
                throw new ArgumentException("The sum of the percentages must not be lower than 100.", nameof(priorities));

            var valuePriorityPairs = new ValuePriorityPairs<T>();

            if (numberOfNullPriorities == 0)
            {
                valuePriorityPairs.AddRange(values, priorities.Cast<int>());
                return valuePriorityPairs;
            }

            if ((100 - sumOfPercentages) % numberOfNullPriorities != 0)
                throw new ArgumentException("The percentages missing to reach 100 is not divisible by the number of omitted values without reminder.", nameof(priorities));

            var replacementValue = (100 - sumOfPercentages) / numberOfNullPriorities;
            var numericPriorities = priorities.Select(x => x ?? replacementValue);

            valuePriorityPairs.AddRange(values, numericPriorities);
            return valuePriorityPairs;
        }
    }
}
