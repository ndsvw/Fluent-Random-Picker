using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces.Percentage
{
    /// <summary>
    /// There are multiple options:
    /// <list type="bullet">
    ///     <item>An additional percentage value can be specified.</item>
    ///     <item>Methods can be called to pick one ore multiple of the specified values.</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyPercentageValueOrPick<T> : ISpecifyAdditionalValue<T, ISpecifyPercentageValueOrValuePercentageOrPick<T>>,
        IPick<T>
    {
    }
}
