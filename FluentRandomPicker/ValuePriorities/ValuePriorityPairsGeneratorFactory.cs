using System;

namespace FluentRandomPicker.ValuePriorities
{
    internal class ValuePriorityPairsGeneratorFactory<T>
    {
        public IValuePriorityPairsGenerator<T> Create(Priority priorityType)
        {
            switch (priorityType)
            {
                case Priority.None:
                    return new NoValuePrioriyPairsGenerator<T>();
                case Priority.Percentage:
                    return new PercentageValuePriorityPairsGenerator<T>();
                case Priority.Weight:
                    return new WeightValuePriorityPairsGenerator<T>();
                default:
                    throw new ArgumentException("Invalid priority type", nameof(priorityType));
            }
        }
    }
}
