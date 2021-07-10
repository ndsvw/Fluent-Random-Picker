using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Exceptions;
using FluentRandomPicker.Interfaces;
using FluentRandomPicker.Interfaces.Percentage;
using FluentRandomPicker.Interfaces.Weight;
using FluentRandomPicker.Picker;
using FluentRandomPicker.Random;

namespace FluentRandomPicker
{
    /// <summary>
    /// The main class.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    internal sealed class RandomPicker<T> : ICanHaveValuePriorityAndNeed1MoreValue<T>,
        ICanHaveValuePrioritiesAndPick<T>,
        ICanHaveAdditionalValueAndPick<T>,

        INeed1MorePercentageValue<T>,
        INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T>,
        ICanHaveAdditionalPercentageValueAndCanPick<T>,

        INeed1MoreWeightValue<T>,
        INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T>,
        ICanHaveAdditionalWeightValueAndCanPick<T>
    {
        private readonly IRandomNumberGenerator _rng;

        private readonly List<T> _values = new List<T>();

        private readonly List<int> _priorities = new List<int>();

        private Priority _priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomPicker{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        internal RandomPicker(IRandomNumberGenerator pRng)
        {
            _rng = pRng;
        }

        private void AddValue(T t)
        {
            _values.Add(t);
        }

        private void SetPriority(int pNumericPriority, Priority pType)
        {
            if (pNumericPriority <= 0)
                throw new ArgumentException("Value priorities must be larger than 0.", nameof(pNumericPriority));

            _priorities.Add(pNumericPriority);
            _priority = pType;
        }

        private void SetPriorities(IEnumerable<int> pNumericPriorities, Priority pType)
        {
            if (pNumericPriorities.Count() != _values.Count)
                throw new NumberOfValuesDoesNotMatchNumberOfPrioritiesException();

            if (pNumericPriorities.Any(p => p <= 0))
                throw new ArgumentException("Value priorities must be larger than 0.", nameof(pNumericPriorities));

            _priorities.AddRange(pNumericPriorities);

            _priority = pType;
        }

        // General

        /// <summary>
        /// Specifies the first value.
        /// </summary>
        /// <param name="t">The first value.</param>
        /// <returns>An <see cref="ICanHaveValuePrioritiesAndPick{T}"/> instance.</returns>
        public ICanHaveValuePriorityAndNeed1MoreValue<T> Value(T t)
        {
            AddValue(t);
            return this;
        }

        /// <summary>
        /// Specifies all value.
        /// </summary>
        /// <param name="ts">The values.</param>
        /// <returns>An <see cref="ICanHaveValuePrioritiesAndPick{T}"/> instance.</returns>
        public ICanHaveValuePrioritiesAndPick<T> Values(IEnumerable<T> ts)
        {
            if (ts.Count() <= 1)
                throw new NotEnoughValuesToPickException();

            foreach (var t in ts)
            {
                AddValue(t);
            }

            return this;
        }

        /// <inheritdoc/>
        public INeed1MorePercentageValue<T> WithPercentage(int p)
        {
            SetPriority(p, Priority.Percentage);
            return this;
        }

        /// <inheritdoc/>
        public ICanPick<T> WithPercentages(IEnumerable<int> ps)
        {
            SetPriorities(ps, Priority.Percentage);
            return this;
        }

        /// <inheritdoc/>
        public ICanPick<T> WithPercentages(params int[] ps)
        {
            SetPriorities(ps, Priority.Percentage);
            return this;
        }

        /// <inheritdoc/>
        public ICanPick<T> WithWeights(IEnumerable<int> ws)
        {
            SetPriorities(ws, Priority.Weight);
            return this;
        }

        /// <inheritdoc/>
        public ICanPick<T> WithWeights(params int[] ws)
        {
            SetPriorities(ws, Priority.Weight);
            return this;
        }

        /// <inheritdoc/>
        public INeed1MoreWeightValue<T> WithWeight(int w)
        {
            SetPriority(w, Priority.Weight);
            return this;
        }

        /// <inheritdoc/>
        public ICanHaveAdditionalValueAndPick<T> AndValue(T t)
        {
            AddValue(t);
            return this;
        }

        // Percentage

        /// <inheritdoc/>
        INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T> INeed1MorePercentageValue<T>.AndValue(T t)
        {
            AddValue(t);
            return this;
        }

        /// <inheritdoc/>
        ICanHaveAdditionalPercentageValueAndCanPick<T> INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T>.WithPercentage(int p)
        {
            SetPriority(p, Priority.Percentage);
            return this;
        }

        /// <inheritdoc/>
        INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T> ICanHaveAdditionalPercentageValueAndCanPick<T>.AndValue(T t)
        {
            AddValue(t);
            return this;
        }

        // Weight

        /// <inheritdoc/>
        INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T> INeed1MoreWeightValue<T>.AndValue(T t)
        {
            AddValue(t);
            return this;
        }

        /// <inheritdoc/>
        ICanHaveAdditionalWeightValueAndCanPick<T> INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T>.WithWeight(int w)
        {
            SetPriority(w, Priority.Weight);
            return this;
        }

        /// <inheritdoc/>
        INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T> ICanHaveAdditionalWeightValueAndCanPick<T>.AndValue(T t)
        {
            AddValue(t);
            return this;
        }

        // Pick

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
            var valuePriorityPairs = new ValuePriorityPairs<T>();
            valuePriorityPairs.Priority = _priority;
            var priorityEnumerator = _priorities.GetEnumerator();
            foreach (var value in _values)
            {
                priorityEnumerator.MoveNext();
                valuePriorityPairs.Add(new ValuePriorityPair<T>(value, priorityEnumerator.Current));
            }

            return valuePriorityPairs;
        }
    }
}
