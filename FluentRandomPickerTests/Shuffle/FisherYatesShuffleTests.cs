using System.Collections.Generic;
using FluentRandomPicker.Random;
using FluentRandomPicker.Shuffle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FluentRandomPickerTests.Shuffle
{
    [TestClass]
    public class FisherYatesShuffleTests
    {
        const int _iterations = 1_000_000;

        [TestMethod]
        public void Shuffle_WithNParameter_AllNValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new FisherYatesShuffle<int>(rng);
            var elements = new List<int> { 1, 2, 3, 4, 5, 6 };

            var value1Set = new HashSet<int>();
            var value2Set = new HashSet<int>();
            var value3Set = new HashSet<int>();

            for (var i = 0; i < _iterations; i++)
            {
                var shuffled = shuffle.Shuffle(elements, 3).ToList();
                value1Set.Add(shuffled[0]);
                value2Set.Add(shuffled[1]);
                value3Set.Add(shuffled[2]);
            }

            Assert.AreEqual(elements.Count, value1Set.Count);
            Assert.AreEqual(elements.Count, value2Set.Count);
            Assert.AreEqual(elements.Count, value3Set.Count);
        }


    }
}
