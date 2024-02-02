using System.Collections.Generic;

namespace FluentRandomPicker.UserInput;

internal interface IValueContainer<T> : IContainer<T>
{
    T Value { get; }
}