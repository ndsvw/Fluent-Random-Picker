namespace FluentRandomPicker.Interfaces.General
{
    /// <summary>
    /// A percentage or a weight for the previous value can be specified.
    /// </summary>
    /// <typeparam name="TPickType">The type of the value(s).</typeparam>
    /// <typeparam name="TPercentageReturnType">The return type of the <see cref="ISpecifyPercentage{TPickType, TReturnType}.WithPercentage(int)"/> method.</typeparam>
    /// <typeparam name="TWeightReturnType">The return type of the <see cref="ISpecifyWeight{TPickType, TReturnType}.WithWeight(int)"/> method.</typeparam>
    public interface ISpecifyPriority<TPickType, TPercentageReturnType, TWeightReturnType> : ISpecifyPercentage<TPickType, TPercentageReturnType>, ISpecifyWeight<TPickType, TWeightReturnType>
        where TPercentageReturnType : IFluentChainElement
        where TWeightReturnType : IFluentChainElement
    {
    }
}
