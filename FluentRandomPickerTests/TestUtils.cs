using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPickerTests
{
    public static class TestUtils
    {
        public static void AssertAllSpecifiedValuesAndNoOthersArePossible<T>(Func<T> pPicking, ISet<T> pPossibleValues)
        {
            const int NumberOfTries = 1_000_000;
            var pickedValues = new HashSet<T>();

            for (var i = 0; i < NumberOfTries; i++)
            {
                var value = pPicking();
                pickedValues.Add(value);
            }

            Assert.IsFalse(pPossibleValues.Except(pickedValues).Any(), "Not all values can be picked.");
            Assert.IsFalse(pickedValues.Except(pPossibleValues).Any(), "More values han expected can be picked.");
        }
    }
}
