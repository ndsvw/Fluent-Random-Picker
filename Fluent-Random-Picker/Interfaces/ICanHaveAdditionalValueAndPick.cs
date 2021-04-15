namespace Fluent_Random_Picker.Interfaces
{
    /// <summary>
    /// An interface to specify that more values can be added
    /// or the Pick methods can be called.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface ICanHaveAdditionalValueAndPick<T> : ICanPick<T>
    {
        /// <summary>
        /// Adds an additional value to the values to pick from.
        /// </summary>
        /// <param name="t">The value.</param>
        /// <returns>An <see cref="ICanHaveAdditionalValueAndPick{T}"/> instance.</returns>
        ICanHaveAdditionalValueAndPick<T> AndValue(T t);
    }
}
