using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.FluentInterfaces.General;
using FluentRandomPicker.FluentInterfaces.Selectors;
using FluentRandomPicker.Random;

namespace FluentRandomPicker
{
    public class SelectorBasedRandomPicker<T> : ISpecifySelector<T>
    {
        private readonly IRandomNumberGenerator _rng;

        private readonly List<T> _elements = new List<T>();

        internal SelectorBasedRandomPicker(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        public ISpecifySelector<T> PrioritizedElements(IEnumerable<T> elements)
        {
            var values = elements.ToList();
            if (values.Count <= 1)
                throw new NotEnoughValuesToPickException();

            _elements.AddRange(elements);

            return this;
        }

        public ISpecifyPrioritySelector<T, TReturn> WithValueSelector<TReturn>(Func<T, TReturn> valueSelector)
        {
            return new SelectorBasedRandomPicker<T, TReturn>(_rng, _elements, valueSelector);
        }

        public IPick<T> WithWeightSelector(Func<T, int> weightSelector)
        {
            return Out.Of<T>(_rng).Values(_elements)
                .WithWeights(_elements.Select(weightSelector));
        }

        public IPick<T> WithPercentageSelector(Func<T, int> percentageSelector)
        {
            return Out.Of<T>(_rng).Values(_elements)
                .WithPercentages(_elements.Select(percentageSelector));
        }
    }

    public class SelectorBasedRandomPicker<T, TValueSelector> : ISpecifyPrioritySelector<T, TValueSelector>
    {
        private readonly IRandomNumberGenerator _rng;

        private IList<T> Elements { get; }

        private readonly Func<T, TValueSelector> _valueSelector;

        internal SelectorBasedRandomPicker(IRandomNumberGenerator rng, IList<T> elements, Func<T, TValueSelector> valueSelector)
        {
            Elements = elements;
            _valueSelector = valueSelector;
            _rng = rng;
        }

        public IPick<TValueSelector> WithWeightSelector(Func<T, int> weightSelector)
        {
            return Out.Of<TValueSelector>(_rng).Values(Elements.Select(_valueSelector))
                .WithWeights(Elements.Select(weightSelector));
        }

        public IPick<TValueSelector> WithPercentageSelector(Func<T, int> percentageSelector)
        {
            return Out.Of<TValueSelector>(_rng).Values(Elements.Select(_valueSelector))
                .WithPercentages(Elements.Select(percentageSelector));
        }
    }
}