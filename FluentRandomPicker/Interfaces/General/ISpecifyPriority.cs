namespace FluentRandomPicker.Interfaces.General
{
    public interface ISpecifyPriority<TPickType, TPercentageReturnType, TWeightReturnType> : ISpecifyPercentage<TPickType, TPercentageReturnType>, ISpecifyWeight<TPickType, TWeightReturnType>
        where TPercentageReturnType : IFluentChainElement
        where TWeightReturnType : IFluentChainElement
    {
    }
}
