namespace FluentRandomPicker.FluentInterfaces.General
{
    /// <summary>
    /// A weight for the previous value can be specified.
    /// </summary>
    /// <typeparam name="T">The return type of the <see cref="ISpecifyWeight{T}.WithWeight(int)"/> method.</typeparam>
    public interface ISpecifyWeight<out T> : IFluentChainElement
        where T : IFluentChainElement
    {
        /// <summary>
        /// Specifies a weight.
        /// </summary>
        /// <param name="w">The weight.</param>
        /// <returns>An <see cref="IFluentChainElement"/> instance that allows specifying
        /// additional information or executing actions via Fluent syntax.</returns>
        T WithWeight(int w);
    }
}
