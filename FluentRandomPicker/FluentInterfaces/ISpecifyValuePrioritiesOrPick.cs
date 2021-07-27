using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces
{
    /// <summary>
    /// There are multiple options:
    /// <list type="bullet">
    ///     <item>Weights for the specified values can be specified.</item>
    ///     <item>Percentage for the specified values can be specified.</item>
    ///     <item>Methods can be called to pick one ore multiple of the specified values.</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">The type of the value(s).</typeparam>
    public interface ISpecifyValuePrioritiesOrPick<T> : ISpecifyPercentages<T>, ISpecifyWeights<T>, IPick<T>
    {
    }
}
