namespace Fluent_Random_Picker.Interfaces.Percentage
{
    /// <summary>
    /// An interface to specify that an additional percentage value is required.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface INeed1MorePercentageValue<T>
    {
        /// <summary>
        /// Adds an additional value to the values to pick from.
        /// </summary>
        /// <param name="t">The value.</param>
        /// <returns>An <see cref="INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick{T}"/> instance.</returns>
        INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T> AndValue(T t);
    }
}
