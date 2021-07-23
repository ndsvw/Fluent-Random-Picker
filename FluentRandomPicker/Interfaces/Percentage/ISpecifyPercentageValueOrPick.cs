using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces.Percentage
{
    public interface ISpecifyPercentageValueOrPick<T> : ISpecifyAdditionalValue<T, ISpecifyPercentageValueOrValuePercentageOrPick<T>>,
        IPick<T>
    {
    }
}
