using System;
using FluentRandomPicker.FluentInterfaces.General;

namespace FluentRandomPicker.FluentInterfaces.Selectors
{
	public interface ISpecifySelector<T>
	{
		ISpecifyPrioritySelector<T, TReturn> WithValueSelector<TReturn>(Func<T, TReturn> valueSelector);
		IPick<T> WithWeightSelector(Func<T, int> weightSelector);
		IPick<T> WithPercentageSelector(Func<T, int> percentageSelector);
	}
}

