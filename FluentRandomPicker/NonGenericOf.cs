using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.FluentInterfaces;
using FluentRandomPicker.Random;

namespace FluentRandomPicker
{
    /// <summary>
    /// A non-generic implementation of the fluent part "Of".
    /// </summary>
    public class NonGenericOf
    {
        private readonly IRandomNumberGenerator _rng;

        private NonGenericOf()
        {
            _rng = new DefaultRandomNumberGenerator();
        }

        private NonGenericOf(int seed)
        {
            _rng = new DefaultRandomNumberGenerator(seed);
        }

        private NonGenericOf(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <summary>
        /// Creates an instance of <see cref="NonGenericOf"/>.
        /// </summary>
        /// <returns>A <see cref="NonGenericOf"/> instance.</returns>
        public static NonGenericOf Create()
        {
            return new NonGenericOf();
        }

        /// <summary>
        /// Creates an instance of <see cref="NonGenericOf"/>.
        /// </summary>
        /// <param name="seed">The seed.</param>
        /// <returns>A <see cref="NonGenericOf"/> instance.</returns>
        public static NonGenericOf Create(int seed)
        {
            return new NonGenericOf(seed);
        }

        /// <summary>
        /// Creates an instance of <see cref="NonGenericOf"/>.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        /// <returns>A <see cref="NonGenericOf"/> instance.</returns>
        public static NonGenericOf Create(IRandomNumberGenerator rng)
        {
            return new NonGenericOf(rng);
        }

        /// <summary>
        /// Specifies the first value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="t">The value.</param>
        /// <returns>An object that can have an optional value priority and needs at least one more value.</returns>
#pragma warning disable CA1822 // Mark members as static; Justification: Necessary for the fluent syntax.
        public ISpecifyValueOrGenesisValuePriority<T> Value<T>(T t)
#pragma warning restore CA1822 // Mark members as static
        {
            return new RandomPicker<T>(_rng).Value(t);
        }

        /// <summary>
        /// Specifies multiple values.
        /// </summary>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <param name="ts">The values.</param>
        /// <returns>An object that can have optional value priorities.</returns>
#pragma warning disable CA1822 // Mark members as static; Justification: Necessary for the fluent syntax.
        public ISpecifyValuePrioritiesOrPick<T> Values<T>(IEnumerable<T> ts)
#pragma warning restore CA1822 // Mark members as static
        {
            return new RandomPicker<T>(_rng).Values(ts);
        }
    }
}
