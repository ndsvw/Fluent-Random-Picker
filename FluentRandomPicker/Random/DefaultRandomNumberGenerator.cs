namespace FluentRandomPicker.Random
{
    /// <summary>
    /// The default implementation for <see cref="IRandomNumberGenerator"/>.
    /// </summary>
    public class DefaultRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly System.Random _random;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRandomNumberGenerator"/> class.
        /// </summary>
        public DefaultRandomNumberGenerator()
        {
            _random = new System.Random();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRandomNumberGenerator"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public DefaultRandomNumberGenerator(int seed)
        {
            _random = new System.Random(seed);
        }

        /// <inheritdoc/>
        public double NextDouble()
        {
            return _random.NextDouble();
        }

        /// <inheritdoc/>
        public int NextInt()
        {
            return _random.Next();
        }

        /// <inheritdoc/>
        public int NextInt(int n)
        {
            return _random.Next(n);
        }

        /// <inheritdoc/>
        public int NextInt(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
