using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces.Percentage
{
    /// <summary>
    /// An additional percentage value can be specified.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyPercentageValue<T> : ISpecifyAdditionalValue<T, ISpecifyPercentageValueOrValuePercentageOrPick<T>>
    {
    }
}
