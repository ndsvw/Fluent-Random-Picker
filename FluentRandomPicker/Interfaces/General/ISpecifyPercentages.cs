using System.Collections.Generic;

namespace FluentRandomPicker.Interfaces.General
{
    /// <summary>
    /// Percentages for the specified values can be specified.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyPercentages<T> : IFluentChainElement
    {
        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithPercentages(IEnumerable<int> ps);

        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithPercentages(params int[] ps);

        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithPercentages(IEnumerable<int?> ps);

        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithPercentages(params int?[] ps);
    }
}
