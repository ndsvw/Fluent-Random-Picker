using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker;
using Fluent_Random_Picker.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluent_Random_Picker_Tests
{
    [TestClass]
    public class PickingTests
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
        public void PickDistinct_ResultValuesAreDistict()
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
    }
}
