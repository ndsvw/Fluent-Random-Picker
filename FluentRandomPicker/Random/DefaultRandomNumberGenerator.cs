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
        /// <param name="pSeed">The seed.</param>
        public DefaultRandomNumberGenerator(int pSeed)
        {
            _random = new System.Random(pSeed);
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
        public int NextInt(int pN)
        {
            return _random.Next(pN);
        }

        /// <inheritdoc/>
        public int NextInt(int pMin, int pMax)
        {
            return _random.Next(pMin, pMax);
        }
    }
}
