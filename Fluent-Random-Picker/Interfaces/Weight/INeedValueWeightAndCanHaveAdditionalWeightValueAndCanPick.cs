namespace Fluent_Random_Picker.Interfaces.Weight
{
    /// <summary>
    /// An interface to specify that a value was permitted and its weight
    /// is required now. Afterwards, the Pick methods can be called.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface INeedValueWeightAndCanHaveAdditionalWeightValueAndCanPick<T>
    {
        /// <summary>
        /// Specifies the weight of the most recent value.
        /// </summary>
        /// <param name="w">The weight.</param>
        /// <returns>An <see cref="ICanHaveAdditionalWeightValueAndCanPick{T}"/> instance.</returns>
        ICanHaveAdditionalWeightValueAndCanPick<T> WithWeight(int w);
    }
}
