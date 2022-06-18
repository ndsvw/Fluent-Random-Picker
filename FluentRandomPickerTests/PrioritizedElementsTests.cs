using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentRandomPicker;
using FluentRandomPicker.Exceptions;

namespace FluentRandomPickerTests
{
	[TestClass]
	public class PrioritizedElementsTests
	{
		private class PrioritizedElement<T>
        {
			public T Value { get; set; }

			public int Priority { get; set; }
        }

		[TestMethod]
		public void EmptyCollection_ThrowsException()
        {
			var arr = new PrioritizedElement<string>[1];

            void Act() => Out.Of().PrioritizedElements(arr);

            Assert.ThrowsException<NotEnoughValuesToPickException>(Act);
		}

        [TestMethod]
        public void Weight3ValuesWith3DifferentWeights_WeightsMatter()
        {
            var elements = new PrioritizedElement<char>[]
            {
                new PrioritizedElement<char> {Value = 'a', Priority = 7},
                new PrioritizedElement<char> {Value = 'b', Priority = 2},
                new PrioritizedElement<char> {Value = 'c', Priority = 1},
            };

            var pickable = Out.Of()
                .PrioritizedElements(elements)
                .WithValueSelector(x => x.Value)
                .AndWeightSelector(x => x.Priority);

            var valueChancesPairs = new[] { ('a', 0.7), ('b', 0.2), ('c', 0.1) };

            Assert.That.ProbabilitiesMatter(pickable, valueChancesPairs: valueChancesPairs);
        }

        [TestMethod]
        public void Percentage3ValuesWith3DifferentPercentages_PercentagesMatter()
        {
            var elements = new PrioritizedElement<char>[]
            {
                new PrioritizedElement<char> {Value = 'a', Priority = 70},
                new PrioritizedElement<char> {Value = 'b', Priority = 20},
                new PrioritizedElement<char> {Value = 'c', Priority = 10},
            };

            var pickable = Out.Of()
                .PrioritizedElements(elements)
                .WithValueSelector(x => x.Value)
                .AndPercentageSelector(x => x.Priority);

            var valueChancesPairs = new[] { ('a', 0.7), ('b', 0.2), ('c', 0.1) };

            Assert.That.ProbabilitiesMatter(pickable, valueChancesPairs: valueChancesPairs);
        }
    }
}

