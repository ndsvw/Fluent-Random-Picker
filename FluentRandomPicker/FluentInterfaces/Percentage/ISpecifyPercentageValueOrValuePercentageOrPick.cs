﻿using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces.Percentage;

/// <summary>
/// There are multiple options:
/// <list type="bullet">
///     <item>An additional percentage value can be specified.</item>
///     <item>A percentage for the previous value can be specified.</item>
///     <item>Methods can be called to pick one ore multiple of the specified values.</item>
/// </list>
/// </summary>
/// <typeparam name="T">The type of the value(s).</typeparam>
public interface ISpecifyPercentageValueOrValuePercentageOrPick<T> : ISpecifyAdditionalValue<T, ISpecifyPercentageValueOrValuePercentageOrPick<T>>,
    ISpecifyPercentage<ISpecifyPercentageValueOrPick<T>>,
    IPick<T>
{
}