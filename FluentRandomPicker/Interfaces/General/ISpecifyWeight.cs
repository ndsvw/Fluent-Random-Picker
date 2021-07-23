namespace FluentRandomPicker.Interfaces.General
{
    public interface ISpecifyWeight<TPickType, TReturnType> : IFluentChainElement
        where TReturnType : IFluentChainElement
    {
        TReturnType WithWeight(int p);
    }
}
