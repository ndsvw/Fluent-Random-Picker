using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker.Shuffle;
using FluentRandomPicker.ValuePriorities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPicker.Tests.Shuffle
{
    public static class ShuffleTestExtensions
    {
        const int _iterations = 1_000_000;

        internal static void AllValuesCanChangePositions(this Assert _, IShuffle<ValuePriorityPair<int>> shuffle)
        {
            var elements = new ValuePriorityPairs<int>
            {
                new ValuePriorityPair<int>(1, 1),
                new ValuePriorityPair<int>(2, 2),
                new ValuePriorityPair<int>(3, 3),
                new ValuePriorityPair<int>(4, 4),
                new ValuePriorityPair<int>(5, 5),
            };

            var value1Set = new HashSet<int>();
            var value2Set = new HashSet<int>();
            var value3Set = new HashSet<int>();
            var value4Set = new HashSet<int>();
            var value5Set = new HashSet<int>();

            for (var i = 0; i < _iterations; i++)
            {
                var shuffled = shuffle.Shuffle(elements).ToList();
                value1Set.Add(shuffled[0].Value);
                value2Set.Add(shuffled[1].Value);
                value3Set.Add(shuffled[2].Value);
                value4Set.Add(shuffled[3].Value);
                value5Set.Add(shuffled[4].Value);
            }

            Assert.AreEqual(elements.Count(), value1Set.Count);
            Assert.AreEqual(elements.Count(), value2Set.Count);
            Assert.AreEqual(elements.Count(), value3Set.Count);
            Assert.AreEqual(elements.Count(), value4Set.Count);
            Assert.AreEqual(elements.Count(), value5Set.Count);
        }

        internal static void AllNValuesCanChangePositions(this Assert _, IShuffle<ValuePriorityPair<int>> shuffle)
        {
            var elements = new ValuePriorityPairs<int>
            {
                new ValuePriorityPair<int>(1, 1),
                new ValuePriorityPair<int>(2, 2),
                new ValuePriorityPair<int>(3, 3),
                new ValuePriorityPair<int>(4, 4),
                new ValuePriorityPair<int>(5, 5),
            };

            var value1Set = new HashSet<int>();
            var value2Set = new HashSet<int>();
            var value3Set = new HashSet<int>();

            for (var i = 0; i < _iterations; i++)
            {
                var shuffled = shuffle.Shuffle(elements, 3).ToList();
                value1Set.Add(shuffled[0].Value);
                value2Set.Add(shuffled[1].Value);
                value3Set.Add(shuffled[2].Value);
            }

            Assert.AreEqual(elements.Count(), value1Set.Count);
            Assert.AreEqual(elements.Count(), value2Set.Count);
            Assert.AreEqual(elements.Count(), value3Set.Count);
        }
    }
}
