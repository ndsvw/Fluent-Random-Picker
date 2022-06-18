using System;
using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces.Selectors
{
	public interface ISpecifyPrioritySelector<T, TValueSelector>
	{
		IPick<TValueSelector> WithWeightSelector(Func<T, int> weightSelector);
		IPick<TValueSelector> WithPercentageSelector(Func<T, int> percentageSelector);
	}
}

