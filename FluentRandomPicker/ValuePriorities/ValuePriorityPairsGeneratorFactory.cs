using System;

namespace FluentRandomPicker.ValuePriorities
{
    /// <summary>
    /// A factory that creates instances of <see cref="IValuePriorityPairsGenerator{T}"/>
    /// based on the priority type it receives.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    internal sealed class ValuePriorityPairsGeneratorFactory<T>
    {
        /// <summary>
        /// Creates an <see cref="IValuePriorityPairsGenerator{T}"/> instance
        /// based on the passed priority type.
        /// </summary>
        /// <param name="priorityType">The priority type.</param>
        /// <returns>An <see cref="IValuePriorityPairsGenerator{T}"/> instance based on the passed priority type.</returns>
        public IValuePriorityPairsGenerator<T> Create(PriorityType priorityType)
        {
            switch (priorityType)
            {
                case PriorityType.None:
                    return new NoValuePriorityPairsGenerator<T>();
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
