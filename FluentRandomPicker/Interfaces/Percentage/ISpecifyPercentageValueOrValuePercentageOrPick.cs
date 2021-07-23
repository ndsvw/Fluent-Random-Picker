using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces.Percentage
{
    public interface ISpecifyPercentageValueOrValuePercentageOrPick<T> : ISpecifyAdditionalValue<T, ISpecifyPercentageValueOrValuePercentageOrPick<T>>,
        ISpecifyPercentage<T, ISpecifyPercentageValueOrPick<T>>,
        IPick<T>
    {
    }
}
