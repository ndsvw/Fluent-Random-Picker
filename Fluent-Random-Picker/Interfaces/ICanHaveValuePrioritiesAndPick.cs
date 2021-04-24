using System.Collections.Generic;

namespace Fluent_Random_Picker.Interfaces
{
    /// <summary>
    /// An interface to specify that priorities can be chosen optionally
    /// and the Pick methods can be called.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface ICanHaveValuePrioritiesAndPick<T> : ICanPick<T>
    {
        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        ICanPick<T> WithPercentages(IEnumerable<int> ps);

        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        ICanPick<T> WithPercentages(params int[] ps);

        /// <summary>
        /// Specifies the weights of all values.
        /// </summary>
        /// <param name="ws">The weights.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        ICanPick<T> WithWeights(IEnumerable<int> ws);

        /// <summary>
        /// Specifies the weights of all values.
        /// </summary>
        /// <param name="ws">The weights.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        ICanPick<T> WithWeights(params int[] ws);
    }
}
