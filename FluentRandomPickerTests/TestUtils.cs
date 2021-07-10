using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPickerTests
{
    public static class TestUtils
    {
        public static void AssertAllSpecifiedValuesAndNoOthersArePossible<T>(Func<T> picking, ISet<T> possibleValues)
        {
            const int NumberOfTries = 1_000_000;
            var pickedValues = new HashSet<T>();

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = picking();
                pickedValues.Add(value);
            }

            Assert.IsFalse(possibleValues.Except(pickedValues).Any(), "Not all values can be picked.");
            Assert.IsFalse(pickedValues.Except(possibleValues).Any(), "More values han expected can be picked.");
        }
    }
}
