using System;
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
        private readonly int _numberOfElements;
        private readonly IRandomNumberGenerator _rng;
        private readonly ValuePriorityPairs<T> _pairs;

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
            _rng = pRng;
            _numberOfElements = pNumberOfElements;
            _pairs = pPairs;
        }

        /// <inheritdoc/>
        public PickResult<IEnumerable<T>> Pick()
        {
            if (_numberOfElements > _pairs.Count())
                throw new NotEnoughValuesToPickException();

            if (_numberOfElements < 0)
                throw new PickingNegativeNumberOfValuesNotPossibleException();

            if (_numberOfElements == 0)
                return new PickResult<IEnumerable<T>>(Enumerable.Empty<T>());

            if (_pairs.Priority == Priority.None)
            {
                var elements = Enumerable.Repeat(_pairs[_rng.NextInt(_pairs.Count())], _numberOfElements);
                return new PickResult<IEnumerable<T>>(elements.Select(e => e.Value));
            }
            else
            {
                var prioritySum = _pairs.Sum(v => v.Priority);
                if (_pairs.Priority == Priority.Percentage && prioritySum != 100)
                    throw new InvalidOperationException("The percentage values must sum up to 100.");
                var values = Enumerable.Repeat(PickPrioritized(prioritySum), _numberOfElements);
                return new PickResult<IEnumerable<T>>(values);
            }
        }

        private T PickPrioritized(int pPrioritySum)
        {
            var n = _rng.NextInt(pPrioritySum);

            int localSum = 0;
            foreach (var pair in _pairs)
            {
                localSum += pair.Priority;

                if (localSum >= n + 1)
                    return pair.Value;
            }

            throw new ArgumentException("Sum of priorities was wrong", nameof(pPrioritySum));
        }
    }
}
