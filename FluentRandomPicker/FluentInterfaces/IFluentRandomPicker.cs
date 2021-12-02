using FluentRandomPicker.FluentInterfaces.Percentage;
using FluentRandomPicker.FluentInterfaces.Weight;

namespace FluentRandomPicker.FluentInterfaces
{
    /// <summary>
    /// An interface for a FluentRandomPicker implementation.
    /// It defines all required methods to allow the expected fluency.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IFluentRandomPicker<T> : ISpecifyValueOrGenesisValuePriority<T>,
        ISpecifyValueOrValuePriorityOrPick<T>,
        /* ISpecifyValueOrValuePriority<T>, redundant because of ISpecifyValueOrValuePriorityOrPick<T> */
        ISpecifyValuePrioritiesOrPick<T>,

        ISpecifyPercentageValue<T>,
        ISpecifyPercentageValueOrPick<T>,
        ISpecifyPercentageValueOrValuePercentageOrPick<T>,

        ISpecifyWeightValue<T>,
        ISpecifyWeightValueOrPick<T>,
        ISpecifyWeightValueOrValueWeightOrPick<T>
    {
    }
}