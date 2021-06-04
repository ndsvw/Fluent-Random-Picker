using System;
using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker.Exceptions;
using Fluent_Random_Picker.Random;
using Fluent_Random_Picker.Shuffle;

namespace Fluent_Random_Picker.Picker
{
    /// <summary>
    /// An implementation of a picker that picks distinct values.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class DistinctPicker<T> : IPicker<IEnumerable<T>>
    {
        private readonly int m_NumberOfElements;
        private readonly IRandomNumberGenerator m_Rng;
        private readonly ValuePriorityPairs<T> m_Pairs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        /// <param name="pPairs">The value-priority paris to pick from.</param>
        public DistinctPicker(IRandomNumberGenerator pRng, ValuePriorityPairs<T> pPairs)
            : this(pRng, pPairs, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        /// <param name="pPairs">The value-priority paris to pick from.</param>
        /// <param name="pNumberOfElements">The number of elements to pick.</param>
        public DistinctPicker(IRandomNumberGenerator pRng, ValuePriorityPairs<T> pPairs, int pNumberOfElements)
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
                return new PickResult<IEnumerable<T>>(PickDistinctElementsWithEqualPriorities());

            if (m_Pairs.Priority == Priority.Percentage && m_Pairs.Sum(v => v.Priority) != 100)
                throw new InvalidOperationException("The percentage values must sum up to 100.");

            return new PickResult<IEnumerable<T>>(PickDistinctElementsWithDifferentPriorities());
        }

        private IEnumerable<T> PickDistinctElementsWithDifferentPriorities()
        {
            var shuffle = new PrioritizedLeftShuffle<T>(m_Rng);
            var shuffledElements = shuffle.Shuffle(m_Pairs, m_NumberOfElements);
            return shuffledElements.Take(m_NumberOfElements).ToList();
        }

        private IEnumerable<T> PickDistinctElementsWithEqualPriorities()
        {
            var shuffle = new FisherYatesShuffle<T>(m_Rng);
            var shuffledElements = shuffle.Shuffle(m_Pairs.Select(p => p.Value), m_NumberOfElements);
            return shuffledElements.Take(m_NumberOfElements).ToList();
        }
    }
}
