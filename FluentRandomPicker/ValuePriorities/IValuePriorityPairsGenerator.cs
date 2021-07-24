using System.Collections.Generic;

namespace FluentRandomPicker.ValuePriorities
{
    internal interface IValuePriorityPairsGenerator<T>
    {
        ValuePriorityPairs<T> Generate(IEnumerable<T> values, IEnumerable<int?> priorities);
    }
}
