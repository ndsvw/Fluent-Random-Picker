using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces
{
    public interface ISpecifyValuePrioritiesOrPick<T> : ISpecifyPercentages<T>, ISpecifyWeights<T>, IPick<T>
    {
    }
}
