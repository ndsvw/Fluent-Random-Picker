namespace Fluent_Random_Picker
{
    /// <summary>
    /// A pair of a value and its priority.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class ValuePriorityPair<T>
    {
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
