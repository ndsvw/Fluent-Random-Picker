using System.Collections;
using System.Collections.Generic;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.UserInput;

internal class ValueContainer<T> : IValueContainer<T>, INonPrioritizedContainer<T>
{
    public ValueContainer(T value)
    {
        Value = value;
    }

    public T Value { get; }
}