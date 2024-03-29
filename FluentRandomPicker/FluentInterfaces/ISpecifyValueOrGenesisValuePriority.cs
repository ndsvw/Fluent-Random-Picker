﻿using FluentRandomPicker.FluentInterfaces.General;
using FluentRandomPicker.FluentInterfaces.Percentage;
using FluentRandomPicker.FluentInterfaces.Weight;

namespace FluentRandomPicker.FluentInterfaces;

/// <summary>
/// There are multiple options:
/// <list type="bullet">
///     <item>An additional value can be specified.</item>
///     <item>A weight for the first value can be specified.</item>
///     <item>A percentage for the previous value can be specified.</item>
/// </list>
/// </summary>
/// <typeparam name="T">The type of the value(s).</typeparam>
public interface ISpecifyValueOrGenesisValuePriority<T> : ISpecifyAdditionalValue<T, ISpecifyValueOrValuePriorityOrPick<T>>,
    ISpecifyPriority<ISpecifyPercentageValue<T>, ISpecifyWeightValue<T>>
{
}