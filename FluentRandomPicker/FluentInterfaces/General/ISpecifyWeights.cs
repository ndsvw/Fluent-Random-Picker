using System.Collections.Generic;

namespace FluentRandomPicker.FluentInterfaces.General;

/// <summary>
/// Weights for the specified values can be specified.
/// </summary>
/// <typeparam name="T">The type of the value(s).</typeparam>
public interface ISpecifyWeights<T> : IFluentChainElement
{
    /// <summary>
    /// Specifies the weights of all values.
    /// The order matters. The nth weight will be linked with the nth value.
    /// </summary>
    /// <param name="ws">The weights.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithWeights(IEnumerable<int> ws);

    /// <summary>
    /// Specifies the weights of all values.
    /// The order matters. The nth weight will be linked with the nth value.
    /// </summary>
    /// <param name="ws">The weights.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithWeights(params int[] ws);

    /// <summary>
    /// Specifies the weights of all values.
    /// The order matters. The nth weight will be linked with the nth value.
    /// </summary>
    /// <param name="ws">The weights.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithWeights(IEnumerable<int?> ws);

    /// <summary>
    /// Specifies the weights of all values.
    /// The order matters. The nth weight will be linked with the nth value.
    /// </summary>
    /// <param name="ws">The weights.</param>
    /// <returns>An <see cref="IPick{T}"/> instance to pick one or multiple values.</returns>
    IPick<T> WithWeights(params int?[] ws);
}