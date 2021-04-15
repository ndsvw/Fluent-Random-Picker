using System;
using Fluent_Random_Picker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fluent_Random_Picker_Tests
{
    [TestClass]
    public class GeneralSyntaxTests
    {
        [TestMethod]
        public void GeneralTests()
        {
            Out.Of().Value(8).AndValue(2).PickOne();
            Out.Of().Value(8).AndValue(2).AndValue(9).PickOne();
            Out.Of().Value(8).AndValue(2).AndValue(9).AndValue(5).AndValue(13).PickOne();
            //Out.Of().Value(2).PickOne();

            Out.Of().Value(9).WithWeight(2).AndValue(12).WithWeight(3).PickOne();
            Out.Of().Value(9).WithWeight(2).AndValue(12).WithWeight(3).AndValue(5).WithWeight(5).PickOne();
            //Out.Of().Value(1).AndValue(2).WithWeight(3).AndValue(4).WithWeight(5).PickOne();
            //Out.Of().Value(1).WithWeight(2).AndValue(3).AndValue(4).WithWeight(5).PickOne();
            //Out.Of().Value(1).WithWeight(2).AndValue(3).AndValue(4).WithWeight(5).AndValue(6).WithWeight(7).AndValue(8).PickOne();

            Out.Of().Value(10).WithPercentage(40).AndValue(20).WithPercentage(60).PickOne();
            Out.Of().Value(10).WithPercentage(40).AndValue(20).WithPercentage(30).AndValue(30).WithPercentage(30).PickOne();

            Out.Of()
                .Value("a")
                .AndValue("b")
                .AndValue("c")
                .PickOne();

            Out.Of()
                .Value(1.1)
                .AndValue(2.2)
                .AndValue(3.3);

            Out.Of()
                .Value(TimeSpan.FromMilliseconds(100))
                .AndValue(TimeSpan.FromMinutes(2))
                .AndValue(TimeSpan.FromHours(4));
        }

        [TestMethod]
        public void GenericTests()
        {
            Out.Of<double?>()
                .Value(1)
                .AndValue(1.1)
                .AndValue(null);

            Out.Of<Func<int, int>>()
                .Value(i => i + 2)
                .AndValue(i => i * 2)
                .AndValue(i => (int)Math.Pow(i, 2))
                .AndValue(i => (int)Math.Pow(i, i));
        }
    }
}
