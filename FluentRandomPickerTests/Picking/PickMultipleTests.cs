using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker;
using FluentRandomPicker.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPickerTests.Picking
{
    [TestClass]
    public class PickMultipleTests
    {
        [TestMethod]
        public void Pick_PicksSpecifiedNumberOfValues()
        {
            var values = Out.Of()
                .Value('a')
                .AndValue('b')
                .AndValue('c')
                .Pick(2);

            Assert.AreEqual(2, new List<char>(values).Count);
        }

        [TestMethod]
        public void PickTryingToPickTooManyValues_ThrowsException()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithPercentage(1)
                    .AndValue('b').WithPercentage(1)
                    .AndValue('c').WithPercentage(97)
                    .AndValue('d').WithPercentage(1)
                    .Pick(5);
            }

            Assert.ThrowsException<NotEnoughValuesToPickException>(Execute);
        }

        [TestMethod]
        public void PickTryingToPickZeroValues_ReturnsEmptyEnumerable()
        {
            var values = Out.Of()
                .Value('a').WithPercentage(1)
                .AndValue('b').WithPercentage(1)
                .AndValue('c').WithPercentage(97)
                .AndValue('d').WithPercentage(1)
                .Pick(0);

            Assert.AreEqual(0, values.Count());
        }

        [TestMethod]
        public void PickTryingToPickNegativeNumberOfValues_ThrowsException()
        {
            static void Execute()
            {
                Out.Of()
                    .Value('a').WithPercentage(1)
                    .AndValue('b').WithPercentage(1)
                    .AndValue('c').WithPercentage(97)
                    .AndValue('d').WithPercentage(1)
                    .Pick(-1);
            }

            Assert.ThrowsException<PickingNegativeNumberOfValuesNotPossibleException>(Execute);
        }
    }
}