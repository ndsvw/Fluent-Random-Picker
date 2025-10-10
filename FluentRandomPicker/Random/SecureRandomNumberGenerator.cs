using System;
using System.Security.Cryptography;

namespace FluentRandomPicker.Random;

/// <summary>
/// A secure implementation of <see cref="IRandomNumberGenerator"/> that uses the
/// RandomNumberGenerator class of .Net.
/// </summary>
public class SecureRandomNumberGenerator : IRandomNumberGenerator
{
    private readonly RandomNumberGenerator _secureRng = RandomNumberGenerator.Create();

    /// <inheritdoc/>
    public double NextDouble()
    {
#if NETCOREAPP3_0_OR_GREATER
        Span<byte> tmpBytes = stackalloc byte[8];
        _secureRng.GetBytes(tmpBytes);
        var bytesAsLong = BitConverter.ToInt64(tmpBytes);
#else
        var tmpBytes = new byte[8];
        _secureRng.GetBytes(tmpBytes);
        var bytesAsLong = BitConverter.ToInt64(tmpBytes, 0);
#endif
        return (double)(bytesAsLong & long.MaxValue) / long.MaxValue;
    }

    /// <inheritdoc/>
    public int NextInt()
    {
#if NET5_0_OR_GREATER
        return RandomNumberGenerator.GetInt32(int.MaxValue);
#else
        var tmpBytes = new byte[8];
        _secureRng.GetBytes(tmpBytes, 4, 4);
        var bytesAsInt32 = BitConverter.ToInt32(tmpBytes, 4);

        // because Math.Abs(Int32.MinValue) throws exception.
        if (bytesAsInt32 == int.MinValue)
            return NextInt();

        return Math.Abs(bytesAsInt32);
#endif
    }

    /// <inheritdoc/>
    public int NextInt(int n)
    {
#if NET5_0_OR_GREATER
        return RandomNumberGenerator.GetInt32(n);
#else
        return (int)(NextDouble() * n); // working with NextInt() + modulo instead casuses modulo bias!
#endif
    }

    /// <inheritdoc/>
    public int NextInt(int min, int max)
    {
#if NET5_0_OR_GREATER
        return RandomNumberGenerator.GetInt32(min, max);
#else
        return min + NextInt(max - min);
#endif
    }
}