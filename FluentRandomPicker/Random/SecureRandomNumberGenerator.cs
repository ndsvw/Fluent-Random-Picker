using System;
using System.Security.Cryptography;

namespace FluentRandomPicker.Random
{
    /// <summary>
    /// A secure implementation of <see cref="IRandomNumberGenerator"/> that uses the
    /// RandomNumberGenerator class of .Net.
    /// </summary>
    public class SecureRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly byte[] _tmpBytes = new byte[8];
        private readonly RandomNumberGenerator _secureRng = RandomNumberGenerator.Create();

        /// <inheritdoc/>
        public double NextDouble()
        {
            _secureRng.GetBytes(_tmpBytes);
            var bytesAsLong = BitConverter.ToInt64(_tmpBytes, 0);
            return (double)(bytesAsLong & long.MaxValue) / long.MaxValue;
        }

        /// <inheritdoc/>
        public int NextInt()
        {
#if NET5_0_OR_GREATER
            return RandomNumberGenerator.GetInt32(int.MaxValue);
#else
            _secureRng.GetBytes(_tmpBytes, 4, 4);
            var bytesAsInt32 = BitConverter.ToInt32(_tmpBytes, 4);
            return Math.Abs(bytesAsInt32);
#endif
        }

        /// <inheritdoc/>
        public int NextInt(int n)
        {
#if NET5_0_OR_GREATER
            return RandomNumberGenerator.GetInt32(n);
#else
            return NextInt() % n;
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
}
