using FluentRandomPicker.Interfaces.General;

namespace FluentRandomPicker.Interfaces
{
    /// <summary>
    /// There are multiple options:
    /// <list type="bullet">
    ///     <item>An additional value can be specified.</item>
    ///     <item>A weight for the previous value can be specified.</item>
    ///     <item>A percentage for the previous value can be specified.</item>
    ///     <item>Methods can be called to pick one ore multiple of the specified values.</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyValueOrValuePriorityOrPick<T> : ISpecifyValueOrValuePriority<T>, IPick<T>
    {
    }
}
