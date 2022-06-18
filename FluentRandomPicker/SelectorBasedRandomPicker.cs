using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.FluentInterfaces.General;
using FluentRandomPicker.FluentInterfaces.Selectors;
using FluentRandomPicker.Random;

namespace FluentRandomPicker
{
    /// <summary>
    /// The starting point of the fluent syntax after Out.Of()
    /// when using the selector syntax.
    /// It allows specifying these selectors and picking values in the end.
    /// </summary>
    /// <typeparam name="T">The type of the element(s).</typeparam>
    public class SelectorBasedRandomPicker<T> : ISpecifySelector<T>
    {
        private readonly IRandomNumberGenerator _rng;

        private readonly List<T> _elements = new List<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorBasedRandomPicker{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        internal SelectorBasedRandomPicker(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        /// <summary>
        /// Specifies all elements.
        /// </summary>
        /// <param name="elements">The values.</param>
        /// <returns>An <see cref="ISpecifySelector{T}"/> instance.</returns>
        public ISpecifySelector<T> PrioritizedElements(IEnumerable<T> elements)
        {
            var values = elements.ToList();
            if (values.Count <= 1)
                throw new NotEnoughValuesToPickException();

            _elements.AddRange(elements);

            return this;
        }

        /// <inheritdoc/>
        public ISpecifyPrioritySelector<T, TReturn> WithValueSelector<TReturn>(Func<T, TReturn> valueSelector)
        {
            return new SelectorBasedRandomPicker<T, TReturn>(_rng, _elements, valueSelector);
        }

        /// <inheritdoc/>
        public IPick<T> WithWeightSelector(Func<T, int> weightSelector)
        {
            return Out.Of<T>(_rng).Values(_elements)
                .WithWeights(_elements.Select(weightSelector));
        }

        /// <inheritdoc/>
        public IPick<T> WithPercentageSelector(Func<T, int> percentageSelector)
        {
            return Out.Of<T>(_rng).Values(_elements)
                .WithPercentages(_elements.Select(percentageSelector));
        }
    }

    /// <summary>
    /// The fluent node after specifying a value selector.
    /// It allows specifying a weight selector or a percentage selector and picking values in the end.
    /// </summary>
    /// <typeparam name="T">The type of the element(s).</typeparam>
    /// <typeparam name="TValueSelector">The type of the values that are picked.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type; Justification: They have the same name. The logic is only separated, because it's not possible to combine everything.
    public class SelectorBasedRandomPicker<T, TValueSelector> : ISpecifyPrioritySelector<T, TValueSelector>
#pragma warning restore SA1402 // File may only contain a single type
    {
        private readonly IRandomNumberGenerator _rng;

        private readonly Func<T, TValueSelector> _valueSelector;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorBasedRandomPicker{T, T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        /// <param name="elements">The elements.</param>
        /// <param name="valueSelector">The value selector.</param>
        internal SelectorBasedRandomPicker(IRandomNumberGenerator rng, IList<T> elements, Func<T, TValueSelector> valueSelector)
        {
            Elements = elements;
            _valueSelector = valueSelector;
            _rng = rng;
        }

        private IList<T> Elements { get; }

        /// <inheritdoc/>
        public IPick<TValueSelector> WithWeightSelector(Func<T, int> weightSelector)
        {
            return Out.Of<TValueSelector>(_rng).Values(Elements.Select(_valueSelector))
                .WithWeights(Elements.Select(weightSelector));
        }

        /// <inheritdoc/>
        public IPick<TValueSelector> WithPercentageSelector(Func<T, int> percentageSelector)
        {
            return Out.Of<TValueSelector>(_rng).Values(Elements.Select(_valueSelector))
                .WithPercentages(Elements.Select(percentageSelector));
        }
    }
}