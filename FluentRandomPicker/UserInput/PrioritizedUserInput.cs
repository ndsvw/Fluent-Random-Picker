using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker.UserInput;

internal class PrioritizedUserInput<T> : IUserInput<T>
{
    public PriorityType PriorityType { get; set; }

    public LinkedList<IPrioritizedContainer<T>> Containers { get; } = new();

    public void Evaluate()
    {
        // todo
    }

    public void SpecifyPriority(PriorityType priorityType, int priority)
    {
        var lastContainer = Containers.Last.Value;

        if (lastContainer is not IValueContainer<T> singleValueContainer)
            throw new InvalidOperationException("Cannot call SpecifyPriority on container that does not hold single value");

        Containers.RemoveLast(); // O(1)
        Containers.AddLast(new ValuePriorityContainer<T>(singleValueContainer.Value, priority)); // O(1)

        PriorityType = priorityType;
    }

    public void SpecifyPriority(PriorityType priorityType, int? priority)
    {
        var lastContainer = Containers.Last.Value;

        if (lastContainer is not IValueContainer<T> singleValueContainer)
            throw new InvalidOperationException("Cannot call SpecifyPriority on container that does not hold single value");

        Containers.RemoveLast(); // O(1)
        Containers.AddLast(new ValueNullablePriorityContainer<T>(singleValueContainer.Value, priority)); // O(1)

        PriorityType = priorityType;
    }

    public void SpecifyPriorities(PriorityType priorityType, IEnumerable<int> priorities)
    {
        var lastContainer = Containers.Last.Value;

        if (lastContainer is not IMultiValuesContainer<T> multiValuesContainer)
            throw new InvalidOperationException("Cannot call SpecifyPriorities on container that does not hold multiple values");

        Containers.RemoveLast(); // O(1)
        Containers.AddLast(new MultiValuePrioritiesContainer<T>(multiValuesContainer.Values, priorities)); // O(1)

        PriorityType = priorityType;
    }

    public void SpecifyPriorities(PriorityType priorityType, IEnumerable<int?> priorities)
    {
        var lastContainer = Containers.Last.Value;

        if (lastContainer is not IMultiValuesContainer<T> multiValuesContainer)
            throw new InvalidOperationException("Cannot call SpecifyPriorities on container that does not hold multiple values");

        Containers.RemoveLast(); // O(1)
        Containers.AddLast(new MultiValueNullablePrioritiesContainer<T>(multiValuesContainer.Values, priorities)); // O(1)

        PriorityType = priorityType;
    }
}