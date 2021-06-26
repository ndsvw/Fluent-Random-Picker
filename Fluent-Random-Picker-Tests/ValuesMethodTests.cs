using System;
using System.Collections.Generic;
using System.Linq;
using Fluent_Random_Picker;
using Fluent_Random_Picker.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluent_Random_Picker_Tests
{
    [TestClass]
    public class ValuesMethodTests
    {
        [TestMethod]
        public void ValuesEmptyEnumerable_ThrowsException()
        {
            Assert.ThrowsException<NotEnoughValuesToPickException>(() => Out.Of().Values(Enumerable.Empty<int>()).PickOne());
        }

        [TestMethod]
        public void ValuesEmptyArray_ThrowsException()
        {
            Assert.ThrowsException<NotEnoughValuesToPickException>(() => Out.Of().Values(Array.Empty<int>()).PickOne());
        }

        [TestMethod]
        public void ValuesListWith1Element_ThrowsException()
        {
            Assert.ThrowsException<NotEnoughValuesToPickException>(() => Out.Of().Values(new List<int> { 1 }).PickOne());
        }

        [TestMethod]
        public void ValuesArrayWith1Element_ThrowsException()
        {
            Assert.ThrowsException<NotEnoughValuesToPickException>(() => Out.Of().Values(new int[] { 1 }).PickOne());
        }


        [TestMethod]
        public void ValuesArrayWith2Elements_AllSpecifiedValuesAndNoOthersArePossible()
        {
            Func<int> pickValues = () => Out.Of().Value(1).AndValue(2).PickOne();
            var expectedValues = new HashSet<int> { 1, 2 };
            TestUtils.AssertAllSpecifiedValuesAndNoOthersArePossible(pickValues, expectedValues);
        }

        [TestMethod]
        public void ValuesArrayWithSeveralElements_AllSpecifiedValuesAndNoOthersArePossible()
        {
            var random = new System.Random();
            var possibleValues = Enumerable.Range(1, 25).ToHashSet();
            Func<int> pickValues = () => Out.Of<int>().Values(possibleValues).PickOne();
            TestUtils.AssertAllSpecifiedValuesAndNoOthersArePossible(pickValues, possibleValues);
        }


        [TestMethod]
        public void ValuesMoreValuesThanWeights_ThrowsException()
        {
            var values = new List<byte> { 1, 2, 3 };
            Assert.ThrowsException<NumberOfValuesDoesNotMatchNumberOfPrioritiesException>(() => Out.Of().Values(values).WithWeights(1, 2).PickOne());
        }

        [TestMethod]
        public void ValuesLessValuesThanWeights_ThrowsException()
        {
            var values = new List<byte> { 1, 2 };
            Assert.ThrowsException<NumberOfValuesDoesNotMatchNumberOfPrioritiesException>(() => Out.Of().Values(values).WithWeights(1, 2, 3).PickOne());
        }

        [TestMethod]
        public void ValuesNoWeights_ThrowsException()
        {
            var values = new List<byte> { 1, 2 };
            Assert.ThrowsException<NumberOfValuesDoesNotMatchNumberOfPrioritiesException>(() => Out.Of().Values(values).WithWeights().PickOne());
        }

        [TestMethod]
        public void ValuesMoreValuesThanPercentages_ThrowsException()
        {
            var values = new List<byte> { 1, 2, 3 };
            Assert.ThrowsException<NumberOfValuesDoesNotMatchNumberOfPrioritiesException>(() => Out.Of().Values(values).WithPercentages(75, 25).PickOne());
        }

        [TestMethod]
        public void ValuesLessValuesThanPercentages_ThrowsException()
        {
            var values = new List<byte> { 1, 2 };
            Assert.ThrowsException<NumberOfValuesDoesNotMatchNumberOfPrioritiesException>(() => Out.Of().Values(values).WithPercentages(50, 25, 25).PickOne());
        }

        [TestMethod]
        public void ValuesNoPercentags_ThrowsException()
        {
            var values = new List<byte> { 1, 2 };
            Assert.ThrowsException<NumberOfValuesDoesNotMatchNumberOfPrioritiesException>(() => Out.Of().Values(values).WithPercentages().PickOne());
        }
    }
}
