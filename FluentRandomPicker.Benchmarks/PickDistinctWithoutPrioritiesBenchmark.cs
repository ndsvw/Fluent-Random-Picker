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
    public class PickDistinctWithoutPrioritiesBenchmark
    {
        private static readonly System.Random _random = new();
        private static readonly Consumer _consumer = new();

        private static readonly int[] _tenElements = Enumerable.Range(0, 10).Select(x => _random.Next()).ToArray();
        private static readonly int[] _hundredElements = Enumerable.Range(0, 100).Select(x => _random.Next()).ToArray();
        private static readonly int[] _thousandElements = Enumerable.Range(0, 1_000).Select(x => _random.Next()).ToArray();
        private static readonly int[] _tenthousandElements = Enumerable.Range(0, 10_000).Select(x => _random.Next()).ToArray();

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public void PickDistinctWithoutPriorities(int[] array)
        {
            Out.Of().Values(array).PickDistinct(array.Length / 2).Consume(_consumer);
        }

        public IEnumerable<int[]> Data()
        {
            yield return _tenElements;
            yield return _hundredElements;
            yield return _thousandElements;
            yield return _tenthousandElements;
        }
    }
}