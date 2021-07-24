using FluentRandomPicker.Interfaces.General;
using FluentRandomPicker.Interfaces.Percentage;
using FluentRandomPicker.Interfaces.Weight;

namespace FluentRandomPicker.Interfaces
{
    /// <summary>
    /// There are multiple options:
    /// <list type="bullet">
    ///     <item>An additional value can be specified.</item>
    ///     <item>A weight for the previous value can be specified.</item>
    ///     <item>A percentage for the previous value can be specified.</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyValueOrGenesisValuePriority<T> : ISpecifyAdditionalValue<T, ISpecifyValueOrValuePriorityOrPick<T>>,
        ISpecifyPriority<T, ISpecifyPercentageValue<T>, ISpecifyWeightValue<T>>
    {
    }
}
