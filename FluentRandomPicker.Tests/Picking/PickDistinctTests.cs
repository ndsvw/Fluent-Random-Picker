using System;
using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker;
using FluentRandomPicker.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPicker.Tests.Picking
{
    [TestClass]
    public class PickDistinctTests
    {
        [TestMethod]
        public void PickDistinct_PicksSpecifiedNumberOfValues()
        {
            var values = Out.Of()
                .Value('a')
                .AndValue('b')
                .AndValue('c')
                .PickDistinct(2);

            Assert.AreEqual(2, new HashSet<char>(values).Count);
        }

        [TestMethod]
        public void PickDistinct_ResultValuesAreDistinct()
        {
            var values = Out.Of()
                .Value('a').WithPercentage(1)
                .AndValue('b').WithPercentage(1)
                .AndValue('c').WithPercentage(97)
                .AndValue('d').WithPercentage(1)
                .PickDistinct(2)
                .ToList();

            Assert.AreNotEqual(values[0], values[1]);
        }

        [TestMethod]
        public void PickDistinct_AllValuesArePickable()
        {
            var values = Out.Of()
                .Value('a').WithPercentage(1)
                .AndValue('b').WithPercentage(1)
                .AndValue('c').WithPercentage(97)
                .AndValue('d').WithPercentage(1)
                .PickDistinct(4)
                .ToList();

            Assert.AreNotEqual(values[0], values[1]);
            Assert.AreNotEqual(values[1], values[2]);
            Assert.AreNotEqual(values[2], values[3]);
        }

        [TestMethod]
        public void PickDistinctTryingToPickTooManyValues_ThrowsException()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithPercentage(1)
                    .AndValue('b').WithPercentage(1)
                    .AndValue('c').WithPercentage(97)
                    .AndValue('d').WithPercentage(1)
                    .PickDistinct(5);
            }

            Assert.ThrowsException<NotEnoughValuesToPickException>(Execute);
        }

        [TestMethod]
        public void PickDistinctTryingToPickZeroValues_ReturnsEmptyEnumerable()
        {
            var values = Out.Of()
                .Value('a').WithPercentage(1)
                .AndValue('b').WithPercentage(1)
                .AndValue('c').WithPercentage(97)
                .AndValue('d').WithPercentage(1)
                .PickDistinct(0);

            Assert.AreEqual(0, values.Count());
        }

        [TestMethod]
        public void PickDistinctTryingToPickNegativeNumberOfValues_ThrowsException()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithPercentage(1)
                    .AndValue('b').WithPercentage(1)
                    .AndValue('c').WithPercentage(97)
                    .AndValue('d').WithPercentage(1)
                    .PickDistinct(-1);
            }

            Assert.ThrowsException<PickingNegativeNumberOfValuesNotPossibleException>(Execute);
        }

        [TestMethod]
        public void PickDistinctWithEqualProbability_ProbabilitiesMatter()
        {
            const int NumberOfTries = 1_000_000;
            const double AcceptedDeviation = 0.1;

            var counterABC = 0;
            var counterACB = 0;
            var counterBAC = 0;
            var counterBCA = 0;
            var counterCAB = 0;
            var counterCBA = 0;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var combination = Out.Of()
                    .Value('a')
                    .AndValue('b')
                    .AndValue('c')
                    .PickDistinct(3)
                    .ToArray();

                if (combination is ['a', 'b', 'c'])
                    counterABC++;
                else if (combination is ['a', 'c', 'b'])
                    counterACB++;
                else if (combination is ['b', 'a', 'c'])
                    counterBAC++;
                else if (combination is ['b', 'c', 'a'])
                    counterBCA++;
                else if (combination is ['c', 'a', 'b'])
                    counterCAB++;
                else if (combination is ['c', 'b', 'a'])
                    counterCBA++;
            }

            Assert.IsTrue(counterABC >= NumberOfTries * (1 / 6d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterABC <= NumberOfTries * (1 / 6d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterACB >= NumberOfTries * (1 / 6d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterACB <= NumberOfTries * (1 / 6d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterBAC >= NumberOfTries * (1 / 6d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterBAC <= NumberOfTries * (1 / 6d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterBCA >= NumberOfTries * (1 / 6d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterBCA <= NumberOfTries * (1 / 6d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterCAB >= NumberOfTries * (1 / 6d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterCAB <= NumberOfTries * (1 / 6d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterCBA >= NumberOfTries * (1 / 6d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterCBA <= NumberOfTries * (1 / 6d) * (1 + AcceptedDeviation));
        }


        [TestMethod]
        public void PickDistinctWithDifferentProbabilities_ProbabilitiesMatter()
        {
            const int NumberOfTries = 1_000_000;
            const double AcceptedDeviation = 0.1;

            var counterABC = 0;
            var counterACB = 0;
            var counterBAC = 0;
            var counterBCA = 0;
            var counterCAB = 0;
            var counterCBA = 0;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var combination = Out.Of()
                    .Value('a').WithPercentage(70)
                    .AndValue('b').WithPercentage(20)
                    .AndValue('c').WithPercentage(10)
                    .PickDistinct(3)
                    .ToArray();

                if (combination is ['a', 'b', 'c'])
                    counterABC++;
                else if (combination is ['a', 'c', 'b'])
                    counterACB++;
                else if (combination is ['b', 'a', 'c'])
                    counterBAC++;
                else if (combination is ['b', 'c', 'a'])
                    counterBCA++;
                else if (combination is ['c', 'a', 'b'])
                    counterCAB++;
                else if (combination is ['c', 'b', 'a'])
                    counterCBA++;
            }

            Assert.IsTrue(counterABC >= NumberOfTries * (0.7 * 2 / 3d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterABC <= NumberOfTries * (0.7 * 2 / 3d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterACB >= NumberOfTries * (0.7 * 1 / 3d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterACB <= NumberOfTries * (0.7 * 1 / 3d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterBAC >= NumberOfTries * (0.2 * 7 / 8d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterBAC <= NumberOfTries * (0.2 * 7 / 8d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterBCA >= NumberOfTries * (0.2 * 1 / 8d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterBCA <= NumberOfTries * (0.2 * 1 / 8d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterCAB >= NumberOfTries * (0.1 * 7 / 9d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterCAB <= NumberOfTries * (0.1 * 7 / 9d) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterCBA >= NumberOfTries * (0.1 * 2 / 9d) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterCBA <= NumberOfTries * (0.1 * 2 / 9d) * (1 + AcceptedDeviation));
        }

        [TestMethod]
        public void PickDistinctWithDifferentLargeProbabilities_ProbabilitiesMatter()
        {
            const int NumberOfTries = 1_000_000;
            const double AcceptedDeviation = 0.1;

            var counterABC = 0;
            var counterACB = 0;
            var counterBAC = 0;
            var counterBCA = 0;
            var counterCAB = 0;
            var counterCBA = 0;

            for (var i = 0; i < NumberOfTries; i++)
            {
                var combination = Out.Of()
                    .Value('a').WithWeight(2_000_000_000)
                    .AndValue('b').WithWeight(1_900_000_000)
                    .AndValue('c').WithWeight(1_800_000_000)
                    .PickDistinct(3)
                    .ToArray();

                if (combination is ['a', 'b', 'c'])
                    counterABC++;
                else if (combination is ['a', 'c', 'b'])
                    counterACB++;
                else if (combination is ['b', 'a', 'c'])
                    counterBAC++;
                else if (combination is ['b', 'c', 'a'])
                    counterBCA++;
                else if (combination is ['c', 'a', 'b'])
                    counterCAB++;
                else if (combination is ['c', 'b', 'a'])
                    counterCBA++;
            }

            var chancePickAFirst = 2_000_000_000 / ((double)2_000_000_000 + 1_999_000_000 + 1_800_000_000);
            var chancePickBFirst = 1_999_000_000 / ((double)2_000_000_000 + 1_999_000_000 + 1_800_000_000);
            var chancePickCFirst = 1_800_000_000 / ((double)2_000_000_000 + 1_999_000_000 + 1_800_000_000);

            var chancePickBSecondThenC = 1_999_000_000 / ((double)1_999_000_000 + 1_800_000_000);
            var chancePickCSecondThenB = 1_800_000_000 / ((double)1_999_000_000 + 1_800_000_000);

            var chancePickASecondThenC = 2_000_000_000 / ((double)2_000_000_000 + 1_800_000_000);
            var chancePickCSecondThenA = 1_800_000_000 / ((double)2_000_000_000 + 1_800_000_000);

            var chancePickASecondThenB = 2_000_000_000 / ((double)2_000_000_000 + 1_999_000_000);
            var chancePickBSecondThenA = 1_999_000_000 / ((double)2_000_000_000 + 1_999_000_000);

            Assert.IsTrue(counterABC >= NumberOfTries * (chancePickAFirst * chancePickBSecondThenC) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterABC <= NumberOfTries * (chancePickAFirst * chancePickBSecondThenC) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterACB >= NumberOfTries * (chancePickAFirst * chancePickCSecondThenB) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterACB <= NumberOfTries * (chancePickAFirst * chancePickCSecondThenB) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterBAC >= NumberOfTries * (chancePickBFirst * chancePickASecondThenC) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterBAC <= NumberOfTries * (chancePickBFirst * chancePickASecondThenC) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterBCA >= NumberOfTries * (chancePickBFirst * chancePickCSecondThenA) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterBCA <= NumberOfTries * (chancePickBFirst * chancePickCSecondThenA) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterCAB >= NumberOfTries * (chancePickCFirst * chancePickASecondThenB) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterCAB <= NumberOfTries * (chancePickCFirst * chancePickASecondThenB) * (1 + AcceptedDeviation));

            Assert.IsTrue(counterCBA >= NumberOfTries * (chancePickCFirst * chancePickBSecondThenA) * (1 - AcceptedDeviation));
            Assert.IsTrue(counterCBA <= NumberOfTries * (chancePickCFirst * chancePickBSecondThenA) * (1 + AcceptedDeviation));
        }
    }
}
