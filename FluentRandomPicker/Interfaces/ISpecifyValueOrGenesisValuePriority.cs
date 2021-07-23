using FluentRandomPicker.Interfaces.General;
using FluentRandomPicker.Interfaces.Percentage;
using FluentRandomPicker.Interfaces.Weight;

namespace FluentRandomPicker.Interfaces
{
    public interface ISpecifyValueOrGenesisValuePriority<T> : ISpecifyAdditionalValue<T, ISpecifyValueOrValuePriorityOrPick<T>>,
        ISpecifyPriority<T, ISpecifyPercentageValue<T>, ISpecifyWeightValue<T>>
    {
    }
}
