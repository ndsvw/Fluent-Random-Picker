using System.Linq;
using FluentRandomPicker;
using FluentRandomPicker.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPickerTests.Random
{
    [TestClass]
    public class OfTests
    {
        [TestMethod]
        public void Of_WithSeedAlwaysReturnsTheSameValue()
        {
            var value = Out.Of(1234567).Values(new[] { 1, 2, 3, 4 }).PickOne();
            var value2 = Out.Of(1234567).Values(new[] { 1, 2, 3, 4 }).PickOne();

            Assert.AreEqual(value, value2);
        }

        [TestMethod]
        public void OfOfT_WithSeedAlwaysReturnsTheSameValues()
        {
            var value = Out.Of<char>(1234567).Values(new[] { 'a', 'b', 'c', 'd' }).PickOne();
            var value2 = Out.Of<char>(1234567).Values(new[] { 'a', 'b', 'c', 'd' }).PickOne();

            Assert.AreEqual(value, value2);
        }

        [TestMethod]
        public void Of_WithSeedAlwaysReturnsTheSameValues()
        {
            var value = Out.Of(1234567).Values(new[] { 1, 2, 3, 4 }).Pick(3).ToList();
            var value2 = Out.Of(1234567).Values(new[] { 1, 2, 3, 4 }).Pick(3).ToList();

            CollectionAssert.AreEqual(value, value2);
        }

        [TestMethod]
        public void Of_WithSeedAlwaysReturnsTheSameDistinctValues()
        {
            var value = Out.Of(1234567).Values(new[] { 1, 2, 3, 4 }).PickDistinct(3).ToList();
            var value2 = Out.Of(1234567).Values(new[] { 1, 2, 3, 4 }).PickDistinct(3).ToList();

            CollectionAssert.AreEqual(value, value2);
        }

        [TestMethod]
        public void Of_WithRngReturnsValuesAccordingToTheRng()
        {
            var rng1 = new DefaultRandomNumberGenerator(987654);
            var rng2 = new DefaultRandomNumberGenerator(987654);
            var value = Out.Of(rng1).Values(new[] { 1, 2, 3, 4 }).PickDistinct(3).ToList();
            var value2 = Out.Of(rng2).Values(new[] { 1, 2, 3, 4 }).PickDistinct(3).ToList();

            CollectionAssert.AreEqual(value, value2);
        }

        [TestMethod]
        public void OfOfT_WithRngReturnsValuesAccordingToTheRng()
        {
            var rng1 = new DefaultRandomNumberGenerator(987654);
            var rng2 = new DefaultRandomNumberGenerator(987654);
            var value = Out.Of<char>(rng1).Values(new[] { 'a', 'b', 'c', 'd' }).PickDistinct(3).ToList();
            var value2 = Out.Of<char>(rng2).Values(new[] { 'a', 'b', 'c', 'd' }).PickDistinct(3).ToList();

            CollectionAssert.AreEqual(value, value2);
        }
    }
}
