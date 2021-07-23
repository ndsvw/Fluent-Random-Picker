using System.Collections.Generic;

namespace FluentRandomPicker.Interfaces.General
{
    public interface ISpecifyPercentages<T> : IFluentChainElement
    {
        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithPercentages(IEnumerable<int> ps);

        /// <summary>
        /// Specifies the percentages of all values.
        /// </summary>
        /// <param name="ps">The percentages.</param>
        /// <returns>An <see cref="ICanPick{T}"/> instance.</returns>
        IPick<T> WithPercentages(params int[] ps);
    }
}
