namespace FluentRandomPicker.Picker;

/// <summary>
/// The result of a Pick operation.
/// </summary>
/// <typeparam name="T">The type of the values.</typeparam>
internal sealed class PickResult<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PickResult{T}"/> class.
    /// </summary>
    /// <param name="result">The result.</param>
    public PickResult(T result)
    {
        Result = result;
    }

    /// <summary>
    /// Gets the result.
    /// </summary>
    public T Result { get; }
}