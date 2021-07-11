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
        public void Shuffle_WithoutNParameter_AllValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new FisherYatesShuffle<int>(rng);
            var elements = new List<int> { 1, 2, 3, 4, 5 };

            var value1Set = new HashSet<int>();
            var value2Set = new HashSet<int>();
            var value3Set = new HashSet<int>();
            var value4Set = new HashSet<int>();
            var value5Set = new HashSet<int>();

            for (var i = 0; i < _iterations; i++)
            {
                var shuffled = shuffle.Shuffle(elements).ToList();
                value1Set.Add(shuffled[0]);
                value2Set.Add(shuffled[1]);
                value3Set.Add(shuffled[2]);
                value4Set.Add(shuffled[3]);
                value5Set.Add(shuffled[4]);
            }

            Assert.AreEqual(elements.Count, value1Set.Count);
            Assert.AreEqual(elements.Count, value2Set.Count);
            Assert.AreEqual(elements.Count, value3Set.Count);
            Assert.AreEqual(elements.Count, value4Set.Count);
            Assert.AreEqual(elements.Count, value5Set.Count);
        }

        [TestMethod]
        public void Shuffle_WithNParameter_AllNValuesCanChangePosition()
        {
            var rng = new DefaultRandomNumberGenerator();
            var shuffle = new FisherYatesShuffle<int>(rng);
            var elements = new List<int> { 1, 2, 3, 4, 5 };

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
