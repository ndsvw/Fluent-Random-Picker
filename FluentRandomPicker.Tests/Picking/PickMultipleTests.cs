using System.Collections.Generic;
using System.Linq;
using FluentRandomPicker;
using FluentRandomPicker.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentRandomPicker.Tests.Picking;

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
    public void PickMoreValuesThanSpecified_ReturnsAtLeast1Duplicate()
    {
        var values = Out.Of()
            .Value(1).WithPercentage(70)
            .AndValue(10).WithPercentage(15)
            .AndValue(100).WithPercentage(10)
            .AndValue(1000).WithPercentage(5)
            .Pick(5).ToList();

        Assert.AreEqual(5, values.Count);
        Assert.IsTrue(values.Distinct().Count() <= 4);
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

        Assert.ThrowsExactly<PickingNegativeNumberOfValuesNotPossibleException>(Execute);
    }
}