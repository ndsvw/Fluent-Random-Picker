using FluentRandomPicker.Interfaces.General;
using FluentRandomPicker.Interfaces.Percentage;
using FluentRandomPicker.Interfaces.Weight;

namespace FluentRandomPicker.Interfaces
{
    public interface ISpecifyValueOrValuePriority<T> : ISpecifyAdditionalValue<T, ISpecifyValueOrValuePriorityOrPick<T>>,
        ISpecifyPriority<T, ISpecifyPercentageValueOrPick<T>, ISpecifyWeightValueOrPick<T>>
    {
    }
}
