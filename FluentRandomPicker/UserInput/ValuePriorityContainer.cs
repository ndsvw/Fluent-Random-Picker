using System.Collections.Generic;

namespace FluentRandomPicker.UserInput;

internal class ValuePriorityContainer<T> : IValueContainer<T>, IPrioritizedContainer<T>
{
    public ValuePriorityContainer(T value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    public T Value { get; }

    public int Priority { get; }
}