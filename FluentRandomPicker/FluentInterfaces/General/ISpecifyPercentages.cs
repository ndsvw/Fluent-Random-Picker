using System.Collections.Generic;

namespace FluentRandomPicker.FluentInterfaces.General;

/// <summary>
/// Percentages for the specified values can be specified.
/// </summary>
/// <typeparam name="T">The type of the value(s).</typeparam>
public interface ISpecifyPercentages<T> : IFluentChainElement
{
    /// <summary>
    /// Specifies the percentages of all values.
    /// The order matters. The nth percentage will be linked with the nth value.
    /// </summary>
    /// <param name="ps">The percentages.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithPercentages(IEnumerable<int> ps);

    /// <summary>
    /// Specifies the percentages of all values.
    /// The order matters. The nth percentage will be linked with the nth value.
    /// </summary>
    /// <param name="ps">The percentages.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithPercentages(params int[] ps);

    /// <summary>
    /// Specifies the percentages of all values.
    /// The order matters. The nth percentage will be linked with the nth value.
    /// </summary>
    /// <param name="ps">The percentages.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithPercentages(IEnumerable<int?> ps);

    /// <summary>
    /// Specifies the percentages of all values.
    /// The order matters. The nth percentage will be linked with the nth value.
    /// </summary>
    /// <param name="ps">The percentages.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithPercentages(params int?[] ps);
}