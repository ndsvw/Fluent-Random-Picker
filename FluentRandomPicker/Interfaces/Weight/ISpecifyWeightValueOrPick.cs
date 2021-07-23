using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces.Weight
{
    public interface ISpecifyWeightValueOrPick<T> : ISpecifyAdditionalValue<T, ISpecifyWeightValueOrValueWeightOrPick<T>>,
        IPick<T>
    {
    }
}
