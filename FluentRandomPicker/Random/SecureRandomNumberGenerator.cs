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
        // Use (long.MaxValue + 1.0) as divisor to guarantee result is in [0, 1).
        // long.MaxValue + 1.0 == 9223372036854775808.0, exactly representable as a double (power of 2).
        return (double)(bytesAsLong & long.MaxValue) / (long.MaxValue + 1.0);
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
        var result = (int)(NextDouble() * n);
        return Math.Min(result, n - 1); // clamp to [0, n) as a safety net
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