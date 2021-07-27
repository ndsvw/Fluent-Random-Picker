namespace FluentRandomPicker.FluentInterfaces.General
{
    /// <summary>
    /// An additional value can be specified.
    /// </summary>
    /// <typeparam name="TPickType">The type of the value(s).</typeparam>
    /// <typeparam name="TReturnType">The return type of the <see cref="ISpecifyAdditionalValue{TPickType, TReturnType}.AndValue(TPickType)"/> method.</typeparam>
    public interface ISpecifyAdditionalValue<TPickType, TReturnType> : IFluentChainElement
        where TReturnType : IFluentChainElement
    {
        /// <summary>
        /// Specifies an additional value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>An <see cref="IFluentChainElement"/> instance that allows specifying
        /// additional information or executing actions via Fluent syntax.</returns>
        TReturnType AndValue(TPickType value);
    }
}
