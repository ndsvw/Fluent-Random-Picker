using System;
using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker.Interfaces;
using Fluent_Random_Picker.Interfaces.Percentage;
using Fluent_Random_Picker.Interfaces.Weight;
using Fluent_Random_Picker.Picker;

namespace Fluent_Random_Picker
{
    /// <summary>
    /// The main class.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    public sealed class RandomPicker<T> : ICanHaveValuePriorityAndNeed1MoreValue<T>,
        ICanHaveAdditionalValueAndPick<T>,

        INeed1MorePercentageValue<T>,
        INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T>,
        ICanHaveAdditionalPercentageValueAndCanPick<T>,

        INeed1MoreWeightValue<T>,
        INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T>,
        ICanHaveAdditionalWeightValueAndCanPick<T>
    {
        private readonly ValuePriorityPairs<T> m_Pairs = new ValuePriorityPairs<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomPicker{T}"/> class.
        /// </summary>
        internal RandomPicker()
        {
        }

        private void AddValue(T t)
        {
            m_Pairs.Add(new ValuePriorityPair<T> { Value = t });
        }

        private void SetPriority(int pNumericPriority, Priority pType)
        {
            if (pNumericPriority < 0)
                throw new ArgumentException("A negative value priority is not allowed.", nameof(pNumericPriority));

            var mostRecentValueWeightPair = m_Pairs[m_Pairs.Count() - 1];
            mostRecentValueWeightPair.Priority = pNumericPriority;
            m_Pairs.Priority = pType;
        }

        // General

        /// <inheritdoc/>
        public ICanHaveValuePriorityAndNeed1MoreValue<T> Value(T t)
        {
            AddValue(t);
            return this;
        }

        /// <inheritdoc/>
        public INeed1MorePercentageValue<T> WithPercentage(int p)
        {
            SetPriority(p, Priority.Percentage);
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
            return new DefaultPicker<T>(m_Pairs, n).Pick().Result;
        }

        /// <inheritdoc/>
        public T PickOne()
        {
            return new DefaultPicker<T>(m_Pairs).Pick().Result.First();
        }

        /// <inheritdoc/>
        public IEnumerable<T> PickDistinct(int n)
        {
            return new DistinctPicker<T>(m_Pairs, n).Pick().Result;
        }
    }
}
