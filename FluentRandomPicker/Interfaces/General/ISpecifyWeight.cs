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
        TReturnType WithWeight(int w);
    }
}
