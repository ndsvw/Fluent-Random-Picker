using System.Collections.Generic;

namespace FluentRandomPicker.ValuePriorities
{
    internal class NoValuePrioriyPairsGenerator<T> : IValuePriorityPairsGenerator<T>
    {
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
