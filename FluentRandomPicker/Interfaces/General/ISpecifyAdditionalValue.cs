namespace FluentRandomPicker.Interfaces.General
{
    public interface ISpecifyAdditionalValue<TPickType, TReturnType> : IFluentChainElement
        where TReturnType : IFluentChainElement
    {
        TReturnType AndValue(TPickType value);
    }
}
