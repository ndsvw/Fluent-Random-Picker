using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.FluentInterfaces;
using FluentRandomPicker.FluentInterfaces.General;
using FluentRandomPicker.FluentInterfaces.Percentage;
using FluentRandomPicker.FluentInterfaces.Weight;
using FluentRandomPicker.Picker;
using FluentRandomPicker.Random;
using FluentRandomPicker.ValuePriorities;

namespace FluentRandomPicker
{
    /// <summary>
    /// The starting point of the fluent syntax after Out.Of().
    /// It implements all relevant interfaces to allow chaining
    /// values, specifying priorities, picking, ...
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    internal sealed class RandomPicker<T> : ISpecifyValueOrGenesisValuePriority<T>,
        ISpecifyValueOrValuePriorityOrPick<T>,
        /* ISpecifyValueOrValuePriority<T>, redundant because of ISpecifyValueOrValuePriorityOrPick<T> */
        ISpecifyValuePrioritiesOrPick<T>,

        ISpecifyPercentageValue<T>,
        ISpecifyPercentageValueOrPick<T>,
        ISpecifyPercentageValueOrValuePercentageOrPick<T>,

        ISpecifyWeightValue<T>,
        ISpecifyWeightValueOrPick<T>,
        ISpecifyWeightValueOrValueWeightOrPick<T>
    {
        private readonly IRandomNumberGenerator _rng;

        private readonly List<T> _values = new List<T>();

        private readonly List<int?> _priorities = new List<int?>();

        private PriorityType _priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomPicker{T}"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        internal RandomPicker(IRandomNumberGenerator rng)
        {
            _rng = rng;
        }

        private void AddValue(T t)
        {
            _values.Add(t);
        }

        private void SetPriority(int numericPriority, PriorityType type)
        {
            if (numericPriority <= 0)
                throw new ArgumentException("Value priorities must be larger than 0.", nameof(numericPriority));

            var additionalDefaultEntries = _values.Count - _priorities.Count - 1;
            if (additionalDefaultEntries > 0)
                _priorities.AddRange(Enumerable.Repeat(default(int?), additionalDefaultEntries));

            _priorities.Add(numericPriority);
            _priority = type;
        }

        private void SetPriorities(IEnumerable<int?> numericPriorities, PriorityType type)
        {
            if (numericPriorities.Count() != _values.Count)
                throw new NumberOfValuesDoesNotMatchNumberOfPrioritiesException();

            if (numericPriorities.Any(p => p <= 0))
                throw new ArgumentException("Value priorities must be larger than 0.", nameof(numericPriorities));

            _priorities.AddRange(numericPriorities);

            _priority = type;
        }

        // Adding initial value(s)

        /// <summary>
        /// Specifies the first value.
        /// </summary>
        /// <param name="t">The first value.</param>
        /// <returns>An <see cref="ISpecifyValueOrGenesisValuePriority{T}"/> instance.</returns>
        public ISpecifyValueOrGenesisValuePriority<T> Value(T t)
        {
            AddValue(t);
            return this;
        }

        /// <summary>
        /// Specifies all value.
        /// </summary>
        /// <param name="ts">The values.</param>
        /// <returns>An <see cref="ISpecifyValuePrioritiesOrPick{T}"/> instance.</returns>
        public ISpecifyValuePrioritiesOrPick<T> Values(IEnumerable<T> ts)
        {
            if (ts.Count() <= 1)
                throw new NotEnoughValuesToPickException();

            foreach (var t in ts)
            {
                AddValue(t);
            }

            return this;
        }

        // Adding additional value(s)

        /// <inheritdoc/>
        public ISpecifyValueOrValuePriorityOrPick<T> AndValue(T value)
        {
            AddValue(value);
            return this;
        }

        /// <inheritdoc/>
        ISpecifyPercentageValueOrValuePercentageOrPick<T> ISpecifyAdditionalValue<T, ISpecifyPercentageValueOrValuePercentageOrPick<T>>.AndValue(T value)
        {
            AddValue(value);
            return this;
        }

        /// <inheritdoc/>
        ISpecifyWeightValueOrValueWeightOrPick<T> ISpecifyAdditionalValue<T, ISpecifyWeightValueOrValueWeightOrPick<T>>.AndValue(T value)
        {
            AddValue(value);
            return this;
        }

        // Specifying percentage(s)

        /// <inheritdoc/>
        public ISpecifyPercentageValue<T> WithPercentage(int p)
        {
            SetPriority(p, PriorityType.Percentage);
            return this;
        }

        /// <inheritdoc/>
        ISpecifyPercentageValueOrPick<T> ISpecifyPercentage<ISpecifyPercentageValueOrPick<T>>.WithPercentage(int p)
        {
            SetPriority(p, PriorityType.Percentage);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithPercentages(IEnumerable<int> ps)
        {
            SetPriorities(ps.Cast<int?>(), PriorityType.Percentage);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithPercentages(params int[] ps)
        {
            SetPriorities(ps.Cast<int?>(), PriorityType.Percentage);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithPercentages(IEnumerable<int?> ps)
        {
            SetPriorities(ps, PriorityType.Percentage);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithPercentages(params int?[] ps)
        {
            SetPriorities(ps, PriorityType.Percentage);
            return this;
        }

        // Specifying weight(s)

        /// <inheritdoc/>
        public ISpecifyWeightValue<T> WithWeight(int w)
        {
            SetPriority(w, PriorityType.Weight);
            return this;
        }

        /// <inheritdoc/>
        ISpecifyWeightValueOrPick<T> ISpecifyWeight<ISpecifyWeightValueOrPick<T>>.WithWeight(int w)
        {
            SetPriority(w, PriorityType.Weight);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithWeights(IEnumerable<int> ws)
        {
            SetPriorities(ws.Cast<int?>(), PriorityType.Weight);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithWeights(params int[] ws)
        {
            SetPriorities(ws.Cast<int?>(), PriorityType.Weight);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithWeights(IEnumerable<int?> ws)
        {
            SetPriorities(ws, PriorityType.Weight);
            return this;
        }

        /// <inheritdoc/>
        public IPick<T> WithWeights(params int?[] ws)
        {
            SetPriorities(ws, PriorityType.Weight);
            return this;
        }

        // Picking

        /// <inheritdoc/>
        public IEnumerable<T> Pick(int n)
        {
            return new DefaultPicker<T>(_rng, GeneratePairs(), n).Pick().Result;
        }

        /// <inheritdoc/>
        public T PickOne()
        {
            return new DefaultPicker<T>(_rng, GeneratePairs()).Pick().Result.First();
        }

        /// <inheritdoc/>
        public IEnumerable<T> PickDistinct(int n)
        {
            return new DistinctPicker<T>(_rng, GeneratePairs(), n).Pick().Result;
        }

        // More
        private ValuePriorityPairs<T> GeneratePairs()
        {
            var factory = new ValuePriorityPairsGeneratorFactory<T>();
            var pairGenerator = factory.Create(_priority);
            return pairGenerator.Generate(_values, _priorities);
        }
    }
}
