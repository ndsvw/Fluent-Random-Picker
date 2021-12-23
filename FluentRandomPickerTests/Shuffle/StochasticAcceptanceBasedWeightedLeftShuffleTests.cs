using FluentRandomPicker.Random;
using FluentRandomPicker.Shuffle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPickerTests.Shuffle
{
    [TestClass]
    public class StochasticAcceptanceBasedWeightedLeftShuffleTests
    {
        [TestMethod]
        public void Shuffle_WithoutNParameter_AllValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new StochasticAcceptanceBasedWeightedLeftShuffle<int>(rng);

            Assert.That.AllValuesCanChangePositions(shuffle);
        }

        [TestMethod]
        public void Shuffle_WithNParameter_AllNValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new StochasticAcceptanceBasedWeightedLeftShuffle<int>(rng);

            Assert.That.AllNValuesCanChangePositions(shuffle);
        }
    }
}
