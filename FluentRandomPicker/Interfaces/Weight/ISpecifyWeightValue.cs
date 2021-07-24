using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces.Weight
{
    /// <summary>
    /// An additional weight value can be specified.
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyWeightValue<T> : ISpecifyAdditionalValue<T, ISpecifyWeightValueOrValueWeightOrPick<T>>
    {
    }
}
