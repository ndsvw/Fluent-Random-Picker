using System;

namespace FluentRandomPicker.ValuePriorities
{
    internal class ValuePriorityPairsGeneratorFactory<T>
    {
        public IValuePriorityPairsGenerator<T> Create(PriorityType priorityType)
        {
            switch (priorityType)
            {
                case PriorityType.None:
                    return new NoValuePrioriyPairsGenerator<T>();
                case PriorityType.Percentage:
                    return new PercentageValuePriorityPairsGenerator<T>();
                case PriorityType.Weight:
                    return new WeightValuePriorityPairsGenerator<T>();
                default:
                    throw new ArgumentException("Invalid priority type", nameof(priorityType));
            }
        }
    }
}
