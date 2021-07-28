namespace FluentRandomPicker.FluentInterfaces.General
{
    /// <summary>
    /// A percentage or a weight for the previous value can be specified.
    /// </summary>
    /// <typeparam name="TPercentageReturnType">The return type of the <see cref="ISpecifyPercentage{TReturnType}.WithPercentage(int)"/> method.</typeparam>
    /// <typeparam name="TWeightReturnType">The return type of the <see cref="ISpecifyWeight{TReturnType}.WithWeight(int)"/> method.</typeparam>
    public interface ISpecifyPriority<TPercentageReturnType, TWeightReturnType> : ISpecifyPercentage<TPercentageReturnType>, ISpecifyWeight<TWeightReturnType>
        where TPercentageReturnType : IFluentChainElement
        where TWeightReturnType : IFluentChainElement
    {
    }
}
