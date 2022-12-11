using FluentRandomPicker.Random;
using FluentRandomPicker.Shuffle;
using FluentRandomPicker.ValuePriorities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPicker.Tests.Shuffle
{
    [TestClass]
    public class FisherYatesShuffleTests
    {
        [TestMethod]
        public void Shuffle_WithoutNParameter_AllValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new FisherYatesShuffle<ValuePriorityPair<int>>(rng);

            Assert.That.AllValuesCanChangePositions(shuffle);
        }

        [TestMethod]
        public void Shuffle_WithNParameter_AllNValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new FisherYatesShuffle<ValuePriorityPair<int>>(rng);

            Assert.That.AllNValuesCanChangePositions(shuffle);
        }
    }
}
