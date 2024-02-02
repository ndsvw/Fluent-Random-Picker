using System.Collections.Generic;

namespace FluentRandomPicker.UserInput;

internal interface IMultiValuesContainer<T> : IContainer<T>
{
    IEnumerable<T> Values { get; }
}