﻿namespace FluentRandomPicker.Interfaces.Weight
{
    /// <summary>
    /// An interface to specify that an additional weight value can be added
    /// and the Pick methods can be called afterwards.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface ICanHaveAdditionalWeightValueAndCanPick<T> : ICanPick<T>
    {
        /// <summary>
        /// Adds an additional value to the values to pick from.
        /// </summary>
        /// <param name="t">The value.</param>
        /// <returns>An <see cref="INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick{T}"/> instance.</returns>
        INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T> AndValue(T t);
    }
}