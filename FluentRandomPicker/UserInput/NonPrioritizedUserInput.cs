using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.UserInput;

internal class NonPrioritizedUserInput<T> : IUserInput<T>
{
    public PriorityType PriorityType { get; set; }

    public LinkedList<INonPrioritizedContainer<T>> Containers { get; } = new();

    public void Evaluate()
    {
        var result = Enumerable.Empty<T>();
        foreach (var container in Containers)
        {
            if (result is IValueContainer<T> singleValueContainer)
                result = result.Concat(new[] { singleValueContainer.Value });
            else if (result is IMultiValuesContainer<T> multiValuesContainer)
                result = result.Concat(multiValuesContainer.Values);
            else
                throw new NotImplementedException("invalid type");
        }

        var evaluatedContainer = new EvaluatedValuesContainer<T>(result.ToArray());
        // var evaluatedContainer = new EvaluatedValuesContainer<T>(Containers.SelectMany(x => x.EnumerateValues()).ToArray());
        Containers.Clear();
        Containers.AddLast(evaluatedContainer);
    }
}