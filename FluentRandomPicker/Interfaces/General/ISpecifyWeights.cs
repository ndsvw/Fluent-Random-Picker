using System.Collections.Generic;

namespace FluentRandomPicker.Interfaces.General
{
    public interface ISpecifyWeights<T> : IFluentChainElement
    {
        /// <summary>
        /// Specifies the weights of all values.
        /// </summary>
        /// <param name="ws">The weights.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithWeights(IEnumerable<int> ws);

        /// <summary>
        /// Specifies the weights of all values.
        /// </summary>
        /// <param name="ws">The weights.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithWeights(params int[] ws);

        /// <summary>
        /// Specifies the weights of all values.
        /// </summary>
        /// <param name="ws">The weights.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithWeights(IEnumerable<int?> ws);

        /// <summary>
        /// Specifies the weights of all values.
        /// </summary>
        /// <param name="ws">The weights.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithWeights(params int?[] ws);
    }
}
