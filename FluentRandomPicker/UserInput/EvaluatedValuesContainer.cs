using System.Collections.Generic;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.UserInput;

internal class EvaluatedValuesContainer<T> : INonPrioritizedContainer<T>
{
    public EvaluatedValuesContainer(T[] values)
    {
        Values = values;
    }

    public IReadOnlyCollection<T> Values { get; }
}