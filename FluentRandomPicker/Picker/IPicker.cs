namespace FluentRandomPicker.Picker;

/// <summary>
/// An interface for pickers.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
internal interface IPicker<T>
{
    /// <summary>
    /// Picks one or more values and returns them as a <see cref="PickResult{T}"/>.
    /// </summary>
    /// <returns>The result.</returns>
    PickResult<T> Pick();
}