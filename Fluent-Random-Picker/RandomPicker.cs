using System;
using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker.Exceptions;
using Fluent_Random_Picker.Interfaces;
using Fluent_Random_Picker.Interfaces.Percentage;
using Fluent_Random_Picker.Interfaces.Weight;
using Fluent_Random_Picker.Picker;
using Fluent_Random_Picker.Random;

namespace Fluent_Random_Picker
{
    /// <summary>
    /// The main class.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    public sealed class RandomPicker<T> : ICanHaveValuePriorityAndNeed1MoreValue<T>,
        ICanHaveValuePrioritiesAndPick<T>,
        ICanHaveAdditionalValueAndPick<T>,

        INeed1MorePercentageValue<T>,
        INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T>,
        ICanHaveAdditionalPercentageValueAndCanPick<T>,

        INeed1MoreWeightValue<T>,
        INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T>,
        ICanHaveAdditionalWeightValueAndCanPick<T>
    {
        private readonly IRandomNumberGenerator m_Rng;

        private readonly List<T> m_Values = new List<T>();

        private readonly List<int> m_Priorities = new List<int>();

        private Priority m_Priority;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomPicker{T}"/> class.
        /// </summary>
        /// <param name="pRng">The random number generator.</param>
        internal RandomPicker(IRandomNumberGenerator pRng)
        {
            m_Rng = pRng;
        }

        private void AddValue(T t)
        {
            m_Values.Add(t);
        }

        private void SetPriority(int pNumericPriority, Priority pType)
        {
            if (pNumericPriority <= 0)
                throw new ArgumentException("Value priorities must be larger than 0.", nameof(pNumericPriority));

            m_Priorities.Add(pNumericPriority);
            m_Priority = pType;
        }

        private void SetPriorities(IEnumerable<int> pNumericPriorities, Priority pType)
        {
            if (pNumericPriorities.Count() != m_Values.Count)
                throw new NumberOfValuesDoesNotMatchNumberOfPrioritiesException();

            if (pNumericPriorities.Any(p => p <= 0))
                throw new ArgumentException("Value priorities must be larger than 0.", nameof(pNumericPriorities));

            m_Priorities.AddRange(pNumericPriorities);

            m_Priority = pType;
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
            return new DefaultPicker<T>(m_Rng, GeneratePairs(), n).Pick().Result;
        }

        /// <inheritdoc/>
        public T PickOne()
        {
            return new DefaultPicker<T>(m_Rng, GeneratePairs()).Pick().Result.First();
        }

        /// <inheritdoc/>
        public IEnumerable<T> PickDistinct(int n)
        {
            return new DistinctPicker<T>(m_Rng, GeneratePairs(), n).Pick().Result;
        }

        // More
        private ValuePriorityPairs<T> GeneratePairs()
        {
            var valuePriorityPairs = new ValuePriorityPairs<T>();
            valuePriorityPairs.Priority = m_Priority;
            var priorityEnumerator = m_Priorities.GetEnumerator();
            foreach (var value in m_Values)
            {
                priorityEnumerator.MoveNext();
                valuePriorityPairs.Add(new ValuePriorityPair<T>(value, priorityEnumerator.Current));
            }

            return valuePriorityPairs;
        }
    }
}
