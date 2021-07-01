namespace FluentRandomPicker
{
    /// <summary>
    /// A pair of a value and its priority.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class ValuePriorityPair<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuePriorityPair{T}"/> class.
        /// </summary>
        /// <param name="pValue">The value.</param>
        /// <param name="pPriority">The priority.</param>
        public ValuePriorityPair(T pValue, int pPriority)
        {
            Value = pValue;
            Priority = pPriority;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public int Priority { get; set; }
    }
}
