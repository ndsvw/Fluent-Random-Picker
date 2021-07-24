namespace FluentRandomPicker.Interfaces.General
{
    /// <summary>
    /// An additional value can be specified.
    /// </summary>
    /// <typeparam name="TPickType">The type of the value(s).</typeparam>
    /// <typeparam name="TReturnType">The return type of the <see cref="ISpecifyAdditionalValue{TPickType, TReturnType}.AndValue(TPickType)"/> method.</typeparam>
    public interface ISpecifyAdditionalValue<TPickType, TReturnType> : IFluentChainElement
        where TReturnType : IFluentChainElement
    {
        TReturnType AndValue(TPickType value);
    }
}
