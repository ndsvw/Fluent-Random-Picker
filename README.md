# Fluent Random Picker

![Fluent Random Picker](https://raw.githubusercontent.com/ndsvw/Fluent-Random-Picker/main/FluentRandomPicker/icon48x48.png "Fluent Random Picker")

Fluent Random Picker is a user-friendly, but also performant .NET library that simplifies random value selection / picking. It allows you to specify probabilities and weights for each value, making it easy to handle complex randomization tasks.

[![License MIT](https://img.shields.io/github/license/ndsvw/Fluent-Random-Picker)](https://github.com/ndsvw/Fluent-Random-Picker/blob/main/LICENSE)
[![Build status](https://github.com/ndsvw/Fluent-Random-Picker/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ndsvw/Fluent-Random-Picker)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ndsvw_Fluent-Random-Picker&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=ndsvw_Fluent-Random-Picker)
[![Code coverage](https://img.shields.io/codecov/c/github/ndsvw/Fluent-Random-Picker)](https://app.codecov.io/gh/ndsvw/Fluent-Random-Picker)
[![Nuget version](https://img.shields.io/nuget/v/FluentRandomPicker)](https://www.nuget.org/packages/FluentRandomPicker)
[![Release notes](https://img.shields.io/badge/release%20notes-%F0%9F%97%92-green
)](RELEASENOTES.md)
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
    - [Migration to version 3](#migration-to-version-3)

<!-- /TOC -->

## Compatibility

Fluent Random Picker targets .Net Standard 2.0 and is therefore compatible with the following target frameworks:

- ✔️ .Net 8
- ✔️ .Net 7
- ✔️ .Net 6
- ✔️ .Net 5
- ✔️ .Net Core 3.X
- ✔️ .Net Core 2.X
- ❌ .Net Core 1.X
- 
- ✔️ .Net Standard 2.1
- ✔️ .Net Standard 2.0
- ❌ .Net Standard 1.X
-
- ✔️ .Net Framework 4.8
- ✔️ .Net Framework 4.7.2
- ❌ .Net Framework 4.6.X

## Getting started

Install the nuget package (https://www.nuget.org/packages/FluentRandomPicker/)

Add the using directive:
```c#
using FluentRandomPicker;
```

To get started, use the `Out.Of()` syntax as shown in the examples below:

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
// randomChar is 'a' with a probability of 70 % and 'b' with a probability of 30 %.
```

### Specifying weights
```c#
var randomString = Out.Of()
                  .Value("hello").WithWeight(2)
                  .AndValue("world").WithWeight(3)
                  .PickOne();
// randomString is "hello" or "world", but the probability for "world" is 1.5 times as high.
```

### Specifying multiple values
```c#
var randomChar = Out.Of().Values(new List<char> { 'a', 'b' })
                  .WithPercentages(new List<int> { 70, 30 })
                  .PickOne();
// randomChar is 'a' with a probability of 70 % and 'b' with a probability of 30 %.

var randomChar = Out.Of().Values(new HashSet<string> { "hello", "world" })
                  .WithWeights(new List<int> { 2, 3 })
                  .PickOne();
// randomString is "hello" or "world", but the probability for "world" is 1.5 times as high.
```

### Picking multiple values
```c#
var randomInts = Out.Of()
                  .Value(1).WithPercentage(70)
                  .AndValue(10).WithPercentage(15)
                  .AndValue(100).WithPercentage(10)
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
// randomInts can be [1, 10], [1, 100], ..., [1000, 100], but not [1, 1], [10, 10], ...
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

Please see [README-Advanced.md](README-Advanced.md) for more advanced topics like:

- [specifying a seed](README-Advanced.md#specifying-a-seed)
- [using a secure RNG](README-Advanced.md#using-a-secure-rng)
- [using a custom RNG](README-Advanced.md#using-a-custom-rng)

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

Some method parameter identifiers do also have changed to match [the coding conventions of Microsoft](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
