using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentRandomPicker.ValuePriorities
{
    internal class PercentageValuePriorityPairsGenerator<T> : IValuePriorityPairsGenerator<T>
    {
        public ValuePriorityPairs<T> Generate(IEnumerable<T> values, IEnumerable<int?> priorities)
        {
            var numberOfValues = values.Count();
            var numberOfPriorities = priorities.Count();
            if (numberOfValues == numberOfPriorities)
                return GeneratePairs(values, priorities);
            if (numberOfValues < numberOfPriorities)
                throw new ArgumentException("The number of values must not be smaller than the number of priorities.", nameof(priorities));

            priorities = priorities.Concat(Enumerable.Repeat(default(int?), numberOfValues - numberOfPriorities));

            return GeneratePairs(values, priorities);
        }

        private ValuePriorityPairs<T> GeneratePairs(IEnumerable<T> values, IEnumerable<int?> priorities)
        {
            var sumOfPercentages = priorities.Where(x => x != null).Sum();
            if (sumOfPercentages > 100)
                throw new ArgumentException("The sum of the percentages must not be larger than 100.", nameof(priorities));

            var numberOfNullPriorities = priorities.Count(x => x == default);
            if (sumOfPercentages == 100 && numberOfNullPriorities > 0)
                throw new ArgumentException("There can't be omitted percentage values when the existing percentage values do already sum up to 100.", nameof(priorities));

            if (numberOfNullPriorities == 0)
                return Zip(values, priorities.Cast<int>());

            if ((100 - sumOfPercentages) % numberOfNullPriorities != 0)
                throw new ArgumentException("The percentages missing to reach 100 is not divisible by the number of omitted values without reminder.", nameof(priorities));

            var replacementValue = (100 - sumOfPercentages) / numberOfNullPriorities;
            priorities = priorities.Select(x => x == null ? replacementValue : x);

            return Zip(values, priorities.Cast<int>());
        }

        private ValuePriorityPairs<T> Zip(IEnumerable<T> values, IEnumerable<int> priorities)
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
