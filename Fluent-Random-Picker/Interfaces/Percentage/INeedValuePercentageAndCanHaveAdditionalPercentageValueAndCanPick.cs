namespace Fluent_Random_Picker.Interfaces.Percentage
{
    /// <summary>
    /// An interface to specify that a value was permitted and its percentage
    /// is required now. Afterwards, the Pick methods can be called.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface INeedValuePercentageAndCanHaveAdditionalPercentageValueAndCanPick<T>
    {
        /// <summary>
        /// Specifies the percentage of the most recent value.
        /// </summary>
        /// <param name="p">The percentage.</param>
        /// <returns>An <see cref="ICanHaveAdditionalPercentageValueAndCanPick{T}"/> instance.</returns>
        ICanHaveAdditionalPercentageValueAndCanPick<T> WithPercentage(int p);
    }
}
