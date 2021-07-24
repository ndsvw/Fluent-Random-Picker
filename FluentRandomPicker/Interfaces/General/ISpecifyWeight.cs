namespace FluentRandomPicker.Interfaces.General
{
    /// <summary>
    /// A weight for the previous value can be specified.
    /// </summary>
    /// <typeparam name="TPickType">The type of the value(s).</typeparam>
    /// <typeparam name="TReturnType">The return type of the <see cref="ISpecifyWeight{TPickType, TReturnType}.WithWeight(int)"/> method.</typeparam>
    public interface ISpecifyWeight<TPickType, TReturnType> : IFluentChainElement
        where TReturnType : IFluentChainElement
    {
        /// <summary>
        /// Specifies a weight.
        /// </summary>
        /// <param name="w">The weight.</param>
        /// <returns>An <see cref="IFluentChainElement"/> instance that allows specifying
        /// additional information or executing actions via Fluent syntax.</returns>
        TReturnType WithWeight(int w);
    }
}
