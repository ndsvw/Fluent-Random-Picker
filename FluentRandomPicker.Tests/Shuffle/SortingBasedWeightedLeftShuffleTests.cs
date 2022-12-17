using FluentRandomPicker.Random;
using FluentRandomPicker.Shuffle;
using FluentRandomPicker.ValuePriorities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace FluentRandomPicker.Tests.Shuffle
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
        public void CalculateRank_RanksIsSmallTheFirstTime_RankIsLowerThan1()
        {
            var rng = Substitute.For<IRandomNumberGenerator>();
            rng.NextDouble().Returns(0.1);
            var shuffle = new SortingBasedWeightedLeftShuffle<int>(rng);

            var rank = shuffle.CalculateRank(int.MaxValue);

            Assert.IsTrue(rank < 1);
        }  

        [TestMethod]
        public void CalculateRank_RanksIs1XTimes_RankIsLargerThanX()
        {
            var rng = Substitute.For<IRandomNumberGenerator>();
            rng.NextDouble().Returns(0.9999999999, 0.9999999999, 0.9999999999, 0.22);
            var shuffle = new SortingBasedWeightedLeftShuffle<int>(rng);

            var rank = shuffle.CalculateRank(int.MaxValue);

            Assert.IsTrue(rank > 3);
        }  
    }
}
