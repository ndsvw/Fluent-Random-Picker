using FluentRandomPicker.Random;
using FluentRandomPicker.Shuffle;
using FluentRandomPicker.ValuePriorities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FluentRandomPickerTests.Shuffle
{
    [TestClass]
    public class SortingBasedWeightedLeftShuffleTests
    {
        [TestMethod]
        public void Shuffle_WithoutNParameter_AllValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new SortingBasedWeightedLeftShuffle<int>(rng);

            Assert.That.AllValuesCanChangePositions(shuffle);
        }

        [TestMethod]
        public void Shuffle_WithNParameter_AllNValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new SortingBasedWeightedLeftShuffle<int>(rng);

            Assert.That.AllNValuesCanChangePositions(shuffle);
        }

        [TestMethod]
        public void ImproveAccuracyIfNecessary_MultipleRanksAre1_TheyAreHigherAfterwards()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new SortingBasedWeightedLeftShuffle<char>(rng);

            var ranks = new (int Index, double Rank)[]{
                (0, 1),
                (1, 1),
                (2, 1),
                (3, 1),
                (4, 1),
                (5, 0.5),
            };

            var elements = new List<ValuePriorityPair<char>>(){
                new ValuePriorityPair<char>('a', 500),
                new ValuePriorityPair<char>('b', 1000),
                new ValuePriorityPair<char>('c', 1500),
                new ValuePriorityPair<char>('d', 2000),
                new ValuePriorityPair<char>('e', 2500),
                new ValuePriorityPair<char>('f', 1),
            };

            shuffle.ImproveAccuracyIfNecessary(ranks, elements);

            Assert.IsTrue(ranks[0].Rank > 1.99, "1st rank that was 1 needs to be higher now.");
            Assert.IsTrue(ranks[1].Rank > 1.99, "2nd rank that was 1 needs to be higher now.");
            Assert.IsTrue(ranks[2].Rank > 1.99, "3rd rank that was 1 needs to be higher now.");
            Assert.IsTrue(ranks[3].Rank > 1.99, "4th rank that was 1 needs to be higher now.");
            Assert.IsTrue(ranks[4].Rank > 1.99, "5th rank that was 1 needs to be higher now.");
        }

                [TestMethod]
        public void ImproveAccuracyIfNecessary_MultipleRanksAre1_TheyAreAllOrderedAfterwards()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new SortingBasedWeightedLeftShuffle<char>(rng);

            var ranks = new (int Index, double Rank)[]{
                (0, 1),
                (1, 1),
                (2, 1),
                (3, 1),
                (4, 1),
                (5, 0.5),
            };

            var elements = new List<ValuePriorityPair<char>>(){
                new ValuePriorityPair<char>('a', 500),
                new ValuePriorityPair<char>('b', 1000),
                new ValuePriorityPair<char>('c', 1500),
                new ValuePriorityPair<char>('d', 2000),
                new ValuePriorityPair<char>('e', 2500),
                new ValuePriorityPair<char>('f', 1),
            };

            shuffle.ImproveAccuracyIfNecessary(ranks, elements);

            Assert.IsTrue(ranks[0].Rank >= ranks[1].Rank, "1st rank must be >= 2nd rank.");
            Assert.IsTrue(ranks[1].Rank >= ranks[2].Rank, "2nd rank must be >= 3rd rank.");
            Assert.IsTrue(ranks[2].Rank >= ranks[3].Rank, "3rd rank must be >= 4th rank.");
            Assert.IsTrue(ranks[3].Rank >= ranks[4].Rank, "4th rank must be >= 5th rank.");
            Assert.IsTrue(ranks[4].Rank >= ranks[5].Rank, "5th rank must be >= 6th rank.");
        }

        [TestMethod]
        public void ImproveAccuracyIfNecessary_MultipleValuesAre1ButOneIsLower_TheLowerValueDoesNotChange()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new SortingBasedWeightedLeftShuffle<char>(rng);

            var ranks = new (int Index, double Rank)[]{
                (0, 1),
                (1, 1),
                (2, 1),
                (3, 1),
                (4, 1),
                (5, 0.5),
            };

            var elements = new List<ValuePriorityPair<char>>(){
                new ValuePriorityPair<char>('a', 500),
                new ValuePriorityPair<char>('b', 1000),
                new ValuePriorityPair<char>('c', 1500),
                new ValuePriorityPair<char>('d', 2000),
                new ValuePriorityPair<char>('e', 2500),
                new ValuePriorityPair<char>('f', 1),
            };

            shuffle.ImproveAccuracyIfNecessary(ranks, elements);

            Assert.IsTrue(ranks[5].Rank > 0.49 && ranks[5].Rank < 0.51, "The lower value must not change.");
        }

        [TestMethod]
        public void ImproveAccuracyIfNecessary_MultipleValuesLowerThan1_TheyDoNotChange()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new SortingBasedWeightedLeftShuffle<char>(rng);

            var ranks = new (int Index, double Rank)[]{
                (0, 0.99),
                (1, 0.5),
                (2, 0.01),
            };

            var elements = new List<ValuePriorityPair<char>>(){
                new ValuePriorityPair<char>('a', 500),
                new ValuePriorityPair<char>('b', 1000),
                new ValuePriorityPair<char>('c', 1500),
                new ValuePriorityPair<char>('d', 2000),
                new ValuePriorityPair<char>('e', 2500),
                new ValuePriorityPair<char>('f', 1),
            };

            shuffle.ImproveAccuracyIfNecessary(ranks, elements);

            Assert.IsTrue(ranks[0].Rank > 0.98 && ranks[0].Rank < 1, "A value has changed.");
            Assert.IsTrue(ranks[1].Rank > 0.49 && ranks[1].Rank < 0.51, "A value has changed.");
            Assert.IsTrue(ranks[2].Rank > 0 && ranks[2].Rank < 0.02, "A value has changed.");
        }

        [TestMethod]
        public void ImproveAccuracyIfNecessary_Multiple1RanksAndHighPriorities_TheyAreHigherAfterwards()
        {
            for(var i = 0; i < 10_000; i++){
                var rng = new DefaultRandomNumberGenerator();
                var shuffle = new SortingBasedWeightedLeftShuffle<char>(rng);

                var ranks = new (int Index, double Rank)[]{
                    (0, 1),
                    (1, 1),
                    (2, 1),
                    (3, 1),
                    (4, 1),
                    (5, 0.5),
                };

                var elements = new List<ValuePriorityPair<char>>(){
                    new ValuePriorityPair<char>('a', Int32.MaxValue),
                    new ValuePriorityPair<char>('b', Int32.MaxValue),
                    new ValuePriorityPair<char>('c', Int32.MaxValue),
                    new ValuePriorityPair<char>('d', Int32.MaxValue / 2),
                    new ValuePriorityPair<char>('e', Int32.MaxValue / 10),
                    new ValuePriorityPair<char>('f', 1),
                };

                shuffle.ImproveAccuracyIfNecessary(ranks, elements);

                Assert.IsTrue(ranks[0].Rank > 1.99, "1st rank that was 1 needs to be higher now.");
                Assert.IsTrue(ranks[1].Rank > 1.99, "2nd rank that was 1 needs to be higher now.");
                Assert.IsTrue(ranks[2].Rank > 1.99, "3rd rank that was 1 needs to be higher now.");
                Assert.IsTrue(ranks[3].Rank > 1.99, "4th rank that was 1 needs to be higher now.");
                Assert.IsTrue(ranks[4].Rank > 1.99, "5th rank that was 1 needs to be higher now.");
            }
        }

        [TestMethod]
        public void ImproveAccuracyIfNecessary_NoRanks_NothingHappens()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new SortingBasedWeightedLeftShuffle<char>(rng);

            var ranks = new (int Index, double Rank)[]{ };

            var elements = new List<ValuePriorityPair<char>>(){ };

            shuffle.ImproveAccuracyIfNecessary(ranks, elements);

            Assert.AreEqual(0, ranks.Length);
        }
    }
}
