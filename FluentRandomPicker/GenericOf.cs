using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Interfaces;
using FluentRandomPicker.Random;

namespace FluentRandomPicker
{
    /// <summary>
    /// A generic implementation of the fluent part "Of".
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    public class GenericOf<T>
    {
        private readonly IRandomNumberGenerator _rng;

        private GenericOf()
        {
            _rng = new DefaultRandomNumberGenerator();
        }

        private GenericOf(int pSeed)
        {
            _rng = new DefaultRandomNumberGenerator(pSeed);
        }

        private GenericOf(IRandomNumberGenerator pRng)
        {
            _rng = pRng;
        }

        /// <summary>
        /// Creates an instance of <see cref="GenericOf{T}"/>.
        /// </summary>
        /// <returns>A <see cref="GenericOf{T}"/> instance.</returns>
        public static GenericOf<T> Create()
        {
            return new GenericOf<T>();
        }

        /// <summary>
        /// Creates an instance of <see cref="GenericOf{T}"/>.
        /// </summary>
        /// <param name="pSeed">The seed.</param>
        /// <returns>A <see cref="GenericOf{T}"/> instance.</returns>
        public static GenericOf<T> Create(int pSeed)
        {
            return new GenericOf<T>(pSeed);
        }

        /// <summary>
        /// Creates an instance of <see cref="GenericOf{T}"/>.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        /// <returns>A <see cref="GenericOf{T}"/> instance.</returns>
        public static GenericOf<T> Create(IRandomNumberGenerator pRng)
        {
            return new GenericOf<T>(pRng);
        }

        /// <summary>
        /// Specifies the first value.
        /// </summary>
        /// <param name="t">The value.</param>
        /// <returns>An object that can have an optional value priority and needs at least one more value.</returns>
        public ICanHaveValuePriorityAndNeed1MoreValue<T> Value(T t)
        {
            return new RandomPicker<T>(_rng).Value(t);
        }

        /// <summary>
        /// Specifies multiple values.
        /// </summary>
        /// <param name="ts">The values.</param>
        /// <returns>An object that can have optional value priorities.</returns>
        public ICanHaveValuePrioritiesAndPick<T> Values(IEnumerable<T> ts)
        {
            return new RandomPicker<T>(_rng).Values(ts);
        }
    }
}
