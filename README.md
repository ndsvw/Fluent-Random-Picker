# Fluent Random Picker

This library offers a nice, fluent way to pick random values.
Probabilities can be specified, values can be weighted.

## Examples

```c#
var randomNumber = Out.Of().Values(5, 6).PickOne();
// randomNumber is 5 or 6 with equal probability.
```

```c#
var randomChar = Out.Of()
                  .Value('a').WithPercentage(70)
                  .AndValue('b').WithPercentage(30)
                  .PickOne();
// or
var randomChar = Out.Of().Values('a', 'b')
                  .WithPercentages(70, 30)
                  .PickOne();
// or
var randomChar = Out.Of().Values(new List<char> { 'a', 'b' })
                  .WithPercentages(new List<int> { 70, 30 })
                  .PickOne();
// randomChar is 'a' with a probability of 70 % and 'b' with a probability of 30 %.
```

```c#
var randomString = Out.Of()
                  .Value("hello").WithWeight(2)
                  .AndValue("world").WithWeight(3)
                  .PickOne();
// or
var randomChar = Out.Of().Values("hello", "world")
                  .WithWeights(2, 3)
                  .PickOne();
// or
var randomChar = Out.Of().Values(new HashSet<string> { "hello", "world" })
                  .WithWeights(new List<int> { 2, 3 })
                  .PickOne();
// randomString is "hello" or "world", but the probability for "world" is 1.5 times as high.
```

```c#
var randomInts = Out.Of()
                  .Value(1).WithPercentage(80)
                  .AndValue(10).WithPercentage(10)
                  .AndValue(100).WithPercentage(5)
                  .AndValue(1000).WithPercentage(5)
                  .Pick(5);
// randomInts can be [1, 1, 1, 1, 1] with a higher probability or [1, 1, 100, 10, 1]
// or even [1000, 1000, 1000, 1000, 1000] with a very small probability.
```

```c#
var randomInts = Out.Of()
                  .Values(1, 10, 100, 1000)
                  .WithPercentages(70, 15, 10, 5)
                  .PickDistinct(2);
// randomInts can be [1, 10], [1, 100], [1, 1000] ... but not [1, 1], [10, 10], ...
```

```c#
var operation = Out.Of<Func<long, long>>()
                .Value(i => i + 2)
                .AndValue(i => i * 2)
                .AndValue(i => (long)Math.Pow(i, 2))
                .AndValue(i => (long)Math.Pow(i, i))
                .PickOne();

var result = operation(10);
// result equals 10 + 2 or 10 * 2 or 10^2 or 10^10. 
```
