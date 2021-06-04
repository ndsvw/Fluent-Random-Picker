using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker.Interfaces;
using Fluent_Random_Picker.Random;

namespace Fluent_Random_Picker
{
    /// <summary>
    /// A generic implementation of the fluent part "Of".
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    public class GenericOf<T>
    {
        private readonly IRandomNumberGenerator m_Rng;

        private GenericOf()
        {
            m_Rng = new DefaultRandomNumberGenerator();
        }

        private GenericOf(int pSeed)
        {
            m_Rng = new DefaultRandomNumberGenerator(pSeed);
        }

        private GenericOf(IRandomNumberGenerator pRng)
        {
            m_Rng = pRng;
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
            return new RandomPicker<T>(m_Rng).Value(t);
        }

        /// <summary>
        /// Specifies multiple values.
        /// </summary>
        /// <param name="ts">The values.</param>
        /// <returns>An object that can have optional value priorities.</returns>
        public ICanHaveValuePrioritiesAndPick<T> Values(IEnumerable<T> ts)
        {
            return new RandomPicker<T>(m_Rng).Values(ts);
        }

        /// <summary>
        /// Specifies multiple values.
        /// </summary>
        /// <param name="ts">The values.</param>
        /// <returns>An object that can have optional value priorities.</returns>
        public ICanHaveValuePrioritiesAndPick<T> Values(params T[] ts)
        {
            return Values(ts.ToList()); // ToList() is necessary. Otherwise endless recursion
        }
    }
}
