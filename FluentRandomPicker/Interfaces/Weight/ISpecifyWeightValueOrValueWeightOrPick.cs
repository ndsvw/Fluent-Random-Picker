using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces.Weight
{
    public interface ISpecifyWeightValueOrValueWeightOrPick<T> : ISpecifyAdditionalValue<T, ISpecifyWeightValueOrValueWeightOrPick<T>>,
        ISpecifyWeight<T, ISpecifyWeightValueOrPick<T>>,
        IPick<T>
    {
    }
}
