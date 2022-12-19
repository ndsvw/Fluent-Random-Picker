using System;
using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces.Selectors;

/// <summary>
/// There are multiple options:
/// <list type="bullet">
///     <item>A selector to determine the weigh(s) can be specified.</item>
///     <item>A selector to determine the percentage(s) can be specified.</item>
/// </list>
/// </summary>
/// <typeparam name="T">The type of the element(s).</typeparam>
/// <typeparam name="TValueSelector">The type of the picked value(s).</typeparam>
public interface ISpecifyPrioritySelector<T, TValueSelector>
{
    /// <summary>
    /// Specifies a selector to determine the weight(s).
    /// </summary>
    /// <param name="weightSelector">The weight selector.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<TValueSelector> AndWeightSelector(Func<T, int> weightSelector);

    /// <summary>
    /// Specifies a selector to determine the percentage(s).
    /// </summary>
    /// <param name="percentageSelector">The percentage selector.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<TValueSelector> AndPercentageSelector(Func<T, int> percentageSelector);
}