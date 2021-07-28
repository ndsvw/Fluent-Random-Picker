namespace FluentRandomPicker.FluentInterfaces.General
{
    /// <summary>
    /// A percentage for the previous value can be specified.
    /// </summary>
    /// <typeparam name="T">The return type of the <see cref="ISpecifyPercentage{T}.WithPercentage(int)"/> method.</typeparam>
    public interface ISpecifyPercentage<T> : IFluentChainElement
        where T : IFluentChainElement
    {
        /// <summary>
        /// Specifies a percentage.
        /// </summary>
        /// <param name="p">The percentage.</param>
        /// <returns>An <see cref="IFluentChainElement"/> instance that allows specifying
        /// additional information or executing actions via Fluent syntax.</returns>
        T WithPercentage(int p);
    }
}
