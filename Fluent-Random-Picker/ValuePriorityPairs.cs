using System.Collections;
using System.Collections.Generic;

namespace Fluent_Random_Picker
{
    /// <summary>
    /// A collection of ValuePriorityPairs.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class ValuePriorityPairs<T> : IEnumerable<ValuePriorityPair<T>>
    {
        private readonly List<ValuePriorityPair<T>> m_Pairs = new List<ValuePriorityPair<T>>();

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Allows index access to the pairs.
        /// </summary>
        /// <param name="i">The index.</param>
        public ValuePriorityPair<T> this[int i]
        {
            get { return m_Pairs[i]; }
            set { m_Pairs[i] = value; }
        }

        /// <inheritdoc/>
        public void Add(ValuePriorityPair<T> pPair)
        {
            m_Pairs.Add(pPair);
        }

        /// <inheritdoc/>
        public IEnumerator<ValuePriorityPair<T>> GetEnumerator()
        {
            return m_Pairs.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Pairs.GetEnumerator();
        }
    }
}
