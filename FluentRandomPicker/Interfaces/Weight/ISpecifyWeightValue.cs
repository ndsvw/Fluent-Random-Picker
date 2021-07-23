using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces.Weight
{
    public interface ISpecifyWeightValue<T> : ISpecifyAdditionalValue<T, ISpecifyWeightValueOrValueWeightOrPick<T>>
    {
    }
}
