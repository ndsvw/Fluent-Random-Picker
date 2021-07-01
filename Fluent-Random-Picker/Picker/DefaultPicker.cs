﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.Random;

namespace FluentRandomPicker.Picker
{
    /// <summary>
    /// A default implementation of a picker.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class DefaultPicker<T> : IPicker<IEnumerable<T>>
    {
        private readonly int m_NumberOfElements;
        private readonly IRandomNumberGenerator m_Rng;
        private readonly ValuePriorityPairs<T> m_Pairs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPicker{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        /// <param name="pPairs">The value-priority paris to pick from.</param>
        public DefaultPicker(IRandomNumberGenerator pRng, ValuePriorityPairs<T> pPairs)
            : this(pRng, pPairs, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPicker{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        /// <param name="pPairs">The value-priority paris to pick from.</param>
        /// <param name="pNumberOfElements">The number of elements to pick.</param>
        public DefaultPicker(IRandomNumberGenerator pRng, ValuePriorityPairs<T> pPairs, int pNumberOfElements)
        {
            m_Rng = pRng;
            m_NumberOfElements = pNumberOfElements;
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

            if (m_Pairs.Priority == Priority.None)
            {
                var elements = Enumerable.Repeat(m_Pairs[m_Rng.NextInt(m_Pairs.Count())], m_NumberOfElements);
                return new PickResult<IEnumerable<T>>(elements.Select(e => e.Value));
            }
            else
            {
                var prioritySum = m_Pairs.Sum(v => v.Priority);
                if (m_Pairs.Priority == Priority.Percentage && prioritySum != 100)
                    throw new InvalidOperationException("The percentage values must sum up to 100.");
                var values = Enumerable.Repeat(PickPrioritized(prioritySum), m_NumberOfElements);
                return new PickResult<IEnumerable<T>>(values);
            }
        }

        private T PickPrioritized(int pPrioritySum)
        {
            var n = m_Rng.NextInt(pPrioritySum);

            int localSum = 0;
            foreach (var pair in m_Pairs)
            {
                localSum += pair.Priority;

                if (localSum >= n + 1)
                    return pair.Value;
            }

            throw new ArgumentException("Sum of priorities was wrong", nameof(pPrioritySum));
        }
    }
}
