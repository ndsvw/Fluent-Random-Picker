namespace Fluent_Random_Picker.Picker
{
    /// <summary>
    /// The result of a Pick operation.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class PickResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PickResult{T}"/> class.
        /// </summary>
        /// <param name="pResult">The result.</param>
        public PickResult(T pResult)
        {
            Result = pResult;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public T Result { get; }
    }
}
