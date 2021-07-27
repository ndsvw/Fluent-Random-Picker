namespace FluentRandomPicker.FluentInterfaces.General
{
    /// <summary>
    /// A percentage for the previous value can be specified.
    /// </summary>
    /// <typeparam name="TPickType">The type of the value(s).</typeparam>
    /// <typeparam name="TReturnType">The return type of the <see cref="ISpecifyPercentage{TPickType, TReturnType}.WithPercentage(int)"/> method.</typeparam>
    public interface ISpecifyPercentage<TPickType, TReturnType> : IFluentChainElement
        where TReturnType : IFluentChainElement
    {
        /// <summary>
        /// Specifies a percentage.
        /// </summary>
        /// <param name="p">The percentage.</param>
        /// <returns>An <see cref="IFluentChainElement"/> instance that allows specifying
        /// additional information or executing actions via Fluent syntax.</returns>
        TReturnType WithPercentage(int p);
    }
}
