# Fluent Random Picker

![Fluent Random Picker](https://raw.githubusercontent.com/ndsvw/Fluent-Random-Picker/main/FluentRandomPicker/icon48x48.png "Fluent Random Picker")

Fluent Random Picker is a nice, performant fluent way to pick random values.
Probabilities can be specified, values can be weighted.

[![License MIT](https://img.shields.io/github/license/ndsvw/Fluent-Random-Picker)](https://github.com/ndsvw/Fluent-Random-Picker/blob/main/LICENSE)
[![Build status](https://github.com/ndsvw/Fluent-Random-Picker/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ndsvw/Fluent-Random-Picker)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ndsvw_Fluent-Random-Picker&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=ndsvw_Fluent-Random-Picker)
[![Code coverage](https://img.shields.io/codecov/c/github/ndsvw/Fluent-Random-Picker)](https://app.codecov.io/gh/ndsvw/Fluent-Random-Picker)
[![Nuget version](https://img.shields.io/nuget/v/FluentRandomPicker)](https://www.nuget.org/packages/FluentRandomPicker)
[![NuGet downloads](https://img.shields.io/nuget/dt/FluentRandomPicker.svg)](https://www.nuget.org/packages/FluentRandomPicker)

<!-- TOC -->

- [Fluent Random Picker](#fluent-random-picker)
    - [Compatibility](#compatibility)
    - [Getting started](#getting-started)
    - [Examples](#examples)
        - [The easiest example](#the-easiest-example)
        - [Specifying percentages](#specifying-percentages)
        - [Specifying weights](#specifying-weights)
        - [Picking multiple values](#picking-multiple-values)
        - [Picking distinct values](#picking-distinct-values)
        - [Using external types with weight/percentage information](#using-external-types-with-weightpercentage-information)
        - [Omitting percentages or weights](#omitting-percentages-or-weights)
        - [Specifying the returned type explicitly](#specifying-the-returned-type-explicitly)
    - [Advanced](#advanced)
        - [Specifying a seed](#specifying-a-seed)
        - [Using a different random number generator](#using-a-different-random-number-generator)
    - [Migration to version 3](#migration-to-version-3)
    - [Migration to version 2](#migration-to-version-2)

<!-- /TOC -->

## Compatibility

Fluent Random Picker uses .Net Standard 2.0. That means, it can be used in projects with the following target frameworks:

- ?????? .Net 6
- ?????? .Net 5
- ?????? .Net Core 3.X
- ?????? .Net Core 2.X
- ??? .Net Core 1.X
- 
- ?????? .Net Standard 2.1
- ?????? .Net Standard 2.0
- ??? .Net Standard 1.X
-
- ?????? .Net Framework 4.8
- ?????? .Net Framework 4.7.2
- ??? .Net Framework 4.6.X

## Getting started

Install the nuget package (https://www.nuget.org/packages/FluentRandomPicker/)

Add the using directive:
```c#
using FluentRandomPicker;
```

Begin with Out.Of() (see the examples).
```c#
Out.Of()...
```


## Examples

### The easiest example
```c#
var randomNumber = Out.Of().Value(5).AndValue(6).PickOne();
// randomNumber is 5 or 6 with equal probability.
```

### Specifying percentages
```c#
var randomChar = Out.Of()
                  .Value('a').WithPercentage(70)
                  .AndValue('b').WithPercentage(30)
                  .PickOne();
// or
var randomChar = Out.Of().Values(new List<char> { 'a', 'b' })
                  .WithPercentages(70, 30)
                  .PickOne();
// or
var randomChar = Out.Of().Values(new List<char> { 'a', 'b' })
                  .WithPercentages(new List<int> { 70, 30 })
                  .PickOne();
// randomChar is 'a' with a probability of 70 % and 'b' with a probability of 30 %.
```

### Specifying weights
```c#
var randomString = Out.Of()
                  .Value("hello").WithWeight(2)
                  .AndValue("world").WithWeight(3)
                  .PickOne();
// or
var randomChar = Out.Of().Values(new HashSet<string> { "hello", "world" })
                  .WithWeights(2, 3)
                  .PickOne();
// or
var randomChar = Out.Of().Values(new HashSet<string> { "hello", "world" })
                  .WithWeights(new List<int> { 2, 3 })
                  .PickOne();
// randomString is "hello" or "world", but the probability for "world" is 1.5 times as high.
```

### Picking multiple values
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

### Picking distinct values
```c#
var randomInts = Out.Of()
                  .Values(new List<int> { 1, 10, 100, 1000 })
                  .WithPercentages(70, 15, 10, 5)
                  .PickDistinct(2);
// randomInts can be [1, 10], [1, 100], [1, 1000] ... but not [1, 1], [10, 10], ...
```

### Using external types with weight/percentage information
```c#
class Item {
    public int Rarity { get; set; }
    public string Name { get; set; }
}

var items = new Item[]
{
    new Item { Name = "Stone", Rarity = 5 }, // common
    new Item { Name = "Silver helmet", Rarity = 2 }, // uncommon
    new Item { Name = "Gold sword", Rarity = 1 }, // rare
};

var itemName = Out.Of()
                  .PrioritizedElements(items)
                  .WithValueSelector(x => x.Name)
                  .AndWeightSelector(x => x.Rarity)
                  .PickOne();

// itemName is "Stone" in 5/8 of the cases, "Silver helmet" in 2/8 of the cases and "Gold sword" in 1/8 of the cases.
// If no value selector is specified, the whole item object will be returned instead of only its name.
```

### Omitting percentages or weights
```c#
var randomChar = Out.Of()
                  .Value('a').WithPercentage(80)
                  .AndValue('b') // no percentage
                  .AndValue('c') // no percentage
                  .PickOne();
// The missing percentages to reach 100% are equally distributed on the values without specified percentages.
// Attention! The missing percentages to reach 100% must be divisible without remainder through the number of values without percentages.
// randomChar is 'a' with a probability of 80% or 'b' or 'c' with a probability of each 10%.
```

```c#
var randomString = Out.Of()
                  .Value("hello").WithWeight(4)
                  .AndValue("world") // no weight
                  .PickOne();
// The default weight is 1.
// randomString is "hello" with a probability of 80% (4 of 5) and "world" with a probability of 20% (1 of 5).
```

### Specifying the returned type explicitly
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

## Advanced

### Specifying a seed
```c#
var seed = 1234567;

var value1 = Out.Of(seed).Values(new[] { 1, 2, 3, 4 }).PickOne();
var value2 = Out.Of(seed).Values(new[] { 1, 2, 3, 4 }).PickOne();
// value1 and value2 are always equal.
```

### Using a different random number generator
The default random number generator uses System.Random.

Alternative: Using a cryptographically secure implementation that uses System.Security.Cryptography.RandomNumberGenerator:

```c#
var secureRng = new FluentRandomPicker.Random.SecureRandomNumberGenerator();
var value = Out.Of(secureRng).Values(new[] { 1, 2, 3, 4 }).PickOne();
```

Alternative: Using own implementation:

```c#
public class MyOwnRandomNumberGenerator : IRandomNumberGenerator
{
    public double NextDouble()
    {
        // ...
    }

    public int NextInt()
    {
        // ...
    }

    public int NextInt(int n)
    {
        // ...
    }

    public int NextInt(int min, int max)
    {
        // ...
    }
}

var myRng = new MyOwnRandomNumberGenerator();
var value = Out.Of(myRng).Values(new[] { 1, 2, 3, 4 }).PickOne();
// value gets picked via a specified random number generator.
```


## Migration to version 3

The namespace was changed to match coding conventions.
Please replace:

```c#
using Fluent_Random_Picker;
```
with
```c#
using FluentRandomPicker;
```

Some method parameter identifiers do also have  changed to match [the coding conventions of Microsoft](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).


## Migration to version 2

The params array is not supported anymore for the Values method. Please replace 
```c#
Out.Of().Values(1, 2, 3)...
```
with
```c#
Out.Of().Value(1).AndValue(2).AndValue(3)...
```
or
```c#
Out.Of().Values(new List<int>{ 1, 2, 3 })...
```

Reason: https://github.com/ndsvw/Fluent-Random-Picker/commit/da9af06bda215d0983e428072febed8f33577041
