using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces
{
    public interface ISpecifyValueOrValuePriorityOrPick<T> : ISpecifyValueOrValuePriority<T>, IPick<T>
    {
    }
}
