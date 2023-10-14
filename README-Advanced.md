# Fluent Random Picker: Advanced

## Specifying a seed
```c#
var seed = 1234567;

var value1 = Out.Of(seed).Values(new[] { 1, 2, 3, 4 }).PickOne();
var value2 = Out.Of(seed).Values(new[] { 1, 2, 3, 4 }).PickOne();
// value1 and value2 are always equal.
```

## Using a secure RNG
The default random number generator uses System.Random, which is not cryptographically secure.

This is how you make Fluent Random Picker use an implementation based on the [cryptographically strong RandomNumberGenerator class](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator?view=net-7.0).

```c#
var secureRng = new FluentRandomPicker.Random.SecureRandomNumberGenerator();
var value = Out.Of(secureRng).Values(new[] { 1, 2, 3, 4 }).PickOne();
```

## Using a custom RNG

Create a class that implements `IRandomNumberGenerator` and pass it to the `Of()` method:

```c#
public class MyCustomRandomNumberGenerator : IRandomNumberGenerator
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

var customRng = new MyCustomRandomNumberGenerator();
var value = Out.Of(customRng).Values(new[] { 1, 2, 3, 4 }).PickOne();
// value gets picked via a specified random number generator.
```