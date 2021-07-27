using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces.Weight
{
    /// <summary>
    /// There are multiple options:
    /// <list type="bullet">
    ///     <item>An additional weight value can be specified.</item>
    ///     <item>A weight for the previous value can be specified.</item>
    ///     <item>Methods can be called to pick one ore multiple of the specified values.</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyWeightValueOrValueWeightOrPick<T> : ISpecifyAdditionalValue<T, ISpecifyWeightValueOrValueWeightOrPick<T>>,
        ISpecifyWeight<T, ISpecifyWeightValueOrPick<T>>,
        IPick<T>
    {
    }
}
