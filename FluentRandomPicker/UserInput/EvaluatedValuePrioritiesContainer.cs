using System.Collections.Generic;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.UserInput;

internal class EvaluatedValuePrioritiesContainer<T> : IPrioritizedContainer<T>
{
    public EvaluatedValuePrioritiesContainer(IReadOnlyCollection<ValuePriorityPair<T>> valuePriorities)
    {
        Pairs = valuePriorities;
    }

    public IReadOnlyCollection<ValuePriorityPair<T>> Pairs { get; }

    public long Sum;
}