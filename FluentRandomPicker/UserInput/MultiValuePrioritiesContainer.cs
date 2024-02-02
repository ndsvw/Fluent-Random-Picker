using System.Collections.Generic;

namespace FluentRandomPicker.UserInput;

internal class MultiValuePrioritiesContainer<T> : IMultiValuesContainer<T>, IPrioritizedContainer<T>
{
    public MultiValuePrioritiesContainer(IEnumerable<T> values, IEnumerable<int> priorities)
    {
        Values = values;
        Priorities = priorities;
    }

    public IEnumerable<T> Values { get; }

    public IEnumerable<int> Priorities { get; }
}