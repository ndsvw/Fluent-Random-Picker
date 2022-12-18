using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using FluentRandomPicker;

namespace FluentRandomPicker.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary0 = BenchmarkRunner.Run<CompareAllPossibilitiesBenchmark>();

            var summary1 = BenchmarkRunner.Run<PickDistinctWithPrioritiesBenchmark>();
            var summary2 = BenchmarkRunner.Run<PickDistinctWithoutPrioritiesBenchmark>();
        }
    }
}
