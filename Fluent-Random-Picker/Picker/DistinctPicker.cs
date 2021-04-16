using System;
using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker.Exceptions;

namespace Fluent_Random_Picker.Picker
{
    /// <summary>
    /// An implementation of a picker that picks distinct values.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class DistinctPicker<T> : IPicker<IEnumerable<T>>
    {
        private readonly int m_NumberOfElements;
        private readonly Random m_Random;
        private readonly ValuePriorityPairs<T> m_Pairs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
        /// </summary>
        /// <param name="pPairs">The value-priority paris to pick from.</param>
        public DistinctPicker(ValuePriorityPairs<T> pPairs)
            : this(pPairs, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
        /// </summary>
        /// <param name="pPairs">The value-priority paris to pick from.</param>
        /// <param name="pNumberOfElements">The number of elements to pick.</param>
        public DistinctPicker(ValuePriorityPairs<T> pPairs, int pNumberOfElements)
        {
            m_NumberOfElements = pNumberOfElements;
            m_Random = new Random();
            m_Pairs = pPairs;
        }

        /// <inheritdoc/>
        public PickResult<IEnumerable<T>> Pick()
        {
            if (m_NumberOfElements > m_Pairs.Count())
                throw new NotEnoughValuesToPickException();

            if (m_NumberOfElements < 0)
                throw new PickingNegativeNumberOfValuesNotPossibleException();

            if (m_NumberOfElements == 0)
                return new PickResult<IEnumerable<T>>(Enumerable.Empty<T>());

            var pickedElements = new List<T>();
            var remainingElements = new List<ValuePriorityPair<T>>(m_Pairs);

            if (m_Pairs.Priority == Priority.None)
            {
                for (var i = 0; i < m_NumberOfElements; i++)
                {
                    var n = m_Random.Next(remainingElements.Count);
                    pickedElements.Add(remainingElements[n].Value);
                    remainingElements.RemoveAt(n);
                }
            }
            else
            {
                var prioritySum = m_Pairs.Sum(v => v.Priority);
                if (m_Pairs.Priority == Priority.Percentage && prioritySum != 100)
                    throw new InvalidOperationException("The percentage values must sum up to 100.");

                for (var i = 0; i < m_NumberOfElements; i++)
                {
                    prioritySum = remainingElements.Sum(v => v.Priority);
                    var index = PickPrioritizedIndex(prioritySum, m_Random, m_Pairs);
                    pickedElements.Add(remainingElements[index].Value);
                    remainingElements.RemoveAt(index);
                }
            }

            return new PickResult<IEnumerable<T>>(pickedElements);
        }

        private static int PickPrioritizedIndex(int pPrioritySum, Random pRandom, ValuePriorityPairs<T> pPairs)
        {
            var n = pRandom.Next(pPrioritySum);

            int localSum = 0;
            for (var i = 0; i < pPairs.Count(); i++)
            {
                var pair = pPairs[i];
                localSum += pair.Priority;

                if (localSum >= n + 1)
                    return i;
            }

            throw new ArgumentException("Sum of priorities was wrong", nameof(pPrioritySum));
        }
    }
}
