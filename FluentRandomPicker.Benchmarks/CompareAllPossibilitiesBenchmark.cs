using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;

namespace FluentRandomPicker.Benchmarks
{
    [MemoryDiagnoser]
    public class CompareAllPossibilitiesBenchmark
    {
        private static readonly System.Random _random = new();
        private static readonly Consumer _consumer = new();

        private static readonly int[] _thousandElements = Enumerable.Range(0, 1_000).Select(x => _random.Next()).ToArray();

        [Benchmark(Baseline = true)]
        public void PickHalfWithoutPriorities()
        {
            Out.Of().Values(_thousandElements)
                    .Pick(_thousandElements.Length / 2)
                    .Consume(_consumer);
        }

        [Benchmark]
        public void PickHalfDistinctWithoutPriorities()
        {
            Out.Of().Values(_thousandElements)
                    .PickDistinct(_thousandElements.Length / 2)
                    .Consume(_consumer);
        }

        [Benchmark]
        public void PickHalfWithPriorities()
        {
            Out.Of().Values(_thousandElements)
                    .WithWeights(_thousandElements)
                    .Pick(_thousandElements.Length / 2)
                    .Consume(_consumer);
        }

        [Benchmark]
        public void PickHalfDistinctWithPriorities()
        {
            Out.Of().Values(_thousandElements)
                    .WithWeights(_thousandElements)
                    .PickDistinct(_thousandElements.Length / 2)
                    .Consume(_consumer);
        }

        [Benchmark]
        public void PickOneWithoutPriorities()
        {
            Out.Of().Values(_thousandElements)
                    .PickOne();
        }

        [Benchmark]
        public void PickOneWithPriorities()
        {
            Out.Of().Values(_thousandElements)
                    .WithWeights(_thousandElements)
                    .PickOne();
        }
    }
}