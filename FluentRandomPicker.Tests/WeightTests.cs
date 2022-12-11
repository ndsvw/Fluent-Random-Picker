using System;
using System.Linq;
using FluentRandomPicker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPicker.Tests
{
    [TestClass]
    public class WeightTests
    {
        [TestMethod]
        public void WeightMinimalExample_ResultMustBeOneOfThe2Values()
        {
            var possibleValues = new[] { 2, 3 };
            var value = Out.Of()
                .Value(2).WithWeight(15)
                .AndValue(3).WithWeight(19)
                .PickOne();

            CollectionAssert.Contains(possibleValues, value);
        }

        [TestMethod]
        public void WeightMinimalExample_BothValuesArePossible()
        {
            const int NumberOfTries = 1_000_000;
            var aCounter = 0;
            var bCounter = 0;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = Out.Of()
                    .Value('a').WithWeight(5)
                    .AndValue('b').WithWeight(7)
                    .PickOne();
                if (value == 'a') aCounter++;
                else if (value == 'b') bCounter++;
            }

            Assert.IsTrue(aCounter > 0);
            Assert.IsTrue(bCounter > 0);
        }

        [TestMethod]
        public void WeightMinimalExample_NoOtherValuesArePossible()
        {
            const int NumberOfTries = 1_000_000;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = Out.Of()
                    .Value('a').WithWeight(5)
                    .AndValue('b').WithWeight(7)
                    .PickOne();
                Assert.IsTrue(value == 'a' || value == 'b');
            }
        }

        [TestMethod]
        public void WeightLargerExample_ResultMustBeOneOfTheValues()
        {
            var possibleValues = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            var value = Out.Of()
                .Value(2).WithWeight(10)
                .AndValue(3).WithWeight(1)
                .AndValue(4).WithWeight(1)
                .AndValue(5).WithWeight(1)
                .AndValue(6).WithWeight(1)
                .AndValue(7).WithWeight(1)
                .AndValue(8).WithWeight(1)
                .AndValue(9).WithWeight(1)
                .AndValue(10).WithWeight(1)
                .AndValue(11).WithWeight(5)
                .PickOne();

            CollectionAssert.Contains(possibleValues, value);
        }

        [TestMethod]
        public void WeightMinimalExample_ChainPartCanBeReusedAndCanReturnAllPossibleResults()
        {
            const int NumberOfTries = 1_000_000;
            var aCounter = 0;
            var bCounter = 0;
            var chain = Out.Of()
                .Value('a').WithWeight(3)
                .AndValue('b').WithWeight(10);

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
        public void Weight3ValuesWith3DifferentWeights_WeightMatter()
        {
            var pickable = Out.Of()
                .Value('a').WithWeight(7)
                .AndValue('b').WithWeight(2)
                .AndValue('c').WithWeight(1);

            var valueChancesPairs = new[] { ('a', 0.7), ('b', 0.2), ('c', 0.1) };

            Assert.That.ProbabilitiesMatter(pickable, valueChancesPairs: valueChancesPairs);
        }

        [TestMethod]
        public void WeightAmountIsNegative_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithWeight(1)
                    .AndValue('b').WithWeight(-1)
                    .AndValue('c').WithWeight(2)
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        [TestMethod]
        public void WeightAmountIsZero_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithWeight(1)
                    .AndValue('b').WithWeight(0)
                    .AndValue('c').WithWeight(2)
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        [TestMethod]
        public void WeightAmountsOneIsNegative_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Values(new[] { 'a', 'b', 'c', 'd' }).WithWeights(new[] { 2, 2, -1, 1 })
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        [TestMethod]
        public void WeightAmountsOneIsZero_ExceptionIsThrown()
        {
            static void Execute()
            {
                Out.Of()
                    .Values(new[] { 'a', 'b', 'c', 'd' }).WithWeights(new[] { 2, 2, 0, 1 })
                    .PickOne();
            }
            Assert.ThrowsException<ArgumentException>(Execute);
        }

        #region Sum exceeds Int32.MaxValue

        [TestMethod]
        public void PickOne_WeightSumExceedsInt32Max_NoExceptionIsThrown()
        {
            var value = Out.Of()
                .Values(new[] { 'a', 'b', 'c' }).WithWeights(new[] { 2, Int32.MaxValue, 10 })
                .PickOne();

            Assert.IsTrue(value == 'a' || value == 'b' || value == 'c');
        }

        [TestMethod]
        public void Pick_WeightSumExceedsInt32Max_NoExceptionIsThrown()
        {
            var values = Out.Of()
                .Values(new[] { 'a', 'b', 'c' }).WithWeights(new[] { Int32.MaxValue, 2, 10 })
                .Pick(2)
                .ToList();

            Assert.AreEqual(2, values.Count);
        }

        [TestMethod]
        public void PickDistinct_WeightSumExceedsInt32Max_NoExceptionIsThrown()
        {
            var values = Out.Of()
                .Values(new[] { 'a', 'b', 'c' }).WithWeights(new[] { 2, Int32.MaxValue, Int32.MaxValue })
                .PickDistinct(2)
                .ToList();

            Assert.AreEqual(2, values.Count);
        }

        #endregion Sum exceeds Int32.MaxValue
    }
}
