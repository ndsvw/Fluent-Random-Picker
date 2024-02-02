using System.Collections.Generic;

namespace FluentRandomPicker.UserInput;

internal class MultiValuesContainer<T> : IMultiValuesContainer<T>, INonPrioritizedContainer<T>
{
    public MultiValuesContainer(IEnumerable<T> values)
    {
        Values = values;
    }

    public IEnumerable<T> Values { get; }
}