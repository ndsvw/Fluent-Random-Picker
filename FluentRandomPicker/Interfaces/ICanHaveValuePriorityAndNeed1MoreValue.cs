using FluentRandomPicker.Interfaces.Percentage;
using FluentRandomPicker.Interfaces.Weight;

namespace FluentRandomPicker.Interfaces
{
    /// <summary>
    /// An interface to specify that the previous value can get a priority
    /// or more values can be added (all without priorities).
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface ICanHaveValuePriorityAndNeed1MoreValue<T>
    {
        /// <summary>
        /// Specifies the percentage of the most recent value.
        /// </summary>
        /// <param name="p">The percentage.</param>
        /// <returns>An <see cref="INeed1MorePercentageValue{T}"/> instance.</returns>
        INeed1MorePercentageValue<T> WithPercentage(int p);

        /// <summary>
        /// Specifies the weight of the most recent value.
        /// </summary>
        /// <param name="w">The weight.</param>
        /// <returns>An <see cref="INeed1MoreWeightValue{T}"/> instance.</returns>
        INeed1MoreWeightValue<T> WithWeight(int w);

        /// <summary>
        /// Adds an additional value to the values to pick from.
        /// </summary>
        /// <param name="t">The value.</param>
        /// <returns>An <see cref="ICanHaveAdditionalValueAndPick{T}"/> instance.</returns>
        ICanHaveAdditionalValueAndPick<T> AndValue(T t);
    }
}
