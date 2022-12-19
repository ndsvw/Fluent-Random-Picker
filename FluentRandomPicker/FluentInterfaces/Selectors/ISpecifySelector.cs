using System;
using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces.Selectors;

/// <summary>
/// There are multiple options:
/// <list type="bullet">
///     <item>A selector to determine the actual value(s) can be specified</item>
///     <item>A selector to determine the weigh(s) can be specified.</item>
///     <item>A selector to determine the percentage(s) can be specified.</item>
/// </list>
/// </summary>
/// <typeparam name="T">The type of the element(s).</typeparam>
public interface ISpecifySelector<T>
{
    /// <summary>
    /// Specifies a selector to determine the weight(s).
    /// </summary>
    /// <param name="weightSelector">The weight selector.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithWeightSelector(Func<T, int> weightSelector);

    /// <summary>
    /// Specifies a selector to determine the percentage(s).
    /// </summary>
    /// <param name="percentageSelector">The percentage selector.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithPercentageSelector(Func<T, int> percentageSelector);

    /// <summary>
    /// Specifies a selector to determine the actual value(s).
    /// </summary>
    /// <typeparam name="TReturn">The type of the value(s) that are picked.</typeparam>
    /// <param name="valueSelector">The value selector.</param>
    /// <returns>An <see cref="IFluentChainElement"/> instance that allows specifying
    /// a selector for the weight(s) or for the percentage(s).</returns>
    ISpecifyPrioritySelector<T, TReturn> WithValueSelector<TReturn>(Func<T, TReturn> valueSelector);
}