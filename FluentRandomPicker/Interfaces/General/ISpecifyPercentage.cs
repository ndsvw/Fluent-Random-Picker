namespace FluentRandomPicker.Interfaces.General
{
    public interface ISpecifyPercentage<TPickType, TReturnType> : IFluentChainElement
        where TReturnType : IFluentChainElement
    {
        TReturnType WithPercentage(int p);
    }
}
