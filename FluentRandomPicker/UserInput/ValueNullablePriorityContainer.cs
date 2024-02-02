using System.Collections.Generic;

namespace FluentRandomPicker.UserInput;

internal class ValueNullablePriorityContainer<T> : IValueContainer<T>, IPrioritizedContainer<T>
{
    public ValueNullablePriorityContainer(T value, int? priority)
    {
        Value = value;
        Priority = priority;
    }

    public T Value { get; }

    public int? Priority { get; }
}