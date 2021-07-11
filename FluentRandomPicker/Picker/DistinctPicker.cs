using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.Random;
using FluentRandomPicker.Shuffle;

namespace FluentRandomPicker.Picker
{
    /// <summary>
    /// An implementation of a picker that picks distinct values.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal class DistinctPicker<T> : IPicker<IEnumerable<T>>
    {
        private readonly int _numberOfElements;
        private readonly IRandomNumberGenerator _rng;
        private readonly ValuePriorityPairs<T> _pairs;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        /// <param name="pairs">The value-priority paris to pick from.</param>
        public DistinctPicker(IRandomNumberGenerator rng, ValuePriorityPairs<T> pairs)
            : this(rng, pairs, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistinctPicker{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        /// <param name="pairs">The value-priority paris to pick from.</param>
        /// <param name="numberOfElements">The number of elements to pick.</param>
        public DistinctPicker(IRandomNumberGenerator rng, ValuePriorityPairs<T> pairs, int numberOfElements)
        {
            _rng = rng;
            _numberOfElements = numberOfElements;
            _pairs = pairs;
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
                return new PickResult<IEnumerable<T>>(PickDistinctElementsWithEqualPriorities());

            if (_pairs.Priority == Priority.Percentage && _pairs.Sum(v => v.Priority) != 100)
                throw new InvalidOperationException("The percentage values must sum up to 100.");

            return new PickResult<IEnumerable<T>>(PickDistinctElementsWithDifferentPriorities());
        }

        private IEnumerable<T> PickDistinctElementsWithDifferentPriorities()
        {
            var shuffle = new PrioritizedLeftShuffle<T>(_rng);
            var shuffledElements = shuffle.Shuffle(_pairs, _numberOfElements);
            return shuffledElements.Take(_numberOfElements).Select(x => x.Value).ToList();
        }

        private IEnumerable<T> PickDistinctElementsWithEqualPriorities()
        {
            var shuffle = new FisherYatesShuffle<T>(_rng);
            var shuffledElements = shuffle.Shuffle(_pairs.Select(p => p.Value), _numberOfElements);
            return shuffledElements.Take(_numberOfElements).ToList();
        }
    }
}
