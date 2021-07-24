using System;
using FluentRandomPicker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPickerTests
{
    [TestClass]
    public class PercentageTests
    {
        [TestMethod]
        public void PercentageMinimalExample_ResultMustBeOneOfThe2Values()
        {
            var possibleValues = new[] { 2, 3 };
            var value = Out.Of()
                .Value(2).WithPercentage(30)
                .AndValue(3).WithPercentage(70)
                .PickOne();

            CollectionAssert.Contains(possibleValues, value);
        }

        [TestMethod]
        public void PercentageMinimalExample_BothValuesArePossible()
        {
            const int NumberOfTries = 1_000_000;
            var aCounter = 0;
            var bCounter = 0;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = Out.Of()
                    .Value('a').WithPercentage(80)
                    .AndValue('b').WithPercentage(20)
                    .PickOne();
                if (value == 'a') aCounter++;
                else if (value == 'b') bCounter++;
            }

            Assert.IsTrue(aCounter > 0);
            Assert.IsTrue(bCounter > 0);
        }

        [TestMethod]
        public void PercentageMinimalExample_NoOtherValuesArePossible()
        {
            const int NumberOfTries = 1_000_000;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = Out.Of()
                    .Value('a').WithPercentage(80)
                    .AndValue('b').WithPercentage(20)
                    .PickOne();
                Assert.IsTrue(value == 'a' || value == 'b');
            }
        }

        [TestMethod]
        public void PercentageLargerExample_ResultMustBeOneOfTheValues()
        {
            var possibleValues = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var value = Out.Of()
                .Value(2).WithPercentage(50)
                .AndValue(3).WithPercentage(5)
                .AndValue(4).WithPercentage(5)
                .AndValue(5).WithPercentage(5)
                .AndValue(6).WithPercentage(5)
                .AndValue(7).WithPercentage(5)
                .AndValue(8).WithPercentage(5)
                .AndValue(9).WithPercentage(5)
                .AndValue(10).WithPercentage(5)
                .AndValue(11).WithPercentage(10)
                .PickOne();

            CollectionAssert.Contains(possibleValues, value);
        }

        [TestMethod]
        public void PercentageMinimalExample_ChainPartCanBeReusedAndCanReturnAllPossibleResults()
        {
            const int NumberOfTries = 1_000_000;
            var aCounter = 0;
            var bCounter = 0;
            var chain = Out.Of()
                .Value('a').WithPercentage(80)
                .AndValue('b').WithPercentage(20);

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
        public void Percentage3ValuesWith3DifferentProbabilities_ProbabilitiesMatter()
        {
            var pickable = Out.Of()
                .Value('a').WithPercentage(70)
                .AndValue('b').WithPercentage(20)
                .AndValue('c').WithPercentage(10);

            var valueChancesPairs = new[] { ('a', 0.7), ('b', 0.2), ('c', 0.1) };

            Assert.That.ProbabilitiesMatter(pickable, valueChancesPairs: valueChancesPairs);
        }

        [TestMethod]
        public void PercentageProbabilitiesSumUpToMoreThan100_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithPercentage(85)
                    .AndValue('b').WithPercentage(25)
                    .AndValue('c').WithPercentage(15)
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        [TestMethod]
        public void PercentageProbabilityIsNegative_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithPercentage(85)
                    .AndValue('b').WithPercentage(-25)
                    .AndValue('c').WithPercentage(15)
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        [TestMethod]
        public void PercentageProbabilityIsZero_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithPercentage(85)
                    .AndValue('b').WithPercentage(0)
                    .AndValue('c').WithPercentage(15)
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        [TestMethod]
        public void PercentagesAProbabilityIsNegative_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Values(new[] { 'a', 'b', 'c', 'd' }).WithPercentages(new[] { -25, 50, 50, 25 })
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        [TestMethod]
        public void PercentagesAProbabilityIsZero_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Values(new[] { 'a', 'b', 'c', 'd' }).WithPercentages(new[] { 0, 50, 50, 25 })
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }
    }
}
