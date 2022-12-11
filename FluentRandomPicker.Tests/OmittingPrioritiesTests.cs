using FluentRandomPicker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FluentRandomPicker.Tests
{
    [TestClass]
    public class OmittingPrioritiesTests
    {
        [TestMethod]
        public void OmittingPercentage_2Values_OmittedValueIsDifferenceTo100()
        {
            var pickable1 = Out.Of().Value('a').WithPercentage(20).AndValue('b');
            var pickable2 = Out.Of().Value('a').AndValue('b').WithPercentage(80);
            var valueChancesPairs = new[] { ('a', 0.2), ('b', 0.8) };

            Assert.That.ProbabilitiesMatter(pickable1, valueChancesPairs: valueChancesPairs);
            Assert.That.ProbabilitiesMatter(pickable2, valueChancesPairs: valueChancesPairs);
        }

        [TestMethod]
        public void OmittingPercentagesAsParamsArray_2Values_OmittedValueIsDifferenceTo100()
        {
            var pickable1 = Out.Of().Values(new[] { 'a', 'b' }).WithPercentages(20, null);
            var pickable2 = Out.Of().Values(new[] { 'a', 'b' }).WithPercentages(null, 80);
            var valueChancesPairs = new[] { ('a', 0.2), ('b', 0.8) };

            Assert.That.ProbabilitiesMatter(pickable1, valueChancesPairs: valueChancesPairs);
            Assert.That.ProbabilitiesMatter(pickable2, valueChancesPairs: valueChancesPairs);
        }

        [TestMethod]
        public void OmittingPercentagesAsIEnumerable_2Values_OmittedValueIsDifferenceTo100()
        {
            var pickable1 = Out.Of().Values(new[] { 'a', 'b' }).WithPercentages(new List<int?> { 20, null });
            var pickable2 = Out.Of().Values(new[] { 'a', 'b' }).WithPercentages(new List<int?> { null, 80 });
            var valueChancesPairs = new[] { ('a', 0.2), ('b', 0.8) };

            Assert.That.ProbabilitiesMatter(pickable1, valueChancesPairs: valueChancesPairs);
            Assert.That.ProbabilitiesMatter(pickable2, valueChancesPairs: valueChancesPairs);
        }

        [TestMethod]
        public void OmittingPercentage_PermittedPercentagesSumUpTo100_ThrowsException()
        {
            var pickable1 = Out.Of()
                .Value('a').WithPercentage(20)
                .AndValue('b').WithPercentage(80)
                .AndValue('c');

            var pickable2 = Out.Of()
                .Value('a').WithPercentage(20)
                .AndValue('b')
                .AndValue('c').WithPercentage(80);

            Assert.ThrowsException<ArgumentException>(() => pickable1.PickOne());
            Assert.ThrowsException<ArgumentException>(() => pickable2.PickOne());
        }

        [TestMethod]
        public void OmittingPercentagesAsParamsArray_PermittedPercentagesSumUpTo100_ThrowsException()
        {
            var pickable1 = Out.Of()
                .Values(new[] { 'a', 'b', 'c' })
                .WithPercentages(20, 80, null);

            var pickable2 = Out.Of()
                .Values(new[] { 'a', 'b', 'c' })
                .WithPercentages(20, null, 80);

            Assert.ThrowsException<ArgumentException>(() => pickable1.PickOne());
            Assert.ThrowsException<ArgumentException>(() => pickable2.PickOne());
        }

        [TestMethod]
        public void OmittingPercentagesAsIEnumerable_PermittedPercentagesSumUpTo100_ThrowsException()
        {
            var pickable1 = Out.Of()
                .Values(new[] { 'a', 'b', 'c' })
                .WithPercentages(new List<int?> { 20, 80, null });

            var pickable2 = Out.Of()
                .Values(new[] { 'a', 'b', 'c' })
                .WithPercentages(new List<int?> { 20, null, 80 });

            Assert.ThrowsException<ArgumentException>(() => pickable1.PickOne());
            Assert.ThrowsException<ArgumentException>(() => pickable2.PickOne());
        }

        [TestMethod]
        public void OmittingPercentage_RemainingPercentagesAreNotEquallyDivisible_ThrowsException()
        {
            var pickable1 = Out.Of()
                .Value('a').WithPercentage(99)
                .AndValue('b')
                .AndValue('c');

            var pickable2 = Out.Of()
                .Value('a').WithPercentage(3)
                .AndValue('b')
                .AndValue('c')
                .AndValue('d').WithPercentage(90);

            Assert.ThrowsException<ArgumentException>(() => pickable1.PickOne());
            Assert.ThrowsException<ArgumentException>(() => pickable2.PickOne());
        }

        [TestMethod]
        public void OmittingPercentagesAsParamsArray_RemainingPercentagesAreNotEquallyDivisible_ThrowsException()
        {
            var pickable1 = Out.Of()
                .Values(new[] { 'a', 'b', 'c' })
                .WithPercentages(99, null, null);

            var pickable2 = Out.Of()
                .Values(new[] { 'a', 'b', 'c', 'd' })
                .WithPercentages(3, null, null, 90);

            Assert.ThrowsException<ArgumentException>(() => pickable1.PickOne());
            Assert.ThrowsException<ArgumentException>(() => pickable2.PickOne());
        }

        [TestMethod]
        public void OmittingPercentagesAsIEnumerable_RemainingPercentagesAreNotEquallyDivisible_ThrowsException()
        {
            var pickable1 = Out.Of()
                .Values(new[] { 'a', 'b', 'c' })
                .WithPercentages(new List<int?> { 99, null, null });

            var pickable2 = Out.Of()
                .Values(new[] { 'a', 'b', 'c', 'd' })
                .WithPercentages(new List<int?> { 3, null, null, 90 });

            Assert.ThrowsException<ArgumentException>(() => pickable1.PickOne());
            Assert.ThrowsException<ArgumentException>(() => pickable2.PickOne());
        }

        [TestMethod]
        public void OmittingWeight_2Values_OmittedWeightIsReplacedWith1()
        {
            var pickable1 = Out.Of().Value('a').WithWeight(3).AndValue('b');
            var pickable2 = Out.Of().Value('a').AndValue('b').WithWeight(3);
            var valueChancesPairs1 = new[] { ('a', 0.75), ('b', 0.25) };
            var valueChancesPairs2 = new[] { ('a', 0.25), ('b', 0.75) };

            Assert.That.ProbabilitiesMatter(pickable1, valueChancesPairs: valueChancesPairs1);
            Assert.That.ProbabilitiesMatter(pickable2, valueChancesPairs: valueChancesPairs2);
        }

        [TestMethod]
        public void OmittingWeightsAsParamsArray_2Values_OmittedWeightIsReplacedWith1()
        {
            var pickable1 = Out.Of().Values(new[] { 'a', 'b' }).WithWeights(3, null);
            var pickable2 = Out.Of().Values(new[] { 'a', 'b' }).WithWeights(null, 3);
            var valueChancesPairs1 = new[] { ('a', 0.75), ('b', 0.25) };
            var valueChancesPairs2 = new[] { ('a', 0.25), ('b', 0.75) };

            Assert.That.ProbabilitiesMatter(pickable1, valueChancesPairs: valueChancesPairs1);
            Assert.That.ProbabilitiesMatter(pickable2, valueChancesPairs: valueChancesPairs2);
        }

        [TestMethod]
        public void OmittingWeightsAsIEnumerable_2Values_OmittedWeightIsReplacedWith1()
        {
            var pickable1 = Out.Of().Values(new[] { 'a', 'b' }).WithWeights(new List<int?> { 3, null });
            var pickable2 = Out.Of().Values(new[] { 'a', 'b' }).WithWeights(new List<int?> { null, 3 });
            var valueChancesPairs1 = new[] { ('a', 0.75), ('b', 0.25) };
            var valueChancesPairs2 = new[] { ('a', 0.25), ('b', 0.75) };

            Assert.That.ProbabilitiesMatter(pickable1, valueChancesPairs: valueChancesPairs1);
            Assert.That.ProbabilitiesMatter(pickable2, valueChancesPairs: valueChancesPairs2);
        }
    }
}
