using FluentRandomPicker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPicker.Tests
{
    [TestClass]
    public class NoPrioritiesTests
    {
        [TestMethod]
        public void NoPrioritiesMinimalExample_ResultMustBeOneOfThe2Values()
        {
            var possibleValues = new[] { 2, 3 };
            var value = Out.Of().Value(2).AndValue(3).PickOne();

            CollectionAssert.Contains(possibleValues, value);
        }

        [TestMethod]
        public void NoPrioritiesMinimalExample_BothValuesArePossible()
        {
            const int NumberOfTries = 1_000_000;
            var aCounter = 0;
            var bCounter = 0;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = Out.Of().Value('a').AndValue('b').PickOne();
                if (value == 'a') aCounter++;
                else if (value == 'b') bCounter++;
            }

            Assert.IsTrue(aCounter > 0);
            Assert.IsTrue(bCounter > 0);
        }

        [TestMethod]
        public void NoPrioritiesMinimalExample_NoOtherValuesArePossible()
        {
            const int NumberOfTries = 1_000_000;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = Out.Of().Value('a').AndValue('b').PickOne();
                Assert.IsTrue(value == 'a' || value == 'b');
            }
        }

        [TestMethod]
        public void NoPrioritiesLargerExample_ResultMustBeOneOfTheValues()
        {
            var possibleValues = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var value = Out.Of()
                .Value(2)
                .AndValue(3)
                .AndValue(4)
                .AndValue(5)
                .AndValue(6)
                .AndValue(7)
                .AndValue(8)
                .AndValue(9)
                .AndValue(10)
                .AndValue(11)
                .PickOne();

            CollectionAssert.Contains(possibleValues, value);
        }

        [TestMethod]
        public void NoPrioritiesMinimalExample_ChainPartCanBeReusedAndCanReturnAllPossibleResults()
        {
            const int NumberOfTries = 1_000_000;
            var aCounter = 0;
            var bCounter = 0;
            var chain = Out.Of()
                .Value('a')
                .AndValue('b');

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = chain.PickOne();
                if (value == 'a') aCounter++;
                else if (value == 'b') bCounter++;
            }

            Assert.IsTrue(aCounter > 0);
            Assert.IsTrue(bCounter > 0);
        }

        [TestMethod]
        public void NoPrioritiesSamePrimitiveValues_ResultIsThatPrimitiveValue()
        {
            var value = Out.Of().Value(1).AndValue(1).PickOne();

            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void NoPrioritiesSameReferenceValues_ResultIsThatReferenceValue()
        {
            var o = new object();
            var value = Out.Of().Value(o).AndValue(o).PickOne();

            Assert.AreEqual(o, value);
        }
    }
}
