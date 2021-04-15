namespace Fluent_Random_Picker.Interfaces.Weight
{
    /// <summary>
    /// An interface to specify that an additional weight value is required.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface INeed1MoreWeightValue<T>
    {
        /// <summary>
        /// Adds an additional value to the values to pick from.
        /// </summary>
        /// <param name="t">The value.</param>
        /// <returns>An <see cref="INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick{T}"/> instance.</returns>
        INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T> AndValue(T t);
    }
}
